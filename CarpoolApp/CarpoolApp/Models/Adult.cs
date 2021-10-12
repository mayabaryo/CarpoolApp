using System;
using System.Collections.Generic;

//#nullable disable

namespace CarpoolApp.Models
{
    public partial class Adult
    {
        public Adult()
        {
            Activities = new HashSet<Activity>();
            Carpools = new HashSet<Carpool>();
        }

        public int Id { get; set; }

        public virtual User IdNavigation { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Carpool> Carpools { get; set; }
    }
}
