using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace frontend.API.Models.Blockchain
{
    public class ProfileByOrgAddress
    {
        public string Address;
        public string OrgAddress;
        public string MySign;
        public string OrgSign;
        Proof[] Proof;
    }
}