﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace frontend.API.Models.UI
{
    public class SendProofRequest
    {
        public string TargetOrgAddress;
        public List<string> ProfileAddressList = new List<string>();
    }
}