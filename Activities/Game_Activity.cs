using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using clash_of_blocks;
using Clash_of_blocks;

namespace Clash_Of_Blocks.Droid
{
    [Activity(Label = "Game_Activity", Theme = "@android:style/Theme.DeviceDefault.NoActionBar.Fullscreen", ScreenOrientation = Android.Content.PM.ScreenOrientation.Locked)]
    public class Game_Activity : Activity
    {
        AllLevels AllLevels;
        LinearLayout game, turnsLayout;
        Button hintbtn;
        ImageButton restartbtn;
        ImageButton skinsbtn;
        Button LeaderBoard;
        int counter;
        public TextView tvturns { get; set; }
        Dialog EndingDialog;
        public Button NextLvl { get; set; }
        public Button RetryLvl { get; set; }
        ISharedPreferences sp;
        Skins skins;
        int skin;
        ImageButton Levelsbtn;
        ImageButton homebtn;
        int[] Locked;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Game_Layout);
            hintbtn = FindViewById<Button>(Resource.Id.hintbtn);
            tvturns = FindViewById<TextView>(Resource.Id.tvTurnsLeft);
            restartbtn = FindViewById<ImageButton>(Resource.Id.rstbtn);
            skinsbtn = FindViewById<ImageButton>(Resource.Id.skinsbtn);
            turnsLayout = FindViewById<LinearLayout>(Resource.Id.turns);
            game = FindViewById<LinearLayout>(Resource.Id.Game);
            LeaderBoard = FindViewById<Button>(Resource.Id.LeaderBoardbtn);
            Levelsbtn = FindViewById<ImageButton>(Resource.Id.Levelsbtn);
            homebtn = FindViewById<ImageButton>(Resource.Id.Homebtn);

            Locked = new int[] { Resource.Drawable.Level1, Resource.Drawable.Level2, Resource.Drawable.Level3, Resource.Drawable.Level4, Resource.Drawable.Level5, Resource.Drawable.Level6, Resource.Drawable.Level7 };

            sp = GetSharedPreferences("details", FileCreationMode.Private);
            skins = new Skins(sp.GetInt("userId", -1));

            skins.CurrentSkin = User.GetSkinById(sp.GetInt("userId", -1));
            skin = skins.CurrentSkin;

            counter = User.GetLevelById(sp.GetInt("userId", -1));

            Point screenSize = new Point();
            this.WindowManager.DefaultDisplay.GetSize(screenSize);

            Cell.CellWidth = (screenSize.X - 100) / GameBoard.NUM_CELLS;
            Cell.CellHeight = game.LayoutParameters.Height / GameBoard.NUM_CELLS;

            hintbtn.Click += Hintbtn_Click;
            restartbtn.Click += Restartbtn_Click;
            skinsbtn.Click += Skinsbtn_Click;
            LeaderBoard.Click += LeaderBoard_Click;
            Levelsbtn.Click += Levelsbtn_Click;
            homebtn.Click += Homebtn_Click;

            AllLevels = new AllLevels(this, tvturns, skins, sp.GetInt("userId", -1));
            sp.Edit().PutInt("Levels", AllLevels.GetLevels().Count).Apply();
            tvturns.Text = AllLevels.GetLevels()[counter].GetTurns().ToString();

