using Android.Content;
using Android.Widget;
using AndroidX.Work;

// Needed to keep the processor from sleeping when a message arrives
// [assembly: UsesPermission(Manifest.Permission.WakeLock)]
// [assembly: UsesPermission(Manifest.Permission.ReceiveBootCompleted)]

namespace ECSystem.Telemetry.Droid {
    public class TelemetryWorker : Worker {
        public TelemetryWorker(Context context, WorkerParameters para) : base(context, para) {
        }


        public override Result DoWork() {
            Toast.MakeText(ApplicationContext, "Received intent from work!", ToastLength.Long).Show();
            // Indicate whether the work finished successfully with the Result
            return new Result.Success();
        }
    }
}