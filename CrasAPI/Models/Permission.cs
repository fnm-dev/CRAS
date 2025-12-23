using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CrasAPI.Models
{
    [Table("permission")]
    public class Permission
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; } = null!;
        public string Description { get; set; } = null!;

        [JsonIgnore]
        public int? ParentPermissionId { get; set; }
        public Permission? ParentPermission { get; set; }
    }
}
