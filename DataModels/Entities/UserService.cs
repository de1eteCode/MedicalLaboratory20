namespace DataModels.Entities
{
    public class UserService
    {
        public int CodeUser { get; set; }
        public int CodeService { get; set; }

        public virtual Service Service { get; set; }
        public virtual User User { get; set; }
    }
}
