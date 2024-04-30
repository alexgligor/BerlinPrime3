
namespace Berlin.Domain.Entities
{
    public class Device : BaseDbObject
    {
        public int SiteId { get; set; }

        public Site Site { get; set; }

    }
}
