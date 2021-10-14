using DataModels.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public class Order : BaseEntity
    {
        public string Barcode { get; set; }
        [Timestamp]
        public string DateTimestamp { get; set; }

        public Patient Patient { get; set; }
        public IEnumerable<OrderService> OrderServices { get; set; }
    }
}
