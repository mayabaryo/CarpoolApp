using System;
using System.Collections.Generic;

//#nullable disable

namespace CarpoolApp.Models
{
    public partial class Activity
    {
        public Activity()
        {
            Carpools = new HashSet<Carpool>();
        }

        public int Id { get; set; }
        public string ActivityName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string Street { get; set; }
        public int HouseNum { get; set; }
        public bool Recurring { get; set; }
        public string EntryCode { get; set; }
        public int AdultId { get; set; }

        public virtual Adult Adult { get; set; }
        public virtual ICollection<Carpool> Carpools { get; set; }
    }
}
