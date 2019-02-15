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
        public async Task SendProofs([FromBody] SendProofRequest request)
        {
            var param = new Hashtable();
            param["address"] = request.Address;
            param["limitDate"] = request.LimitDate;
            param["mySign"] = request.MySign;
            param["pubKey"] = request.PubKey;
            param["profileAddressList"] = request.ProfileAddressList;

            return await _repository.SendProofs(param);
        }
    }
}
