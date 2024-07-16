using SQLite;

namespace TrSinntecPrueba.Models
{
     public class Frameworks
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public int language_id { get; set; }
    }
}
