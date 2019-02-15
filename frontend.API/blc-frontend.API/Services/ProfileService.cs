using frontend.API.Models;
using frontend.API.Models.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace frontend.API.Services
{
    public class ProfileService
    {
        BlockchainRepository repo = new BlockchainRepository();
        IwaRepository iwarepo = new IwaRepository();

        public async Task<UserProfile> CreateProfile(Issuer issuer)
        {
            DigitalSignature ds = DigitalSignature.Generate();
            var privateKey = Convert.ToBase64String(ds.PrivateKey);
            var publicKey = Convert.ToBase64String(ds.PublicKey);

            IEnumerable<Issuer> issuers = await iwarepo.GetIssuers();

            var guid = Guid.NewGuid();
            var address = guid.ToString();
            var orgAddress = issuers.Where(x => x.Name == issuer.Name).Select(x => x.Address).FirstOrDefault();//  名前で判定（仮）
            var mySign = ds.Sign(Guid.NewGuid().ToByteArray());
            var orgSign = "";//大学に入れてもらうからブランク
            await repo.InitProfile(address, orgAddress, Convert.ToBase64String(mySign), orgSign, publicKey);

            return new UserProfile { PrivateKey = privateKey, PublicKey = publicKey, Address = address };
        }

        public async Task IssueProof(UserProof userProof)
        {
            
        }

        public async Task<IEnumerable<Proof>> GetProofListByProfileAddresses(string[] addresses)
        {
            var profiles = await repo.GetProfileByAddresses(addresses);
            return profiles.SelectMany(profile => profile.Proof);
        }
    }
}