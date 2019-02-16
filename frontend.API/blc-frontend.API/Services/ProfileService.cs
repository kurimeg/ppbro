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
        IwaRepository iwarepo = new IwaRepository();

        public async Task<UserProfile> CreateProfile(string issuerAddress)
        {
            DigitalSignature ds = DigitalSignature.Generate();
            var privateKey = Convert.ToBase64String(ds.PrivateKey);
            var publicKey = Convert.ToBase64String(ds.PublicKey);

            IEnumerable<Issuer> issuers = await iwarepo.GetIssuers();
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
            IEnumerable<Issuer> issuers = await iwarepo.GetIssuers();
            Issuer issuer = issuers.Where(x => x.Address == param.IssuerAddress).Select(x => x).FirstOrDefault();
            Profile profile = (await repo.GetProfileByAddresses(new string[] { address })).FirstOrDefault();
            DigitalSignature ds = DigitalSignature.FromKey(Convert.FromBase64String(param.PrivateKey), Convert.FromBase64String(issuer.Pubkey));
            byte[] signedValue = ds.Sign(EncodingUtil.GetEncoding().GetBytes(value));//エンコーディング要確認

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
            var issuers = await iwarepo.GetIssuers();
            var profiles = await repo.GetProfileByAddresses(addresses);
            foreach (var profile in profiles)
            {
                var orgName = issuers.Where(x => x.Address == profile.OrgAddress).FirstOrDefault().Name;
                var publickey = profile.Pubkey;
                DigitalSignature signature = DigitalSignature.FromKey(Convert.FromBase64String(profile.Pubkey));
                foreach (var proof in profile.Proof)
                {
                    list.Add(new ProofResult {
                        DateTime = proof.DateTime,
                        Id = proof.Id,
                        OrgName = orgName,
                        Value = proof.Value,
                        Verified = signature.Verify(encoding.GetBytes(proof.Value), Convert.FromBase64String(proof.OrgSign))
                    });
                }
            }
            return list;
        }

    }
}