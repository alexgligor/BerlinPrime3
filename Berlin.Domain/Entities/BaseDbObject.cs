using System.ComponentModel.DataAnnotations;

namespace Berlin.Domain.Entities
{
    public class BaseDbObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

}
