using DataModels.Abstract;

namespace DataModels
{
    public class Safety : BaseEntity
    {
        public string IpAddress { get; set; }
        public string UA { get; set; }

        public Patient Patient { get; set; }
    }
}
