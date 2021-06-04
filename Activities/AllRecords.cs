﻿using System;
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
    [Activity(Label = "AllRecords")]
    public class AllRecords : Activity
    {
        ListView Records;
        ISharedPreferences sp;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.AllRecords);
            // Create your application here
            Records = FindViewById<ListView>(Resource.Id.listView1);
            int currentLevel = Intent.GetIntExtra("level", -1);
            this.GetAdapterByLvlAll(currentLevel);
        }

        public void GetAdapterByLvlAll(int level)
        {
            List<Record> records = Record.GetAllRecords(level);
            RecordAdapter MyAdapter = new RecordAdapter(records, this);
            Records.Adapter = MyAdapter;
            Records.DeferNotifyDataSetChanged();
        }
    }
}