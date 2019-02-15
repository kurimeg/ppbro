namespace frontend.API.Models.Blockchain
{
    //	{
    //		"Key": "U7F1B20C8E6DD429A90C090BF39334934",
    //		"Record": {
    //			"Address": "U7F1B20C8E6DD429A90C090BF39334934",
    //			"Name": "Okinawa University",
    //			"Pubkey": "pubkey03"
    //		}
    //	},
    //	{
    //		"Key": "U8F1B20C8E6DD429A90C090BF39334934",
    //		"Record": {
    //			"Address": "U8F1B20C8E6DD429A90C090BF39334934",
    //			"Name": "Tokyo University",
    //			"Pubkey": "pubkey02"
    //		}
    //	}

    public class Organization
    {
        public string Key { get; set; }
        public Issuer Record { get; set; }
    }
}