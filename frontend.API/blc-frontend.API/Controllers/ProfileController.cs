using frontend.API.Models;
using frontend.API.Models.UI;
using frontend.API.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace frontend.API.Controllers
{
    public class ProfileController : ApiController
    {
        [HttpPost]
        public async Task<UserProfile> Create(Issuer issuer)
        {
            ProfileService service = new ProfileService();
            return await service.CreateProfile(issuer.Address);
        }

        //[HttpGet]
        //public async Task<IEnumerable<Proof>> Get(string id)
        //{
        //    ProfileService service = new ProfileService();

        //    return await service.GetProofListByProfileAddresses(id);
        //}
    }
}