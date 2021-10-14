using DataModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public class Patient : BaseEntity
    {
        public string Fullname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Guid Guid { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Passport_s { get; set; }
        public string Passport_n { get; set; }
        [Timestamp]
        public string BrithdateTimestamp { get; set; }
        public string Coutry { get; set; }

        public Insurance Insurance { get; set; }
        public Safety Safety { get; set; }
        public Social Social { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}