using ECSystem.Server.Main.Models;
using System.ComponentModel.DataAnnotations;

namespace ECSystem.Server.Main.Data {
    public class LocationDevice {
        [Key]
        public int Id { get; set; } = default!;
        //public Location Location { get; set; } = default!;
    }
}