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
        public async Task<Hashtable> SendProofs([FromBody] List<String> profileAddresses)
        {
            // create cmpany temporary bank account
           var profile = await _profileService.CreateProfile("temprary");

            // send profiles to temporary bank account
            var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            var param = new Hashtable();
            param["address"] = profile.Address;
            param["limitDate"] = request.LimitDate;
            param["mySign"] = request.MySign;
            param["pubKey"] = request.PubKey;
            param["profileAddressList"] = profileAddresses;

            return param
             //await _repository.SendProofs(param);
        }
    }
}
