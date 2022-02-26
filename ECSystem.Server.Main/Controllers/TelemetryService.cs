﻿using ECSystem.Server.Main.Data;
using ECSystem.Server.Main.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace ECSystem.Server.Main.Controllers {

    [Authorize(AuthenticationSchemes = "BasicAuth", Roles = "TelemetryClient")]
    public class TelemetryService : Telemetry.TelemetryBase {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;

        public TelemetryService(ApplicationDbContext dbContext,
                UserManager<IdentityUser> userManager) : base() {

            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async override Task<TelemetryReplay> SendTelemetry(TelemetryData request, ServerCallContext context) {

            var reqLocation = request.Location;
            var log = new LogInfo() {
                ConnectedWifi = request.ConnectedWifi.Id,
                Location = new Location(reqLocation.Latitude, reqLocation.Longitude, reqLocation.Height),
                ListWifi = request.ListWifi.Select(s => s.Id).ToList(),
            };

            var currentUserName = context.GetHttpContext().User.Identity?.Name;
            var currentUser = await userManager.FindByNameAsync(currentUserName);

            var deviceLog = new DeviceLogs() {
                User = currentUser,
                FieldDateCreated = DateTime.UtcNow,
                LogVersion = 1,
                Log = log,
            };

            await dbContext.DeviceLogs.AddAsync(deviceLog);
            await dbContext.SaveChangesAsync();

            return new TelemetryReplay() {
                Message = "Done"
            };
        }
    }
}
