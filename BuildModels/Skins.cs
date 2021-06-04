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
using Clash_of_blocks;

namespace Clash_Of_Blocks.Droid
{
    class Skins
    {
        public List<Skin> skins { get; set; }
        public int CurrentSkin { get; set; }
        int UserId;

        public Skins(int userid)
        {
            UserId = userid;
            skins = new List<Skin>();
            BuildSkin1();
            BuildSkin2();
            BuildSkin3();
            BuildSkin4();
            BuildSkin5();
            BuildSkin6();
        }

        private void BuildSkin1()
        {
            Color Player = Color.Green;
            Color Bot1 = Color.Blue;
            Color Bot2 = Color.Red;
            Bitmap bitmap = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.skin1);
            Skin.AddSkin(UserId, (int)Player, (int)Bot1, (int)Bot2, SqlHelper.BitmapToBase64(bitmap), 0, true);
            skins.Add(new Skin(UserId, (int)Player, (int)Bot1, (int)Bot2, SqlHelper.BitmapToBase64(bitmap), 0, true));
        }

        private void BuildSkin2()
        {
            Color Player = Color.Rgb(0, 255, 255);
            Color Bot1 = Color.Rgb(255, 153, 51);
            Color Bot2 = Color.Rgb(255, 102, 102);
            Bitmap bitmap = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.skin2);
            if (Skin.Exists((int)Player, (int)Bot1, (int)Bot2, UserId))
                skins.Add(new Skin(UserId, (int)Player, (int)Bot1, (int)Bot2, SqlHelper.BitmapToBase64(bitmap), 0, true));
            else
                skins.Add(new Skin(UserId, (int)Player, (int)Bot1, (int)Bot2, SqlHelper.BitmapToBase64(bitmap), 100, false));

        }

        private void BuildSkin3()
        {
            Color Player = Color.Rgb(228, 0, 255);
            Color Bot1 = Color.Rgb(0, 209, 103);
            Color Bot2 = Color.Rgb(149, 0, 56);
            Bitmap bitmap = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.skin3);
            if (Skin.Exists((int)Player, (int)Bot1, (int)Bot2, UserId))
                skins.Add(new Skin(UserId, (int)Player, (int)Bot1, (int)Bot2, SqlHelper.BitmapToBase64(bitmap), 0, true));
            else
                skins.Add(new Skin(UserId, (int)Player, (int)Bot1, (int)Bot2, SqlHelper.BitmapToBase64(bitmap), 200, false));
        }

        private void BuildSkin4()
        {
            Color Player = Color.Rgb(225, 0, 0);
            Color Bot1 = Color.Rgb(204, 0, 204);
            Color Bot2 = Color.Rgb(255, 102, 204);
            Bitmap bitmap = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.skin4);
            if (Skin.Exists((int)Player, (int)Bot1, (int)Bot2, UserId))
                skins.Add(new Skin(UserId, (int)Player, (int)Bot1, (int)Bot2, SqlHelper.BitmapToBase64(bitmap), 0, true));
            else
                skins.Add(new Skin(UserId, (int)Player, (int)Bot1, (int)Bot2, SqlHelper.BitmapToBase64(bitmap), 250, false));
        }

        private void BuildSkin5()
        {
            Color Player = Color.Rgb(0, 0, 0);
            Color Bot1 = Color.Rgb(102, 204, 255);
            Color Bot2 = Color.Rgb(204, 0, 153);
            Bitmap bitmap = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.skin5);
            if (Skin.Exists((int)Player, (int)Bot1, (int)Bot2, UserId))
                skins.Add(new Skin(UserId, (int)Player, (int)Bot1, (int)Bot2, SqlHelper.BitmapToBase64(bitmap), 0, true));
            else
                skins.Add(new Skin(UserId, (int)Player, (int)Bot1, (int)Bot2, SqlHelper.BitmapToBase64(bitmap), 500, false));
        }

        private void BuildSkin6()
        {
            Color Player = Color.Rgb(255, 60, 0);
            Color Bot1 = Color.Rgb(255, 255, 0);
            Color Bot2 = Color.Rgb(255, 153, 0);
            Bitmap bitmap = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.skin6);
            if (Skin.Exists((int)Player, (int)Bot1, (int)Bot2, UserId))
                skins.Add(new Skin(UserId, (int)Player, (int)Bot1, (int)Bot2, SqlHelper.BitmapToBase64(bitmap), 0, true));
            else
                skins.Add(new Skin(UserId, (int)Player, (int)Bot1, (int)Bot2, SqlHelper.BitmapToBase64(bitmap), 750, false));
        }
    }
}