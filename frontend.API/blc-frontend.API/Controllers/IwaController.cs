using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using frontend.API.Models;
using frontend.API.Models.UI;
using frontend.API.Services;
using System.Text;
using frontend.API.Utility;

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
        public async Task<dynamic> SendProofs([FromBody] SendProofRequest request)
        {
            // Create address to send profiles
            var destination = await _profileService.CreateProfile(request.TargetOrgAddress);

            // sign to profile addresses
            var signature = DigitalSignature.FromKey(
                Convert.FromBase64String(destination.PrivateKey), 
                Convert.FromBase64String(destination.PublicKey));
            var joindAddresses = string.Join(", ", request.ProfileAddressList);
            byte[] signedValue = signature.Sign(EncodingUtil.GetEncoding.GetBytes(joindAddresses));

            // send profile addresses
            var param = new Hashtable();
            param["address"] = destination.Address;
            param["limitDate"] = DateTime.UtcNow.AddHours(24).ToString("yyyy-MM-dd HHmmss");
            param["mySign"] = Convert.ToBase64String(signedValue);
            param["pubKey"] = destination.PublicKey;
            param["profileAddressList"] = request.ProfileAddressList;

            await _repository.SendProofs(param);

            // create access token
            // TODO: encrypted token by company private key
            byte[] encrypted = DigitalSignature.Encrypt(EncodingUtil.GetEncoding.GetBytes(joindAddresses), Convert.FromBase64String(destination.PrivateKey));
            var token = Convert.ToBase64String(encrypted);
            return new {
                AccessToken = token,
                Destination = destination
            };
        }
    }
}
