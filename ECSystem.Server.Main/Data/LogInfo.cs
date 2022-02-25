using ECSystem.Server.Main.Models;

namespace ECSystem.Server.Main.Data {
    //Default version for logs
    public record class LogInfo {
        public Location GeoLocation { get; set; }
        public string ConnectedWifi { get; set; } = string.Empty;
        public List<string> ListWifi { get; set; } = default!;
    }
}
