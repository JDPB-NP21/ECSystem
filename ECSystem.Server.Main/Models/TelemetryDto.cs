namespace ECSystem.Server.Main.Models {
    public record class TelemetryDto {
        public int DeviceId { get; set; } = default!;
        public string Logs { get; set; } = default!;

    }
}
