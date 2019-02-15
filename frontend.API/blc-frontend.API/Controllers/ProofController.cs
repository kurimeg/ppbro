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

namespace frontend.API.Controllers
{
    public class ProofController : ApiController
    {
        [HttpGet]
        public async Task<IEnumerable<Proof>> Get(string id)
        {
            ProfileService service = new ProfileService();

            return await service.GetProofListByProfileAddresses(new string[] { id });
        }

        [HttpGet]
        public async Task<IEnumerable<Proof>> Get([FromUri] string[] ids)
        {
            ProfileService service = new ProfileService();

            return await service.GetProofListByProfileAddresses(ids);
            //return new Proof[] { new Proof { Id = "aaaa", DateTime = DateTime.Now.ToString("yyyy-mm-"), OrgSign = "", Value = "" } };
        }


        [HttpPost]
        public async Task Post(UserProof userProof)
        {
            ProfileService service = new ProfileService();
            await service.IssueProof(userProof);
        }
    }
}