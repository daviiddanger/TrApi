using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrSinntecPrueba.Models
{
    public class ApiResponse
    {
        public Data data { get; set; }
        public bool success { get; set; }
        public string[] message { get; set; }
        public int code { get; set; }
    }

    public class Data
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int platform_id { get; set; }
        public Platform platform { get; set; }
        public characteristic characteristic { get; set; }
        public location location { get; set; }
    }

    public class characteristic
    {
        public int width { get; set; }
        public int height { get; set; }
        public int large { get; set; }
        public double weight { get; set; }
        public int is_fragile { get; set; }
    }

    public class location
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
    }

    public class Platform
    {
        public int id { get; set; }
        public string name { get; set; }
        public string key { get; set; }
    }

}
