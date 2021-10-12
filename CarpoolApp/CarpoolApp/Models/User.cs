using System;
using System.Collections.Generic;
using System.Text;

namespace CarpoolApp.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserPswd { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNum { get; set; }
        public string Photo { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string Street { get; set; }
        public string HouseNum { get; set; }

        public virtual Adult Adult { get; set; }
        public virtual Kid Kid { get; set; }
    }
}
