using Grpc.Core;
using Microsoft.AspNetCore.Authorization;

namespace ECSystem.Server.Main.Controllers {
    //[Authorize(Roles = "admin")]
    public class GreeterService : Greeter.GreeterBase {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger) {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context) {
            
            return Task.FromResult(new HelloReply {
                Message = $"Hello {request.Name} ; Host={context.Host} ; Peer={context.Peer}"
            });
        }
    }
}
