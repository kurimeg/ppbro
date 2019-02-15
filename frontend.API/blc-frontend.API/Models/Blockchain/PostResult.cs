using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace frontend.API.Models.Blockchain
{
    //    [
    //        {
    //        "status": "SUCCESS",
    //            "info": ""
    //    },
    //    {
    //        "event_status": "VALID",
    //        "tx_id": "94bbd358181edd6feb3f16de2084025c2302b47592d1aa05519c63547f67b9eb"
    //    }
    //]    
    public class PostResult
    {
        public string Status;
        public string Info;
        public string EventStatus;
        public string TxId;
    }
}