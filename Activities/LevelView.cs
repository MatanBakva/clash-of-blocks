using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Clash_of_blocks;

namespace Clash_Of_Blocks.Droid
{
    [Activity(Label = "LevelView")]
    public class LevelView : Activity
    {

        GridView levels;
        AllLevels Levels;
        ISharedPreferences sp;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AllLevels);
            // Create your application here

            sp = GetSharedPreferences("details", FileCreationMode.Private);
            levels = FindViewById<GridView>(Resource.Id.LevelsgridView);
            Levels = new AllLevels(this, new TextView(this), new Skins(sp.GetInt("userId", -1)), sp.GetInt("userId", -1));
            LevelAdapter LevelAdapter = new LevelAdapter(this, Levels.level);
            levels.Adapter = LevelAdapter;
            levels.DeferNotifyDataSetChanged();
        }

        public void ChangeLevel(int x)
        {
            if (Level.GetDoneById(x, sp.GetInt("userId", -1)))
            {
                Intent intent = new Intent(this, typeof(Game_Activity));
                User.SetLevelById(sp.GetInt("userId", -1), x-1);
                StartActivity(intent);
                this.Finish();
            }
            else
            {
                if (User.GetLevelById(sp.GetInt("userId", -1)) > x)
                {
                    for (int i = 0; i <= x; i++)
                    {
                        Level.SetDoneById(i, true, sp.GetInt("userId", -1));
                    }
                }
                else
                    Toast.MakeText(this, "this level is still unlocked", ToastLength.Short).Show();
            }
        }
    }
}