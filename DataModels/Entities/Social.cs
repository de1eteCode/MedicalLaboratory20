using DataModels.Abstract;

namespace DataModels.Entities
{
    public class Social : BaseEntity
    {
        public string SocialSecNumber { get; set; }
        public string EIN { get; set; }
        public string SocialType { get; set; }

        public virtual Patient Patient { get; set; }
    }
}