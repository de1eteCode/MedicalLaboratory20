using DataModels.Abstract;

namespace DataModels.Entities
{
    public class Insurance : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Inn { get; set; }
        public string P { get; set; }
        public string Bik { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
