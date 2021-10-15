using DataModels.Abstract;
using System;

namespace DataModels.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Ip { get; set; }
        public DateTime? LastEnter { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}