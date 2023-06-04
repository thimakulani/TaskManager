using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidHUD;
using AndroidX.AppCompat.App;
using Google.Android.Material.Button;
using Google.Android.Material.TextField;
using Google.Android.Material.TextView;
using ID.IonBit.IonAlertLib;
using Plugin.FirebaseAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Dialogs;

namespace TaskManager.Activities
{
    [Activity(Label = "Login")]
    public class Login : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.login);
            ConnectViews();
        }
        private MaterialButton BtnLogin;
        private MaterialButton BtnSignUp;

        private MaterialTextView TxtForgotPassword;
        private TextInputEditText InputEmail;
        private TextInputEditText InputPassword;
        private void ConnectViews()
        {
            BtnLogin = FindViewById<MaterialButton>(Resource.Id.BtnLogin);
            TxtForgotPassword = FindViewById<MaterialTextView>(Resource.Id.txt_forgot_password);
            InputEmail = FindViewById<TextInputEditText>(Resource.Id.InputEmail);
            InputPassword = FindViewById<TextInputEditText>(Resource.Id.InputPassword);
            BtnSignUp = FindViewById<MaterialButton>(Resource.Id.BtnSignup);

            BtnLogin.Click += async delegate
            {
                if (string.IsNullOrEmpty(InputEmail.Text) && string.IsNullOrWhiteSpace(InputEmail.Text))
                {
                    Toast.MakeText(this, "Please provide your email", ToastLength.Long).Show();
                    return;
                }
                if (string.IsNullOrEmpty(InputPassword.Text) && string.IsNullOrWhiteSpace(InputPassword.Text))
                {
                    Toast.MakeText(this, "Please provide password", ToastLength.Long).Show();
                    return;
                }
                var loadingDialog = new IonAlert(this, IonAlert.ProgressType);
                loadingDialog.SetSpinKit("DoubleBounce")
                    .ShowCancelButton(false)
                    .Show();
                try
                {
                    await CrossFirebaseAuth
                       .Current
                       .Instance
                       .SignInWithEmailAndPasswordAsync(InputEmail.Text.Trim(), InputPassword.Text.Trim());
                }
                catch (FirebaseAuthException ex)
                {
                    AndHUD.Shared.ShowError(this, ex.Message, MaskType.Clear, TimeSpan.FromSeconds(3));
                }
                finally { loadingDialog.Dismiss(); }
            };
            BtnSignUp.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Register));
                StartActivity(intent);
                OverridePendingTransition(Resource.Animation.Side_in_right, Resource.Animation.Side_out_left);
            };
            TxtForgotPassword.Click += delegate
            {
                new ForgotPasswordDlgFragment().Show(SupportFragmentManager.BeginTransaction(), "");
            };
        }
    }
}