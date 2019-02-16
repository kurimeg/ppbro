using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace frontend.API.Models
{
    public class Profile
    {
        public string Address;
        public string OrgAddress;
        public string MySign;
        public string OrgSign;
        public List<Proof> Proof = new List<Proof>();
        public string[] ProfileAddress;
        public string LimitDate;
        public string Pubkey;
   }
}