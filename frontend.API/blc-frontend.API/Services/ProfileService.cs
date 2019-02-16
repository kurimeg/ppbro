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

        public async Task<UserProfile> CreateProfile(string issuerName)
        {
            DigitalSignature ds = DigitalSignature.Generate();
            var privateKey = Convert.ToBase64String(ds.PrivateKey);
            var publicKey = Convert.ToBase64String(ds.PublicKey);

            IEnumerable<Issuer> issuers = await iwarepo.GetIssuers();
            Issuer _issuer = issuers.Where(x => x.Name == issuerName).Select(x => x).FirstOrDefault();

            var guid = Guid.NewGuid();
            var address = guid.ToString();
            var orgAddress = _issuer.Address;//  名前で判定（仮）
            var mySign = ds.Sign(Guid.NewGuid().ToByteArray());
            var orgSign = _issuer.Pubkey;
            await repo.InitProfile(address, orgAddress, Convert.ToBase64String(mySign), orgSign, publicKey);

            return new UserProfile { PrivateKey = privateKey, PublicKey = publicKey, Address = address };
        }

        public async Task IssueProof(IssueProofRequest proof)
        {
            var id = Guid.NewGuid().ToString();
            var address = proof.Address;
            var value = proof.Value;
            //var profiles = await repo.GetProfileByAddresses(new string[] { address });
            Profile profile = (await repo.GetProfileByAddresses(new string[] { address })).FirstOrDefault();
            DigitalSignature ds = DigitalSignature.FromKey(Convert.FromBase64String(proof.PrivateKey), Convert.FromBase64String(profile.OrgSign));
            byte[] signedValue = ds.Sign(System.Text.Encoding.ASCII.GetBytes(value));

            await repo.IssueProof(id, address, value, Convert.ToBase64String(signedValue));
        }

        public async Task<IEnumerable<Proof>> GetProofListByProfileAddresses(string[] addresses)
        {
            var profiles = await repo.GetProfileByAddresses(addresses);
            return profiles.SelectMany(profile => profile.Proof);
        }
    }
}