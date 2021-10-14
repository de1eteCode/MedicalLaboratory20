using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public class OrderService
    {
        public float Result { get; set; }
        [Timestamp]
        public string FinishedTimestamp { get; set; }
        public bool Accepted { get; set; }
        public string Status { get; set; }
        public string Analyzer { get; set; }

        public Order Order { get; set; }
        public Services Services { get; set; }
        public User User { get; set; }

    }
}
