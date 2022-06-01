using System;
using System.Collections.Generic;


namespace CarpoolApp.Models
{
    public partial class KidsInCarpool
    {
        public int KidId { get; set; }
        public int CarpoolId { get; set; }
        public int StatusId { get; set; }
        public bool KidOnBoard { get; set; }

        public virtual Carpool Carpool { get; set; }
        public virtual Kid Kid { get; set; }
        public virtual RequestCarpoolStatus Status { get; set; }
    }
}
