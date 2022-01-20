using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ECSystem.Server.Main.Data {
    public class DeviceLogs {
        [Key]
        public ulong Id { get; set; } = default!;

        public IdentityUser User { get; set; } = default!;

        public DateTime DateTime { get; set; } = default!;

        public string Log { get; set; } = default!;
    }
}