using System.ComponentModel.DataAnnotations;

namespace PrimeraApiRest.Models
{
    public class AtlasPhoto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Photographer { get; set; }
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }

    }
}
