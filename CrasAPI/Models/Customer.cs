using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrasAPI.Models
{
    [Table("customers")]
    public class Customer
    {
        [Key]    
        public int Id { get; set; }
        [Required]
        public string Code { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string EIN { get; set; } = null!;
        public string? Email { get; set; }
        public string? Observation { get; set; }
    }
}
