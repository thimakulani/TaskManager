using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidHUD;
using AndroidX.Fragment.App;
using Google.Android.Material.Button;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.TextField;
using ID.IonBit.IonAlertLib;
using Plugin.FirebaseAuth;
using System;
namespace TaskManager.Dialogs
{

    public class ForgotPasswordDlgFragment : DialogFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        Context context;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.reset_password_dialog, container, false);
            context = view.Context;
            ConnectViews(view);
            return view;
        }

        private void ConnectViews(View view)
        {
            var InputEmail = view.FindViewById<TextInputEditText>(Resource.Id.ResetInputEmail);
            var BtnReset = view.FindViewById<MaterialButton>(Resource.Id.BtnReset);
            var FabClose = view.FindViewById<FloatingActionButton>(Resource.Id.FabCloseResetDialog);
            var loadingDialog = new IonAlert(view.Context, IonAlert.ProgressType);

            BtnReset.Click += async (s, e) =>
            {
                loadingDialog.SetSpinKit("FoldingCube")
                    .SetSpinColor("#008D91")
                    .ShowCancelButton(false)
                    .Show();
                if (string.IsNullOrEmpty(InputEmail.Text))
                {
                    InputEmail.Error = "provide your email";
                    return;
                }
                try
                {
                    await CrossFirebaseAuth
                        .Current
                        .Instance
                        .SendPasswordResetEmailAsync(InputEmail.Text.Trim());
                    AndHUD.Shared.ShowError(context, $"Password Reset Link Has Been Sent To Your Email", MaskType.Black, TimeSpan.FromSeconds(2));
                }
                catch (FirebaseAuthException ex)
                {
                    AndHUD.Shared.ShowError(context, $"{ex.Message}", MaskType.Black, TimeSpan.FromSeconds(2));
                }
                finally
                {
                    loadingDialog.Dismiss();
                }
            };
            FabClose.Click += (s, e) =>
            {
                Dismiss();
            };


        }
        public override void OnStart()
        {
            base.OnStart();
            Dialog.Window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
        }
    }
}
