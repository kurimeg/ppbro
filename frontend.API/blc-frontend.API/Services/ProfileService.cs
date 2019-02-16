using frontend.API.Models;
using frontend.API.Models.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Text;
using frontend.API.Utility;

namespace frontend.API.Services
{
    public class ProfileService
    {
        BlockchainRepository repo = new BlockchainRepository();

        public async Task<UserProfile> CreateProfile(string issuerAddress)
        {
            DigitalSignature ds = DigitalSignature.Generate();
            var privateKey = Convert.ToBase64String(ds.PrivateKey);
            var publicKey = Convert.ToBase64String(ds.PublicKey);

            IEnumerable<Issuer> issuers = await repo.GetIssuers();
            Issuer _issuer = issuers.Where(x => x.Address == issuerAddress).Select(x => x).FirstOrDefault();

            var guid = Guid.NewGuid();
            var address = guid.ToString();
            var mySign = ds.Sign(Guid.NewGuid().ToByteArray());
            var orgSign = "";
            await repo.InitProfile(address, issuerAddress, Convert.ToBase64String(mySign), orgSign, publicKey);

            return new UserProfile { PrivateKey = privateKey, PublicKey = publicKey, Address = address };
        }

        public async Task IssueProof(IssueProofRequest param)
        {
            var id = Guid.NewGuid().ToString();
            var address = param.ProfileAddress;
            var value = param.Value;
            IEnumerable<Issuer> issuers = await repo.GetIssuers();
            Issuer issuer = issuers.Where(x => x.Address == param.IssuerAddress).Select(x => x).FirstOrDefault();
            Profile profile = (await repo.GetProfileByAddresses(new string[] { address })).FirstOrDefault();
            DigitalSignature ds = DigitalSignature.FromKey(Convert.FromBase64String(param.PrivateKey), Convert.FromBase64String(issuer.Pubkey));
            byte[] signedValue = ds.Sign(EncodingUtil.GetEncoding().GetBytes(value));

            await repo.IssueProof(id, address, value, Convert.ToBase64String(signedValue));
        }

        public async Task<Profile> GetProfile(string address)
        {
            return await repo.GetProfileByAddress(address);
        }

        public async Task<IEnumerable<ProofResult>> GetProofListByProfileAddresses(string[] addresses)
        {
            var list = new List<ProofResult>();
            var encoding = EncodingUtil.GetEncoding();
            var issuers = await repo.GetIssuers();
            var profiles = await repo.GetProfileByAddresses(addresses);

            return profiles.SelectMany(profile => {
                var issuer = issuers.Where(x => x.Address == profile.OrgAddress).FirstOrDefault();
                var orgName = issuer.Name;
                var publickey = issuer.Pubkey;
                DigitalSignature signature = DigitalSignature.FromKey(Convert.FromBase64String(publickey));
                return profile.Proof.Select(proof =>
                {
                    return new ProofResult
                    {
                        ProfileAddress = profile.Address,
                        DateTime = proof.DateTime.ToString("yyyy-MM-dd"),
                        LimitDate = profile.LimitDate,
                        Id = proof.Id,
                        OrgName = orgName,
                        Value = proof.Value,
                        Verified = signature.Verify(
                            encoding.GetBytes(proof.Value), 
                            Convert.FromBase64String(proof.OrgSign))
                    };
                });
            }).ToArray();
        }

        public string[] DecryptAddresses(string token, string key)
        {
            var tokenBytes = Convert.FromBase64String(token);
            var keyBytes = Convert.FromBase64String(key);
            string joinedAddresses = EncodingUtil.GetEncoding().GetString(DigitalSignature.Decrypt(tokenBytes, keyBytes));
            return joinedAddresses.Split(',');
        }


        public async Task<IEnumerable<ProofResult>> GetProofListByProfileAddressesToShow(string[] addresses)
        {
            var current = DateTime.UtcNow;
            return (await GetProofListByProfileAddresses(addresses)).Where(x =>
                current < DateTime.ParseExact(x.LimitDate, "yyyy-MM-dd HHmmss", null)).ToArray();
        }
       
    }
}