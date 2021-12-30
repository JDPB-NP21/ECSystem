using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Content;
using ECSystem.Telemetry.Droid.Broadcasts;
using Android.Widget;
using ECSystem.Telemetry.Droid.Services;

namespace ECSystem.Telemetry.Droid {
    [Activity(Label = "ECSystem.Telemetry", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            Toast.MakeText(ApplicationContext, "Hi", ToastLength.Short).Show();
            //var myIntent = new Intent(ApplicationContext, typeof(MyService));
            //myIntent.AddFlags(ActivityFlags.NewTask);
            //ApplicationContext.StartForegroundService(myIntent);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults) {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}