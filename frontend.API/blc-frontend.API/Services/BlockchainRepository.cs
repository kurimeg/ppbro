using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using frontend.API.Models;
using frontend.API.Models.Blockchain;
//using frontend.API.Models.UI;
using Newtonsoft.Json;
using System.Diagnostics;

namespace frontend.API.Services
{
    public class BlockchainRepository
    {
        private const string BASE_URL = "http://52.246.180.35:3000";

        private HttpClient CreateBlockchainClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public async Task<Organization> GetOrg()
        {
            return new Organization();
        }

        public async Task InitProfile(string address, string orgAddress, string mySign, string orgSign, string publicKey)
        {
            var client = CreateBlockchainClient();

            var param = new
            {
                address,
                orgAddress,
                mySign,
                orgSign,
                pubKey = publicKey
            };

            var json = JsonConvert.SerializeObject(param);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/initProfile", content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<IEnumerable<PostResult>>();
                if (result.Any(x => x.Status == "SUCCESS"))
                {
                    Trace.WriteLine("Profile initialized");
                }
            }
            Trace.WriteLine(response.ToString());
        }

        public async Task<IEnumerable<Profile>> GetProfileByAddresses(string[] addresses)
        {
            var client = CreateBlockchainClient();

            //var a = addresses.Select(async address => {
            //    var param = new { address };

            //    var json = JsonConvert.SerializeObject(param);
            //    var content = new StringContent(json, Encoding.UTF8, "application/json");
            //    var response = await client.PostAsync("/getProfile", content);
            //    //if (response.IsSuccessStatusCode)
            //    //{
            //    //    return await response.Content.ReadAsAsync<Profile>();
            //    //}
            //    var aa = await response.Content.ReadAsAsync<Profile>();

            //    return new Profile { Address = aa.Address, MySign = aa.MySign, OrgAddress = aa.OrgAddress, OrgSign = aa.OrgSign, Proof = aa.Proof};
            //}).ToArray();
            //return a;

            var profileList = new List<Profile>();
            foreach (var address in addresses)
            {
                var param = new { address };
                var json = JsonConvert.SerializeObject(param);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/getProfile", content);
                if (response.IsSuccessStatusCode)
                {
                    profileList.Add(await response.Content.ReadAsAsync<Profile>());
                }
            }
            return profileList;
        }

        public async Task IssueProof(string id, string address, string value, string orgSign)
        {
            var client = CreateBlockchainClient();

            var param = new
            {
                id,
                address,
                value,
                orgSign
            };
            var json = JsonConvert.SerializeObject(param);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/issueProof", content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<IEnumerable<PostResult>>();
                if (result.Any(x => x.Status == "SUCCESS"))
                {
                    Trace.WriteLine("Issued Proof");
                }
            }
            Trace.WriteLine(response.ToString());
        }

        public async Task<Profile> GetProfileByAddress(string address)
        {
            var client = CreateBlockchainClient();

            var param = new { address };
            var json = JsonConvert.SerializeObject(param);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/getProfile", content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Profile>();
            }
            return new Profile();
        }

    }
}