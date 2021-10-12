using System;
using System.Collections.Generic;

//#nullable disable

namespace CarpoolApp.Models
{
    public partial class KidsOfAdult
    {
        public int AdultId { get; set; }
        public int KidId { get; set; }

        public virtual Adult Adult { get; set; }
        public virtual Kid Kid { get; set; }
    }
}
