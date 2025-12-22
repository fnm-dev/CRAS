using System.ComponentModel.DataAnnotations.Schema;

namespace CrasAPI.Models
{
    [Table("access_group_permission")]
    public class AccessGroupPermission
    {
        public int AccessGroupId { get; set; }
        public AccessGroup AccessGroup { get; set; } = null!;

        public int PermissionId { get; set; }
        public Permission Permission { get; set; } = null!;
    }
}
