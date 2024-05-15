namespace Berlin.Domain.Entities
{
    public class SiteUser
    {
        public User User { get; set; }
        public int UserId { get; set; }

        public Site Site { get; set; }
        public int SiteId { get; set; } 
    }
}
