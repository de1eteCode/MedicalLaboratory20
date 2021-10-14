using DataModels.Abstract;

namespace DataModels
{
    public class Social : BaseEntity
    {
        public string SocialSecNumber { get; set; }
        public string EIN { get; set; }
        public string SocialType { get; set; }

        public Patient Patient { get; set; }
    }
}