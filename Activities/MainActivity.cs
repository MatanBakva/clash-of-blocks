using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Threading.Tasks;
using System.Collections.Generic;
using AndroidX.AppCompat.App;
using Clash_of_blocks;
using SQLite;
using Microsoft.Data.Sqlite;

namespace Clash_Of_Blocks.Droid
{
    [Activity(Label = "Clash Of Blocks", Icon = "@drawable/mainpic1", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = Android.Content.PM.ScreenOrientation.Locked)]
    public class MainActivity : AppCompatActivity
    {
        Button btnLogin;
        Button btnRegister;
        ISharedPreferences sp;
        ProgressDialog pd;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);

            btnLogin.Click += BtnLogin_Click;
            btnRegister.Click += BtnRegister_Click;

            FireBaseHelper.Initialize(this);

            SqlHelper.GetConnection().CreateTable<User>();
            SqlHelper.GetConnection().CreateTable<Record>();
            SqlHelper.GetConnection().CreateTable<Skin>();
            SqlHelper.GetConnection().CreateTable<Level>();



            sp = GetSharedPreferences("details", FileCreationMode.Private);

            int id = sp.GetInt("userId", -1);
            if (id != -1) // User connected
            {
                Intent intent = new Intent(this, typeof(Home_Activity));
                StartActivity(intent);
            }

            this.pd = new ProgressDialog(this);
            pd.SetMessage("please wait creating user");
            pd.SetCancelable(false);
        }

        private void BtnRegister_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Register_Activity));
            StartActivity(intent);
        }

        private void BtnLogin_Click(object sender, System.EventArgs e)
        {
            Dialog d = new Dialog(this);
            d.SetContentView(Resource.Layout.Dialog_Login);
            EditText etUsername = d.FindViewById<EditText>(Resource.Id.editTextUsernameDialog_Login);
            EditText etPassword = d.FindViewById<EditText>(Resource.Id.editTextPasswordDialog_Login);
            Button btnSave = d.FindViewById<Button>(Resource.Id.btnContinueDialog_Login);
            btnSave.Click += async (senderSave, eSave) =>
            {

                if (User.IsUserNameAndPassWordMatch(etUsername.Text, etPassword.Text))
                {
                    sp.Edit().PutInt("userId", User.GetId(etUsername.Text)).Apply();
                    if (!await User.ExistsFireBase(etUsername.Text))
                    {
                        Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(this);
                        builder.SetTitle("Adding User to global system");
                        builder.SetMessage("this user isn't exist in the global system would you like adding it?");

                        builder.SetPositiveButton("confirm", async (senderDialog, eDialog) =>
                        {
                            pd.Show();
                            if (await User.AddUserFirebase(User.GetName(User.GetId(etUsername.Text)), User.UserById(User.GetId(etUsername.Text)).PhoneNumber, etPassword.Text, etUsername.Text))
                            {
                                pd.Dismiss();
                                Intent intent = new Intent(this, typeof(Home_Activity));
                                StartActivity(intent);
                            }
                        });

                        builder.SetNegativeButton("decline", (senderDialog, eDialog) =>
                        {
                            Toast.MakeText(this, "enjoy anyway", ToastLength.Short).Show();
                        });

                        Dialog dialog = builder.Create();
                        dialog.Show();
                    }

                    Intent intent = new Intent(this, typeof(Home_Activity));
                    StartActivity(intent);
                    d.Dismiss();
                }
                else if (await User.ExistsFireBase(etUsername.Text))
                {
                    Task<User> user1 = User.GetUserFireBase(etUsername.Text);
                    User user = await user1;
                    if (etPassword.Text == user.Password)
                    {
                        User.AddUser(user.FullName, user.PhoneNumber, user.Password, user.UserName);
                        sp.Edit().PutInt("userId", User.GetId(etUsername.Text)).Apply();
                        Intent intent = new Intent(this, typeof(Home_Activity));
                        StartActivity(intent);
                        d.Dismiss();
                    }
                }
                else
                {
                    Toast.MakeText(this, "user name or password incorrect", ToastLength.Long).Show();
                }
            };
            d.Show();
        }

    }
}