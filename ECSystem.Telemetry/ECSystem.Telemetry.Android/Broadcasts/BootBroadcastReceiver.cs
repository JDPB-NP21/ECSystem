using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ECSystem.Telemetry.Droid.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Needed to keep the processor from sleeping when a message arrives
// [assembly: UsesPermission(Manifest.Permission.WakeLock)]
// [assembly: UsesPermission(Manifest.Permission.ReceiveBootCompleted)]

namespace ECSystem.Telemetry.Droid.Broadcasts {

    [BroadcastReceiver(Enabled = true, Exported = true, DirectBootAware = true)]
    [IntentFilter(new[] { Intent.ActionLockedBootCompleted, Intent.ActionBootCompleted, Intent.ActionPowerConnected })]
    public class BootBroadcastReceiver : BroadcastReceiver {

        public override void OnReceive(Context context, Intent intent) {
            Toast.MakeText(context, "Received intent!", ToastLength.Long).Show();
            TelemetryScheduler.ScheduleAlarm(context);
        }


        void StartTelemetryService(Context context) {
            var myIntent = new Intent(context, typeof(TelemetryService));
            myIntent.AddFlags(ActivityFlags.NewTask);
            context.StartForegroundService(myIntent);
        }
    }
}