using System;
using System.Collections.Generic;
using System.Text;

namespace Ketan.Square2.Service.Authentication.Model
{
    public class User
    {
        public Guid _id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationUser { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ModificationUser { get; set; }
    }
}
