using ECSystem.Server.Main.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECSystem.Server.Main.Data {
    public class LocationDevice {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "bigint")]
        public ulong Id { get; set; } = default!;
        
        [Column(TypeName = "jsonb")]
        public Location Location { get; set; } = default!;
    }
}