            View v = AllLevels.GetLevels()[counter];
            LinearLayout.LayoutParams parameters = new LinearLayout.LayoutParams(screenSize.X - 100, LinearLayout.LayoutParams.MatchParent);
            parameters.Gravity = GravityFlags.Center;
            v.LayoutParameters = parameters;
            game.AddView(v);
        }

        private void Homebtn_Click(object sender, EventArgs e)
        {
            this.Finish();
            Intent intent = new Intent(this, typeof(Home_Activity));
            StartActivity(intent);
        }

        private void Levelsbtn_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(LevelView));
            StartActivity(intent);
        }

        private void LeaderBoard_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AllRecords));
            intent.PutExtra("level", counter);
            StartActivity(intent);
        }

        public void ShowScoreDialog()
        {
            double green = AllLevels.GetLevels()[counter].GetPColor(Cell.Type.userGreen);
            double red = AllLevels.GetLevels()[counter].GetPColor(Cell.Type.botRed);
            double blue = AllLevels.GetLevels()[counter].GetPColor(Cell.Type.botBlue);
            Point screenSize = new Point();
            TextView CoinsForWin;

            this.WindowManager.DefaultDisplay.GetSize(screenSize);
            int height = (int)(screenSize.Y * 0.4);
            int width = (int)(screenSize.X * 0.75);
            EndingDialog = new Dialog(this);
            LinearLayout.LayoutParams layoutParamsColor = new LinearLayout.LayoutParams(80, 80);
            LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams(0, ViewGroup.LayoutParams.MatchParent);
            layoutParams.Gravity = GravityFlags.CenterHorizontal;
            layoutParams.Weight = 0.3f;
            if (green > red && green > blue)
            {
                EndingDialog.SetContentView(Resource.Layout.WonDialog);
                LinearLayout lScore = EndingDialog.FindViewById<LinearLayout>(Resource.Id.LayoutScoreWinning);
                LinearLayout ScoreGreen = new LinearLayout(this);
                ScoreGreen.SetGravity(GravityFlags.Center);
                ScoreGreen.LayoutParameters = layoutParams;
                ScoreGreen.Orientation = Orientation.Vertical;
                View ivgreen = new View(this);
                ivgreen.SetBackgroundColor(new Color(skins.skins[skin].Player));
                ivgreen.LayoutParameters = layoutParamsColor;
                TextView tvGreenScore = new TextView(this);
                tvGreenScore.Text = green.ToString() + "%";
                tvGreenScore.Gravity = GravityFlags.CenterHorizontal;
                ScoreGreen.AddView(ivgreen);
                ScoreGreen.AddView(tvGreenScore);
                lScore.AddView(ScoreGreen);
                if (red > 0)
                {
                    LinearLayout ScoreRed = new LinearLayout(this);
                    ScoreRed.SetGravity(GravityFlags.Center);
                    ScoreRed.LayoutParameters = layoutParams;
                    ScoreRed.Orientation = Orientation.Vertical;
                    View ivRed = new View(this);
                    ivRed.SetBackgroundColor(new Color(skins.skins[skin].Bot2));
                    ivRed.LayoutParameters = layoutParamsColor;
                    TextView tvRedScore = new TextView(this);
                    tvRedScore.Text = red.ToString() + "%";
                    tvRedScore.Gravity = GravityFlags.CenterHorizontal;
                    ScoreRed.AddView(ivRed);
                    ScoreRed.AddView(tvRedScore);
                    lScore.AddView(ScoreRed);
                }
                if (blue > 0)
                {
                    LinearLayout ScoreBlue = new LinearLayout(this);
                    ScoreBlue.SetGravity(GravityFlags.Center);
                    ScoreBlue.LayoutParameters = layoutParams;
                    ScoreBlue.Orientation = Orientation.Vertical;
                    View ivBlue = new View(this);
                    ivBlue.SetBackgroundColor(new Color(skins.skins[skin].Bot1));
                    ivBlue.LayoutParameters = layoutParamsColor;
                    TextView tvBlueScore = new TextView(this);
                    tvBlueScore.Text = blue.ToString() + "%";
                    tvBlueScore.Gravity = GravityFlags.CenterHorizontal;
                    ScoreBlue.AddView(ivBlue);
                    ScoreBlue.AddView(tvBlueScore);
                    lScore.AddView(ScoreBlue);
                }

                EndingDialog.SetCancelable(false);
                NextLvl = EndingDialog.FindViewById<Button>(Resource.Id.NextLvl);
                CoinsForWin = EndingDialog.FindViewById<TextView>(Resource.Id.CoinsForWin);
                CoinsForWin.Text = ((int)(green * 2 - ((counter + 1) * 10))).ToString();
                NextLvl.Click += async (senderNext, eNext) =>
                {
                    Record.AddRecord(User.GetName(sp.GetInt("userId", -1)), green, counter + 1);
                    if (await User.ExistsFireBase(User.GetName(sp.GetInt("userId", -1))))
                    {
                        await Record.AddRecordFirebase(User.GetName(sp.GetInt("userId", -1)), green, counter + 1);
                    }
                    Level.SetDoneById(counter + 1, true, sp.GetInt("userId", -1));
                    Level.SetIconById(counter + 1, Locked[counter], sp.GetInt("userId", -1));
                    User.SetCoinsById(sp.GetInt("userId", -1), int.Parse(CoinsForWin.Text));
                    game.RemoveView(AllLevels.GetLevels()[counter]);
                    AllLevels.GetLevels()[counter].Restart();
                    if (await User.ExistsFireBase(User.GetName(sp.GetInt("userId", -1))))
                    {
                        await User.SetLevelByIdFirebase(sp.GetInt("userId", -1), counter);
                    }
                    
                    try
                    {
                        User.SetLevelById(sp.GetInt("userId", -1), counter + 1);
                        Level.SetDoneById(counter + 1, true, sp.GetInt("userId", -1));
                        Level.SetIconById(counter + 1, Locked[counter], sp.GetInt("userId", -1));
                        var level = AllLevels.GetLevels()[counter+1];
                        tvturns.Text = level.GetTurns().ToString();
                        game.AddView(level);
                        EndingDialog.Dismiss();
                    }
                    catch
                    {
                        counter = 0;
                        User.SetLevelById(sp.GetInt("userId", -1), counter);
                        Intent intent = new Intent(this, typeof(FinishedActivity));
                        StartActivity(intent);
                    }
                };
                EndingDialog.Show();
                EndingDialog.Window.SetLayout(width, height);
                EndingDialog.Window.SetGravity(GravityFlags.Center);
            }
            else
            {
                EndingDialog.SetContentView(Resource.Layout.LosingDialog);
                LinearLayout lScore = EndingDialog.FindViewById<LinearLayout>(Resource.Id.LayoutScoreLosing);
                LinearLayout ScoreGreen = new LinearLayout(this);
                ScoreGreen.SetGravity(GravityFlags.Center);
                ScoreGreen.LayoutParameters = layoutParams;
                ScoreGreen.Orientation = Orientation.Vertical;
                ImageView ivgreen = new ImageView(this);
                ivgreen.SetBackgroundColor(new Color(skins.skins[skin].Player));
                ivgreen.LayoutParameters = layoutParamsColor;
                TextView tvGreenScore = new TextView(this);
                tvGreenScore.Text = green.ToString() + "%";
                tvGreenScore.Gravity = GravityFlags.CenterHorizontal;
                ScoreGreen.AddView(ivgreen);
                ScoreGreen.AddView(tvGreenScore);
                lScore.AddView(ScoreGreen);
                if (red > 0)
                {
                    LinearLayout ScoreRed = new LinearLayout(this);
                    ScoreRed.SetGravity(GravityFlags.Center);
                    ScoreRed.LayoutParameters = layoutParams;
                    ScoreGreen.Orientation = Orientation.Vertical;
                    ImageView ivRed = new ImageView(this);
                    ivRed.SetBackgroundColor(new Color(skins.skins[skin].Bot2));
                    ivRed.LayoutParameters = layoutParamsColor;
                    TextView tvRedScore = new TextView(this);
                    tvRedScore.Text = red.ToString() + "%";
                    tvRedScore.Gravity = GravityFlags.CenterHorizontal;
                    ScoreRed.AddView(ivRed);
                    ScoreRed.AddView(tvRedScore);
                    lScore.AddView(ScoreRed);
                }
                if (blue > 0)
                {
                    LinearLayout ScoreBlue = new LinearLayout(this);
                    ScoreBlue.SetGravity(GravityFlags.Center);
                    ScoreBlue.LayoutParameters = layoutParams;
                    ScoreBlue.Orientation = Orientation.Vertical;
                    ImageView ivBlue = new ImageView(this);
                    ivBlue.SetBackgroundColor(new Color(skins.skins[skin].Bot1));
                    ivBlue.LayoutParameters = layoutParamsColor;
                    TextView tvBlueScore = new TextView(this);
                    tvBlueScore.Text = blue.ToString() + "%";
                    tvBlueScore.Gravity = GravityFlags.CenterHorizontal;
                    ScoreBlue.AddView(ivBlue);
                    ScoreBlue.AddView(tvBlueScore);
                    lScore.AddView(ScoreBlue);
                }
                RetryLvl = EndingDialog.FindViewById<Button>(Resource.Id.Restartbtn);
                RetryLvl.Click += (senderNext, eNext) =>
                {
                    AllLevels.GetLevels()[counter].Restart();
                    tvturns.Text = AllLevels.GetLevels()[counter].GetTurns().ToString();
                    EndingDialog.Dismiss();
                };
                EndingDialog.Show();
                EndingDialog.Window.SetLayout(width, height);
            }
        }

        private void Skinsbtn_Click(object sender, EventArgs e)
        {
            if (!AllLevels.GetLevels()[counter].Playing)
            {
                Intent intent = new Intent(this, typeof(AllSkins));
                StartActivity(intent);
                AllLevels.GetLevels()[counter].Restart();
                this.Finish();
            }
        }

        private void Restartbtn_Click(object sender, EventArgs e)
        {
            if (!AllLevels.GetLevels()[counter].Playing)
            {
                AllLevels.GetLevels()[counter].Restart();
                tvturns.Text = AllLevels.GetLevels()[counter].GetTurns().ToString();
            }
        }

        private void Hintbtn_Click(object sender, EventArgs e)
        {
            if (!AllLevels.GetLevels()[counter].Playing)
            {
                AllLevels.GetLevels()[counter].UseHint();
            }
        }
    }
}