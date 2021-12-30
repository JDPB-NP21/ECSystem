using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECSystem.Telemetry.Droid.Services {


    [Service(Exported = true)]
    internal class MyService : Service {



        public override void OnCreate() {
            if(false) {
                NotificationCompat.Builder mBuilder = new NotificationCompat.Builder(this);
                mBuilder.SetSmallIcon(Resource.Drawable.abc_btn_radio_material);
                mBuilder.SetContentTitle("Notification Alert, Click Me!");
                mBuilder.SetContentText("Hi, This is Android Notification Detail!");
                NotificationManager mNotificationManager = (NotificationManager)GetSystemService(Context.NotificationService);

                // notificationID allows you to update the notification later on.
                mNotificationManager.Notify(100, mBuilder.Build());
                StartForeground(100, mBuilder.Notification);


                IntentFilter filter1 = new IntentFilter();
                filter1.AddAction(Intent.ActionPowerConnected);
                RegisterReceiver(myBroadcastReceiver, filter1);
            }
        }

        // This is any integer value unique to the application.
        public const int SERVICE_RUNNING_NOTIFICATION_ID = 1000;

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId) {
            String NOTIFICATION_CHANNEL_ID = "ECSystem.Telemetry.Android.FService";
            String channelName = "test app service";
            // Code not directly related to publishing the notification has been omitted for clarity.
            // Normally, this method would hold the code to be run when the service is started.

            NotificationChannel chan = new NotificationChannel(NOTIFICATION_CHANNEL_ID, channelName, NotificationImportance.None) {
                LockscreenVisibility = NotificationVisibility.Private
            };
            NotificationManager manager = (NotificationManager)GetSystemService(Context.NotificationService);
            manager.CreateNotificationChannel(chan);




            var notification = new Notification.Builder(this, NOTIFICATION_CHANNEL_ID)
                .SetContentTitle("Notification Alert, Click Me!")
                .SetContentText("Notification Detail!")
                .SetSmallIcon(Resource.Drawable.ic_arrow_down_24dp)
                .SetTicker("TickerText")
                //.SetOngoing(true)
                //.SetCategory(Notification.CategoryService)
                .Build();


            // Enlist this instance of the service as a foreground service
            StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, notification);
            return StartCommandResult.NotSticky;
        }

        public override void OnDestroy() {
            base.OnDestroy();
            //UnregisterReceiver(myBroadcastReceiver);
        }

        BroadcastReceiver myBroadcastReceiver = new MyBroadcastReceiver();

        internal class MyBroadcastReceiver : BroadcastReceiver {
            public override void OnReceive(Context context, Intent intent) {
                Toast.MakeText(context, "Received in Service Reciver!", ToastLength.Long).Show();
            }
        }

        public override IBinder OnBind(Intent intent) {
            return null;
        }
    }
}