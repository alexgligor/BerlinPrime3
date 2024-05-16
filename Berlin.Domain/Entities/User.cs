namespace Berlin.Domain.Entities
{
    public class User:BaseDbObject
    {     
        public string? ImageUrl { get; set; }

        public ICollection<SiteUser> Sites { get; set; } = new List<SiteUser>();

        public float? Target { get; set; } = 25000;

    }


}
