using System;
using System.Collections.Generic;
using System.Text;

namespace CarpoolApp.DTO
{
    public class Street
    {
        public int id { get; set; }
        public int city_symbol { get; set; }
        public string city_name { get; set; }
        public int street_symbol { get; set; }
        public string street_name { get; set; }
    }
}
