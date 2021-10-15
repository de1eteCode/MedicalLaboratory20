namespace DataModels.Entities
{
    public class OrderService
    {
        public int OrderId { get; set; }
        public int ServiceCode { get; set; }
        public double? Result { get; set; }
        public byte[] FinishedTimestamp { get; set; }
        public bool? Accepted { get; set; }
        public string Status { get; set; }
        public string Analyzer { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual Service Service { get; set; }
        public virtual Order Order { get; set; }
    }
}
