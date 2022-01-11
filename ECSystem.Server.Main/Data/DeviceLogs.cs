using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ECSystem.Server.Main.Data {
    public class DeviceLogs {
        [Key]
        public int Id { get; set; } = default!;

        public IdentityUser User { get; set; } = default!;

        public string Log { get; set; } = default!;
    }
}