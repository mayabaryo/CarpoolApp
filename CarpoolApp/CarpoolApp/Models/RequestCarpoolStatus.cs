using System;
using System.Collections.Generic;


namespace CarpoolApp.Models
{
    public partial class RequestCarpoolStatus
    {
        public RequestCarpoolStatus()
        {
            RequestToJoinCarpools = new HashSet<RequestToJoinCarpool>();
        }

        public int RequestId { get; set; }
        public string RequestName { get; set; }

        public virtual ICollection<RequestToJoinCarpool> RequestToJoinCarpools { get; set; }
    }
}
