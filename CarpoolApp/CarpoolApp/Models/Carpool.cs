using System;
using System.Collections.Generic;

//#nullable disable

namespace CarpoolApp.Models
{
    public partial class Carpool
    {
        public Carpool()
        {
            KidsInCarpools = new HashSet<KidsInCarpool>();
        }

        public int Id { get; set; }
        public int AdultId { get; set; }
        public DateTime CarpoolTime { get; set; }
        public int Seats { get; set; }
        public int CarpoolStatusId { get; set; }
        public int ActivityId { get; set; }

        public virtual Activity Activity { get; set; }
        public virtual Adult Adult { get; set; }
        public virtual CarpoolStatus CarpoolStatus { get; set; }
        public virtual ICollection<KidsInCarpool> KidsInCarpools { get; set; }
    }
}
