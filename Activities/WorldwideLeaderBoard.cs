using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Clash_of_blocks;

namespace Clash_Of_Blocks.Droid
{
    [Activity(Label = "WorldwideLeaderBoard")]
    public class WorldwideLeaderBoard : Activity
    {
        Spinner spl;
        ArrayAdapter ad;
        ArrayList Levels;
        ListView lv;
        ISharedPreferences sp;
        AllLevels AllLevels;
        Button backButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ChooseLvlRecords);
            // Create your application here
            spl = FindViewById<Spinner>(Resource.Id.ChooseLvlSpinner);
            lv = FindViewById<ListView>(Resource.Id.LevelLV);
            backButton = FindViewById<Button>(Resource.Id.backButton);

            sp = GetSharedPreferences("details", FileCreationMode.Private);

            TextView tv = new TextView(this);
            AllLevels = new AllLevels(this, tv, new Skins(sp.GetInt("userId", -1)), sp.GetInt("userId", -1));
            this.GetLevels();

            ad = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, Levels);
            spl.Adapter = ad;

            spl.ItemSelected += Spl_ItemSelected;
            backButton.Click += BackButton_Click;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Home_Activity));
            this.Finish();
            StartActivity(intent);
        }

        private void Spl_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            this.GetAdapterByLvl(e.Position + 1);
        }
        public async void GetAdapterByLvl(int level)
        {
            List<Record> records1 = await Record.ReverseSortForLvl(level);
            RecordAdapter MyAdapter = new RecordAdapter(records1, this);
            lv.Adapter = MyAdapter;
            lv.DeferNotifyDataSetChanged();
        }

        private void GetLevels()
        {
            Levels = new ArrayList();
            int lvl = AllLevels.GetLevels().Count;
            for (int i = 1; i <= lvl; i++)
            {
                Levels.Add("Level " + i);
            }
        }
    }
}