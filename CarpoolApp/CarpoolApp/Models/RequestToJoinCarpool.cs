using System;
using System.Collections.Generic;

//#nullable disable

namespace CarpoolApp.Models
{
    public partial class RequestToJoinCarpool
    {
        public int KidId { get; set; }
        public int CarpoolId { get; set; }
        public int RequestStatusId { get; set; }

        public virtual Carpool Carpool { get; set; }
        public virtual Kid Kid { get; set; }
        public virtual RequestCarpoolStatus RequestStatus { get; set; }
    }
}
