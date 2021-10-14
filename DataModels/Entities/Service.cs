using DataModels.Abstract;
using System.Collections.Generic;

namespace DataModels.Entities
{
    public class Service : BaseEntity
    {
        public string ServiceName { get; set; }
        public int Price { get; set; }

        public virtual IEnumerable<User> Users { get; set; }
        public virtual IEnumerable<OrderService> OrderServices { get; set; }
    }
}
