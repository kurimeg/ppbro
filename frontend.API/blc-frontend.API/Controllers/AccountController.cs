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
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private readonly ProfileService service = new ProfileService();

        private readonly BlockchainRepository _repository = new BlockchainRepository();
        private readonly ProfileService _profileService = new ProfileService();

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
        [Route("profile")]
        public async Task<UserProfile> Create([FromBody] Issuer issuer)
        {
            return await service.CreateProfile(issuer.Address);
        }

        [HttpOptions]
        [Route("profile")]
        public void Options()
        {

        }
    }
}