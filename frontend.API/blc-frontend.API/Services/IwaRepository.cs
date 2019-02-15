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
using frontend.API.Models.UI;
//using frontend.API.Models.UI;
using Newtonsoft.Json;

namespace frontend.API.Services
{
    public class IwaRepository
    {
        private const string BASE_URL = "http://52.246.180.35:3000";

        public async Task<IEnumerable<Issuer>> GetIssuers()
        {
            var client = CreateBlockchainClient();

            var param = new Hashtable();
            var json = JsonConvert.SerializeObject(param);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/getorg", content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<IEnumerable<Organization>>();
               return result.Select(item => {
                        return new Issuer
                        {
                            Address = item.Record.Address,
                            Name = item.Record.Name,
                            Pubkey = item.Record.Pubkey
                        };
                });
            }
            return new List<Issuer>();
        }

        public async Task<IEnumerable<Models.Profile>> GetProfilesByIssuerAddress(string address)
        {
            var client = CreateBlockchainClient();

            var param = new Hashtable();
            param["orgAddress"] = address;
            var json = JsonConvert.SerializeObject(param);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/getProfileByOrgAddress", content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<IEnumerable<RequestedProfile>>();
                return result.Select(item => {
                    return new Profile
                    {
                        Address = item.Record.Address,
                        MySign = item.Record.MySign,    
                        OrgAddress = item.Record.OrgAddress,
                        OrgSign = item.Record.OrgSign,
                        Proof = item.Record.Proof
                    };
                });
            }
            return new List<Profile>();
        }

        public async Task<IEnumerable<Models.Profile>> SendProof(Hashtable param)
        {
            var client = CreateBlockchainClient();
            var json = JsonConvert.SerializeObject(param);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/getProfileByOrgAddress", content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<IEnumerable<RequestedProfile>>();
                return result.Select(item => {
                    return new Profile
                    {
                        Address = item.Record.Address,
                        MySign = item.Record.MySign,
                        OrgAddress = item.Record.OrgAddress,
                        OrgSign = item.Record.OrgSign,
                        Proof = item.Record.Proof
                    };
                });
            }
            return new List<Profile>();
        }

        private HttpClient CreateBlockchainClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }

}