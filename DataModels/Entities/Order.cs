using DataModels.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModels.Entities
{
    public class Order : BaseEntity
    {
        public string Barcode { get; set; }
        [Timestamp]
        public string DateTimestamp { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual IEnumerable<OrderService> OrderServices { get; set; }
    }
}
