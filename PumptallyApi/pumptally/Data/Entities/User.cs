using System;
using System.Collections.Generic;

#nullable disable

namespace pumptally.Data.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? Dob { get; set; }
        public int? RoleId { get; set; }
    }
}
