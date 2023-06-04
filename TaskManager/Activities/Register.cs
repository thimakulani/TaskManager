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
using ID.IonBit.IonAlertLib;
using Plugin.CloudFirestore;
using Plugin.FirebaseAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Models;

namespace TaskManager.Activities
{
    [Activity(Label = "Register")]
    public class Register : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.sign_up);
        }
        private void ConnectViews()
        {
            var InputName = FindViewById<TextInputEditText>(Resource.Id.InputName);
            var InputSurname = FindViewById<TextInputEditText>(Resource.Id.InputLastName);
            var InputContact = FindViewById<TextInputEditText>(Resource.Id.InputContact);
            var InputEmail = FindViewById<TextInputEditText>(Resource.Id.InputEmail);
            var InputPassword = FindViewById<TextInputEditText>(Resource.Id.InputPassword);
            var BtnSignInBtn = FindViewById<MaterialButton>(Resource.Id.BtnSignInBtn);
            //var InputName = FindViewById<MaterialButton>(Resource.Id.InputName);
            BtnSignInBtn.Click += async delegate
            {
                if (string.IsNullOrEmpty(InputName.Text))
                {
                    InputName.Error = "provide your name";
                    return;
                }
                if (string.IsNullOrEmpty(InputSurname.Text))
                {
                    InputSurname.Error = "provide your surname name";
                    return;
                }
                if (string.IsNullOrEmpty(InputContact.Text))
                {
                    InputContact.Error = "provide your phone number";
                    return;
                }
                if (string.IsNullOrEmpty(InputEmail.Text))
                {
                    InputEmail.Error = "provide your email";
                    return;
                }
                if (string.IsNullOrEmpty(InputPassword.Text))
                {
                    InputPassword.Error = "provide your password";
                    return;
                }

                Users user_ = new Users()
                {
                    Email = InputEmail.Text.Trim(),
                    LastName = InputSurname.Text.Trim(),
                    Name = InputName.Text.Trim(),
                    Phone = InputContact.Text.Trim(),
                    ImgUrl = null,
                };

                var loadingDialog = new IonAlert(this, IonAlert.ProgressType);
                loadingDialog.SetSpinKit("DoubleBounce")
                    .ShowCancelButton(false)
                    .Show();
                try
                {
                    var user = await CrossFirebaseAuth.Current
                       .Instance
                       .CreateUserWithEmailAndPasswordAsync(InputEmail.Text.Trim(), InputPassword.Text.Trim());
                    if (user.User != null)
                    {
                        await CrossCloudFirestore
                            .Current
                            .Instance
                            .Collection(nameof(Users))
                            .Document(user.User.Uid)
                            .SetAsync(user_);
                        AndHUD.Shared.ShowError(this, "Your profile has been successfully created", MaskType.Clear, TimeSpan.FromSeconds(3));
                    }
                }
                catch (FirebaseAuthException ex)
                {
                    AndHUD.Shared.ShowError(this, ex.Message, MaskType.Clear, TimeSpan.FromSeconds(3));
                }
                finally { loadingDialog.Dismiss(); }
            };
        }
    }
}