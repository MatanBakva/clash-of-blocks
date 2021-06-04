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
    class LevelAdapter : BaseAdapter<Level>
    {
        LevelView activity;
        List<Level> levels;
        int[] unLocked =
        {
           Resource.Drawable.Level1,
           Resource.Drawable.Level2,
           Resource.Drawable.Level3,
           Resource.Drawable.Level4,
           Resource.Drawable.Level5,
           Resource.Drawable.Level6,
           Resource.Drawable.Level7
        };

        public LevelAdapter(LevelView activity, List<Level> levels)
        {
            this.activity = activity;
            this.levels = levels;
        }

        public override Level this[int position]
        {
            get
            {
                return levels[position];
            }
        }

        public override int Count
        {
            get
            {
                return levels.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = activity.LayoutInflater.Inflate(Resource.Layout.LevelCube, parent, false);
            }

            TextView Id = convertView.FindViewById<TextView>(Resource.Id.LevelId);
            ImageButton Level = convertView.FindViewById<ImageButton>(Resource.Id.Level1);

            Id.Text = "Level " + (position + 1).ToString();
            Level.SetImageResource(unLocked[position]);
            Level.Click += (senderSave, eSave) =>
            {
                activity.ChangeLevel(position+1);
            };
            return convertView;
        }

    }
}