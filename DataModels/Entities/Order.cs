using DataModels.Abstract;

namespace DataModels.Entities
{
    public class Order : BaseEntity
    {
        public int PatientId { get; set; }
        public string Barcode { get; set; }
        public byte[] DateTimestamp { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
