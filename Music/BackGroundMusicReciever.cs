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

namespace Clash_Of_Blocks.Droid
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { "my.music" }, Priority = (int)IntentFilterPriority.HighPriority)]
    class BackGroundMusicReciever : BroadcastReceiver
    {
        public static bool started { get; set; }

        public BackGroundMusicReciever()
        {
            started = true;
        }

        public override void OnReceive(Context context, Intent intent)
        {
            started = intent.GetBooleanExtra("musicStarted", false);

            if (started)
                Toast.MakeText(context, "music started", ToastLength.Short).Show();
            else
                Toast.MakeText(context, "music stopped", ToastLength.Short).Show();
        }
    }
}