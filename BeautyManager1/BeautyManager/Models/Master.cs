using System.ComponentModel.DataAnnotations;

namespace BeautyManager.Models
{
    public class Master
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Specialization { get; set; }

        public string Bio { get; set; }

        public string WorkDays { get; set; }

        public string WorkHours { get; set; }
    }
}