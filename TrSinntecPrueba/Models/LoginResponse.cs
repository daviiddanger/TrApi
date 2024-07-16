using System;
using System.Collections.Generic;
using System.Text;

namespace TrSinntecPrueba.Models
{

    public class LoginResponse
    {
        public data data { get; set; }
        public bool success { get; set; }
        public List<string> message { get; set; }
        public int code { get; set; }
    }

    public class data
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
    }
}
