using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CrasAPI.Models
{
    [Table("access_group_permission")]
    public class AccessGroupPermission
    {
        [JsonIgnore]
        public int AccessGroupId { get; set; }
        [JsonIgnore]
        public AccessGroup AccessGroup { get; set; } = null!;

        [JsonIgnore]
        public int PermissionId { get; set; }
        public Permission Permission { get; set; } = null!;
    }
}
