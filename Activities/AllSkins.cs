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
    [Activity(Label = "AllSkins")]
    public class AllSkins : Activity
    {
        GridView skins;
        Skins s;
        ISharedPreferences sp;
        TextView Coins;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AllSkins);
            // Create your application here
            sp = GetSharedPreferences("details", FileCreationMode.Private);
            skins = FindViewById<GridView>(Resource.Id.gridView1);
            Coins = FindViewById<TextView>(Resource.Id.Coins);
            Coins.Text = User.GetCoinsById(sp.GetInt("userId", -1)).ToString();
            s = new Skins(sp.GetInt("userId", -1));
            SkinAdapter skinAdapter = new SkinAdapter(this, s.skins);
            skins.Adapter = skinAdapter;
            skins.DeferNotifyDataSetChanged();
        }

        public async void ChangeSkin(int x)
        {
            if (s.skins[x].Sold)
            {
                Intent intent = new Intent(this, typeof(Game_Activity));
                intent.PutExtra("currentSkin", x);
                User.SetSkinById(sp.GetInt("userId", -1), x);
                await User.SetLevelByIdFirebase(sp.GetInt("userId", -1), x);
                StartActivity(intent);
            }
            else if (s.skins[x].Price <= User.GetCoinsById(sp.GetInt("userId", -1)))
            {
                User.SetCoinsById(sp.GetInt("userId", -1), -s.skins[x].Price);
                Skin.AddSkin(sp.GetInt("userId", -1), s.skins[x].Player, s.skins[x].Bot1, s.skins[x].Bot2, s.skins[x].Icon, 0, true);
                User.SetSkinById(sp.GetInt("userId", -1), x);
                s.skins[x].Price = 0;
                s.skins[x].Sold = true;
                await User.SetLevelByIdFirebase(sp.GetInt("userId", -1), x);
                Intent intent = new Intent(this, typeof(Game_Activity));
                intent.PutExtra("currentSkin", x);
                skins.DeferNotifyDataSetChanged();
                StartActivity(intent);
            }
            else
            {
                Toast.MakeText(this, "you dont have enough coins to buy this skin", ToastLength.Short).Show();
            }

        }
    }
}