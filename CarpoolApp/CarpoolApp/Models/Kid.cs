using System;
using System.Collections.Generic;


namespace CarpoolApp.Models
{
    public partial class Kid
    {
        public Kid()
        {
            KidsInActivities = new HashSet<KidsInActivity>();
            KidsInCarpools = new HashSet<KidsInCarpool>();
            KidsOfAdults = new HashSet<KidsOfAdult>();
            RequestToJoinCarpools = new HashSet<RequestToJoinCarpool>();
        }

        public int Id { get; set; }

        public virtual User IdNavigation { get; set; }
        public virtual ICollection<KidsInActivity> KidsInActivities { get; set; }
        public virtual ICollection<KidsInCarpool> KidsInCarpools { get; set; }
        public virtual ICollection<KidsOfAdult> KidsOfAdults { get; set; }
        public virtual ICollection<RequestToJoinCarpool> RequestToJoinCarpools { get; set; }
    }
}
