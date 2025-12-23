using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CrasAPI.Models
{
    [Table("access_group")]
    public class AccessGroup
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ICollection<AccessGroupPermission> Permissions { get; set; }
        = new List<AccessGroupPermission>();
    }
}
