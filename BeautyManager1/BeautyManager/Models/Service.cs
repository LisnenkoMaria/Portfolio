using System.ComponentModel.DataAnnotations;

namespace BeautyManager.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Duration { get; set; }

        public string Category { get; set; }
    }
}