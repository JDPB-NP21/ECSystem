using ECSystem.Server.Main.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECSystem.Server.Main.Data {
    public class LocationDevice {
        [Key]
        public ulong Id { get; set; } = default!;
        
        [Column(TypeName = "json")]
        public LocationData Location { get; set; } = default!;
    }
}