using DataModels.Abstract;
using System;
using System.Collections.Generic;

namespace DataModels.Entities
{
    public class Patient : BaseEntity
    {
        public Patient()
        {
            Orders = new HashSet<Order>();
        }

        public string Fullname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Guid Guid { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PassportS { get; set; }
        public string PassportN { get; set; }
        public byte[] BirthdateTimestamp { get; set; }
        public string Country { get; set; }

        public virtual Insurance Insurance { get; set; }
        public virtual Safety Safety { get; set; }
        public virtual Social Social { get; set; }
        public virtual IEnumerable<Order> Orders { get; set; }
    }
}