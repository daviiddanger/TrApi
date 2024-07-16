using System;
using System.Collections.Generic;
using System.Text;

namespace TrSinntecPrueba.Models
{
    public class RegisterRequest
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string password_confirmation { get; set; }
    }
}
