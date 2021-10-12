using System;
using System.Collections.Generic;

//#nullable disable

namespace CarpoolApp.Models
{
    public partial class KidsInCarpool
    {
        public int KidId { get; set; }
        public int CarpoolId { get; set; }

        public virtual Carpool Carpool { get; set; }
        public virtual Kid Kid { get; set; }
    }
}
