using Grpc.Core;
using Microsoft.AspNetCore.Authorization;

namespace ECSystem.Server.Main.Controllers {

    [Authorize(AuthenticationSchemes = "BasicAuth", Roles = "Administrator,Victim")]
    public class TelemetryService : Telemetry.TelemetryBase {
        public async override Task<TelemetryReplay> SendTelemetry(TelemetryData request, ServerCallContext context) {
            
            return new TelemetryReplay() {
                Message = "None"
            };
        }
    }
}
