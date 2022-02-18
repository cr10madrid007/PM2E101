using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace PM2E101.Models
{
    public class constructor
    {
        [PrimaryKey, AutoIncrement]
        public int codigo { get; set; }

        [MaxLength(100)]
        public string latitud { get; set; }
        [MaxLength(100)]
        public string longitud { get; set; }
        
        
        [MaxLength(250)]
        public string descripcion { get; set; }


        [MaxLength(250)]
        public string imgRuta { get; set; }
    }
}
