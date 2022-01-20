using ECSystem.Server.Main.Models;
using System.ComponentModel.DataAnnotations;

namespace ECSystem.Server.Main.Data {
    public class LocationDevice {
        [Key]
        public ulong Id { get; set; } = default!;
        public LocationData Location { get; set; } = default!;
    }
}