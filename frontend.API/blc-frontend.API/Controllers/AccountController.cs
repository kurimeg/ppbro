using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using frontend.API.Services;
//using frontend.API.Models.UI;
using frontend.API.Models;

namespace frontend.API.Controllers
{
    public class AccountController : ApiController
    {
        // GET: api/Account
        public IEnumerable<string> Get()
        {
            DigitalSignature ds = DigitalSignature.Generate();
            byte[] privateKey = ds.PrivateKey;
            byte[] publicKey = ds.PublicKey;
            //return new string[] { "value1", "value2" };
            return new string[] {
                System.Text.Encoding.ASCII.GetString(privateKey) ,
                System.Text.Encoding.ASCII.GetString(publicKey) };
        }

        // GET: api/Account/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Account
        public async Task Post(HttpRequestMessage value)
        {
            //DigitalSignature ds = DigitalSignature.Generate();
            //var param = await value.Content.ReadAsStringAsync(); 
            //var issuer = JsonConvert.DeserializeObject<Issuer>(param);

            //Models.UI.Profile res = new Models.UI.Profile() { PrivateKey = Convert.ToBase64String(ds.PrivateKey), PublicKey = Convert.ToBase64String(ds.PublicKey) };
            //var repo = new BlockchainRepository();
            //var iwarepo = new IwaRepository();

            //var orgs = await iwarepo.GetIssuers();

            //var guid = Guid.NewGuid();
            //var address = guid.ToString();
            //var orgAddress = orgs.Where(x => x.Name == issuer.Name).Select(x => x.Address).FirstOrDefault();//  名前で判定（仮）
            //var mySign = ds.Sign(Guid.NewGuid().ToByteArray());// Convert.ToBase64String(ds.PrivateKey);//アドレスを署名するのか
            //var orgSign = "";//これは大学に入れてもらうからブランクにしておく
            //await repo.InitProfile(address, orgAddress, Convert.ToBase64String(mySign), orgSign, publicKey);

            //return res;
        }

        // PUT: api/Account/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Account/5
        public void Delete(int id)
        {
        }
    }
}
