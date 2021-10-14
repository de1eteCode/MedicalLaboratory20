using DataModels.Abstract;
using System;
using System.Collections.Generic;

namespace DataModels
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Ip { get; set; }
        public DateTime LastEnter { get; set; }
        public int RoleId { get; set; }

        public IEnumerable<Services> Services { get; set; }
        public IEnumerable<OrderService> OrderServices { get; set; }
    }
}