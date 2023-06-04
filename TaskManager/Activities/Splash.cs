using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Plugin.FirebaseAuth;
using TaskManager.Activities;

namespace TaskManager
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class Splash : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            CrossFirebaseAuth.Current.Instance
                     .AuthState += (s, e) =>
                     {
                         if (e.Auth.CurrentUser != null)
                         {
                             Intent intent = new Intent(Application.Context, typeof(Home));
                             StartActivity(intent);
                             OverridePendingTransition(Resource.Animation.Side_in_right, Resource.Animation.Side_out_left);
                         }
                         else
                         {
                             Intent intent = new Intent(Application.Context, typeof(Login));
                             StartActivity(intent);
                             OverridePendingTransition(Resource.Animation.Side_in_right, Resource.Animation.Side_out_left);
                         }
                     };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}