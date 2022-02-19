using System;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Content;
using ECSystem.Telemetry.Droid.Broadcasts;
using Android.Widget;
using ECSystem.Telemetry.Droid.Services;
using Android;
using Xamarin.Essentials;

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
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                if (CheckSelfPermission(Manifest.Permission.AccessCoarseLocation) != Android.Content.PM.Permission.Granted)
                {
                    RequestPermissions(new string[] { Manifest.Permission.AccessCoarseLocation }, 87);
                }
            }
            
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults) {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnResume()
        {
            base.OnResume();
            if(Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                if(CheckSelfPermission(Manifest.Permission.AccessCoarseLocation) != Android.Content.PM.Permission.Granted)
                {
                    RequestPermissions(new string[] { Manifest.Permission.AccessCoarseLocation }, 87);
                }
            }
        }

        CancellationTokenSource cts;
        async Task GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium,
                    TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longtitude: {location.Longitude}, " +
                                      $"Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {

            }
            catch (FeatureNotEnabledException fneEx)
            {

            }
            catch (PermissionException pEx)
            {

            }
            catch (Exception ex)
            {
                
            }
        }
        
    }
}