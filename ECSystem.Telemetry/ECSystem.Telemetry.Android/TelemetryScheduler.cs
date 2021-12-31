using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Work;
using ECSystem.Telemetry.Droid.Broadcasts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECSystem.Telemetry.Droid {
    internal class TelemetryScheduler {
        public const String TAG = "ECSystem.Telemetry";
        // Setup a recurring alarm every half hour
        public static void OldScheduleAlarm(Context context) {
            // Construct an intent that will execute the AlarmReceiver
            Intent intent = new Intent(context, typeof(TelemetrySignalReceiver));
            PendingIntent pIntent = PendingIntent.GetBroadcast(context, TelemetrySignalReceiver.RequestCode, intent, PendingIntentFlags.UpdateCurrent); // PendingIntent.FLAG_UPDATE_CURRENT);
            // Setup periodic alarm every every half hour from this point onwards
            long firstMillis = DateTime.Now.Ticks; // alarm is set right away
            AlarmManager alarm = (AlarmManager)context.GetSystemService(Context.AlarmService);

            // First parameter is the type: ELAPSED_REALTIME, ELAPSED_REALTIME_WAKEUP, RTC_WAKEUP
            // Interval can be INTERVAL_FIFTEEN_MINUTES, INTERVAL_HALF_HOUR, INTERVAL_HOUR, INTERVAL_DAY
            alarm.SetInexactRepeating(AlarmType.RtcWakeup, firstMillis, AlarmManager.IntervalFifteenMinutes, pIntent);
        }

        public static void ScheduleAlarm(Context context) {
            WorkManager workManager = WorkManager.GetInstance(context);

            var reqWork = new PeriodicWorkRequest.Builder(typeof(TelemetryWorker), TimeSpan.FromMinutes(15))
                           .Build();
            workManager.EnqueueUniquePeriodicWork(TAG, ExistingPeriodicWorkPolicy.Keep, reqWork);
        }
    }
}