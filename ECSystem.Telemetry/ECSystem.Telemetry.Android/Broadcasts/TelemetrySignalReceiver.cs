using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Android.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroidX.Work;

namespace ECSystem.Telemetry.Droid.Broadcasts {
    [BroadcastReceiver(Enabled = false)]
    public class TelemetrySignalReceiver : BroadcastReceiver {
        public const int RequestCode = 456;
        public override void OnReceive(Context context, Intent intent) {
            WorkManager workManager = WorkManager.GetInstance(context);
            //workManager.EnqueueUniquePeriodicWork("TelemetryTag", ExistingPeriodicWorkPolicy.Keep, );
        }
    }
}