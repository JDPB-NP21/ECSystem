namespace ECSystem.Server.Main.Models {
    public record class TelemetryDto {
        public ulong DeviceId { get; set; } = default!;

        public string UserName { get; set; } = default!;
        public string Logs { get; set; } = default!;

    }
}
