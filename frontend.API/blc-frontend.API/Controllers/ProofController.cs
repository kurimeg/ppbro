using frontend.API.Models;
using frontend.API.Models.UI;
using frontend.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Text;
using frontend.API.Utility;
using System.Collections;

namespace frontend.API.Controllers
{
    [RoutePrefix("api/proof")]
    public class ProofController : ApiController
    {
        private readonly ProfileService _profileService = new ProfileService();
        private readonly BlockchainRepository _repository = new BlockchainRepository();

        [HttpGet]
        [Route("{address}")]
        public async Task<IEnumerable<ProofResult>> Get([FromUri] string[] address)
        {
            return await _profileService.GetProofListByProfileAddresses(address);
        }

        [HttpPost]
        [Route("")]
        public async Task Post([FromBody] IEnumerable<IssueProofRequest> proofs)
        {
            foreach (var proof in proofs)
            {
                await _profileService.IssueProof(proof);
            }
        }

        [HttpPost]
        [Route("send")]
        public async Task<dynamic> SendProofs([FromBody] SendProofRequest request)
        {
            // Create address to send profiles
            var destination = await _profileService.CreateProfile(request.TargetOrgAddress);

            // sign to profile addresses
            var signature = DigitalSignature.FromKey(
                Convert.FromBase64String(destination.PrivateKey),
                Convert.FromBase64String(destination.PublicKey));
            var joindAddresses = string.Join(", ", request.ProfileAddressList);
            byte[] signedValue = signature.Sign(EncodingUtil.GetEncoding().GetBytes(joindAddresses));

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
            byte[] encrypted = DigitalSignature.Encrypt(EncodingUtil.GetEncoding().GetBytes(joindAddresses), Convert.FromBase64String(destination.PrivateKey));
            var token = Convert.ToBase64String(encrypted);
            return new
            {
                AccessToken = token,
                Destination = destination
            };
        }

        [HttpPost]
        [Route("show")]
        public async Task<IEnumerable<ProofResult>> ListPoof([FromBody] ShowProof param)
        {
            string[] addresses = _profileService.DecryptAddresses(param.Token, param.PrivateKey);
            //return await _profileService.GetProofListByProfileAddressesToShow(addresses);//TODO: テスト不足で不安なため暫定コメントアウト
            return await _profileService.GetProofListByProfileAddresses(addresses);
        }

        [HttpOptions]
        [Route("send")]
        [Route("show")]
        public void Options()
        {

        }
    }
}