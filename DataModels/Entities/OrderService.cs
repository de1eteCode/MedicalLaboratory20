using DataModels.Abstract;
using System.ComponentModel.DataAnnotations;

namespace DataModels.Entities
{
    public class OrderService : BaseEntity
    {
        public float Result { get; set; }
        [Timestamp]
        public string FinishedTimestamp { get; set; }
        public bool Accepted { get; set; }
        public string Status { get; set; }
        public string Analyzer { get; set; }

        public virtual User User { get; set; }
        public virtual Service Service { get; set; }
        public virtual Order Order { get; set; }
    }
}
