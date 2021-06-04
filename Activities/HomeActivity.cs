using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Clash_of_blocks;

namespace Clash_Of_Blocks.Droid
{
    [Activity(Label = "Home_Activity")]
    public class Home_Activity : AppCompatActivity
    {
        Button btnInstructions;
        Button btnPlay;
        Button btnLeaderBoards;
        Button btnFirebaseLB;
        Button btnstore;
        Button btnselectLvl;
        ISharedPreferences sp;
        BroadcastReceiver MyMusic;
        static bool firsttime = true;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.Home_Layout);

            btnInstructions = FindViewById<Button>(Resource.Id.btnInstructions);
            btnPlay = FindViewById<Button>(Resource.Id.btnPlay);
            btnLeaderBoards = FindViewById<Button>(Resource.Id.btnLeaderBoards);
            btnFirebaseLB = FindViewById<Button>(Resource.Id.btnLeaderBoardsFireBase);
            btnselectLvl = FindViewById<Button>(Resource.Id.btnSelectLvl);
            btnstore = FindViewById<Button>(Resource.Id.btnstore);

            btnInstructions.Click += BtnInstructions_Click;
            btnPlay.Click += BtnPlay_Click;
            btnLeaderBoards.Click += BtnLeaderBoards_Click;
            btnFirebaseLB.Click += BtnFirebaseLB_Click;
            btnstore.Click += Btnstore_Click;
            btnselectLvl.Click += BtnselectLvl_Click;

            sp = GetSharedPreferences("details", FileCreationMode.Private);

            this.MyMusic = new BackGroundMusicReciever();
            RegisterReceiver(this.MyMusic, new IntentFilter("my.music"));

            if (BackGroundMusic.IsPlaying)
            {
                Intent i = new Intent(this, typeof(BackGroundMusic));
                StartService(i);
            }

            if (firsttime)
            {
                string name = User.GetName(sp.GetInt("userId", -1));
                Toast.MakeText(this, "welcome " + name + " enjoy", ToastLength.Long).Show();
                firsttime = false;
            }
        }

        private void BtnselectLvl_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(LevelView));
            StartActivity(intent);
        }

        private void Btnstore_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AllSkins));
            StartActivity(intent);
        }

        private void BtnFirebaseLB_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(WorldwideLeaderBoard));
            StartActivity(intent);
        }

        private void BtnLeaderBoards_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RecordsForLvl));
            StartActivity(intent);
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Game_Activity));
            StartActivity(intent);
        }

        private void BtnInstructions_Click(object sender, System.EventArgs e)
        {
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(this);
            builder.SetTitle("Instructiuons");
            builder.SetMessage(Resource.String.InstructionsText);
            builder.SetPositiveButton("confirm", (senderDialog, eDialog) =>
            {
                Toast.MakeText(this, "lets play then", ToastLength.Long).Show();
            });
            Dialog d = builder.Create();
            d.Show();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.MenuInflater.Inflate(Resource.Menu.GameMenu, menu);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_Logout:
                    {
                        sp.Edit().PutInt("userId", -1).Apply();
                        this.Finish();
                        firsttime = true;
                        Intent intent = new Intent(this, typeof(MainActivity));
                        StartActivity(intent);
                        break;
                    }
                case Resource.Id.action_stop:
                    {
                        Intent intent = new Intent(this, typeof(BackGroundMusic));
                        if (BackGroundMusic.IsPlaying)
                        {
                            item.SetTitle("Start Music");
                            StopService(intent);
                        }
                        else
                        {
                            item.SetTitle("Stop Music");
                            StartService(intent);
                        }
                        break;
                    }
            }
            return base.OnContextItemSelected(item);
        }
    }
}