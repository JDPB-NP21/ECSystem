namespace ECSystem.Server.Main.Models {
    public record struct Location(double Latitude, double Longitude, double Height);

    public class LocationData {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Height { get; set; }
    }
}
