using DataModels.Abstract;

namespace DataModels
{
    public class Insurance : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string INN { get; set; }
        public string P { get; set; }
        public string BIK { get; set; }

        public Patient Patient { get; set; }
    }
}
