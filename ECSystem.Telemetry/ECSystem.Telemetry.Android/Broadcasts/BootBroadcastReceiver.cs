using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Work;
using ECSystem.Telemetry.Droid.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Needed to keep the processor from sleeping when a message arrives
[assembly: UsesPermission(Manifest.Permission.WakeLock)]
[assembly: UsesPermission(Manifest.Permission.ReceiveBootCompleted)]

namespace ECSystem.Telemetry.Droid.Broadcasts {

    [BroadcastReceiver(Enabled = true, Exported = true, DirectBootAware = true)]
    [IntentFilter(new[] { Intent.ActionLockedBootCompleted, Intent.ActionBootCompleted, Intent.ActionPowerConnected })]
    public class BootBroadcastReceiver : BroadcastReceiver {
        public override void OnReceive(Context context, Intent intent) {
            Toast.MakeText(context, "Received intent!", ToastLength.Long).Show();
            //Scheduler.ScheduleAlarm(context);
            //WorkManager workManager = WorkManager.GetInstance(context);
            //WorkRequest reqWork = new OneTimeWorkRequest.Builder(typeof(MyWorker)).Build();
            //workManager.Enqueue(reqWork);


            var myIntent = new Intent(context, typeof(MyService));
            myIntent.AddFlags(ActivityFlags.NewTask);
            context.StartForegroundService(myIntent);
        }

        
    }

    public class MyWorker : Worker {
        public MyWorker(Context context, WorkerParameters para) : base(context, para) {
        }


        public override Result DoWork() {
            Toast.MakeText(ApplicationContext, "Received intent from work!", ToastLength.Long).Show();
            // Indicate whether the work finished successfully with the Result
            return new Result.Success();
        }
    }
}