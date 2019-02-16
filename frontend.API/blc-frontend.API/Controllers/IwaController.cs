using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using frontend.API.Models;
using frontend.API.Models.UI;
using frontend.API.Services;

namespace frontend.API.Controllers
{
    [RoutePrefix("api/iwa")]
    public class IwaController : ApiController
    {
        IwaRepository _repository = new IwaRepository();
        ProfileService _profileService = new ProfileService();

        [HttpGet]
        [Route("issuers")]
        public async Task<IEnumerable<Issuer>> Issuers()
        {
            return await _repository.GetIssuers();
        }

        [HttpGet]
        [Route("requestedprofiles/{address}")]
        public async Task<IEnumerable<Profile>> RequestedProfiles([FromUri] string address)
        {
            return await _repository.GetProfilesByIssuerAddress(address);
        }

        [HttpPost]
        [Route("sendproofs")]
        public async Task<Hashtable> SendProofs([FromBody] List<String> profileAddressList)
        {
            // create cmpany temporary bank account
           var profile = await _profileService.CreateProfile("公開先企業名");

            var target = string.Join(", ", profileAddressList);
            var signature = DigitalSignature.FromKey(Convert.FromBase64String(profile.PrivateKey),  Convert.FromBase64String(profile.PublicKey));
            byte[] signedValue = signature.Sign(System.Text.Encoding.ASCII.GetBytes(target));

            // send profiles to temporary bank account
            var param = new Hashtable();
            param["address"] = profile.Address;
            param["limitDate"] = DateTime.UtcNow.AddHours(24).ToString("yyyy-MM-dd HHmmss");
            param["mySign"] = Convert.ToBase64String(signedValue);
            param["pubKey"] = profile.PublicKey;
            param["profileAddressList"] = profileAddressList;

            return param;
             //await _repository.SendProofs(param);
        }
    }
}
