using System;

namespace Ketan.Square2.Service.Authentication.Model
{
    public class Role
    {
        public Guid _id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationUser { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ModificationUser { get; set; }

    }
}
