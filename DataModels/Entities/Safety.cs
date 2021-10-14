using DataModels.Abstract;

namespace DataModels.Entities
{
    public class Safety : BaseEntity
    {
        public string IpAddress { get; set; }
        public string UA { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
