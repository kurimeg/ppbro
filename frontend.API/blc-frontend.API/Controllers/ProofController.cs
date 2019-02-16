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

namespace frontend.API.Controllers
{
    [RoutePrefix("api/proof")]
    public class ProofController : ApiController
    {

        [HttpGet]
        [Route("{address}")]
        public async Task<IEnumerable<ProofResult>> Get([FromUri] string[] address)
        {
            ProfileService service = new ProfileService();
            return await service.GetProofListByProfileAddresses(address);
        }

        [HttpPost]
        [Route("")]
        public async Task Post(IssueProofRequest proof)
        {
            ProfileService service = new ProfileService();
            await service.IssueProof(proof);
        }

        [HttpPost]
        [Route("show")]
        public async Task<IEnumerable<ProofResult>> ListPoof([FromBody] ShowProof param)
        {
            ProfileService service = new ProfileService();
            string[] addresses = service.DecryptAddresses(param.Token, param.PrivateKey);
            return await service.GetProofListByProfileAddresses(addresses);
        }
    }
}