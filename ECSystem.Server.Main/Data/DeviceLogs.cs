using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECSystem.Server.Main.Data {
    public class DeviceLogs {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "bigint")]
        public ulong Id { get; set; } = default!;

        public IdentityUser User { get; set; } = default!;


        public DateTime FieldDateCreated { get; set; } = default!;

        public uint LogVersion { get; set; } = default!;

        [Column(TypeName = "jsonb")]
        public LogInfo Log { get; set; } = default!;
        //public JsonDocument Log
    }
}