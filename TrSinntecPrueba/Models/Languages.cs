using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrSinntecPrueba.Models
{
    public class Languages
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
    }
}
