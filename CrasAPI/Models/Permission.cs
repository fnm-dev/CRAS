using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int? ParentPermissionId { get; set; }
        public Permission? ParentPermission { get; set; }
    }
}
