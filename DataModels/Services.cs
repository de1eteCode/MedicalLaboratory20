using DataModels.Abstract;
using System.Collections.Generic;

namespace DataModels
{
    public class Services : BaseEntity
    {
        public string Service { get; set; }
        public int Price { get; set; }

        public IEnumerable<User> Users { get; set; }
    }
}
