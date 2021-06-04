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

namespace Clash_Of_Blocks.Droid
{
    class Cell : Shape
    {
        public static float CellWidth { get; set; }
        public static float CellHeight { get; set; }

        public int Row { get; set; }
        public int Col { get; set; }

        Skins S;

        float width;
        float Height;
        public enum Type
        {
            nothing,
            empty,
            userGreen,
            botBlue,
            botRed,
        }
        int num;
        public int CurrentSkin { get; set; }

        public Cell()
        {

        }

        public Cell(float x, float y, float width, float height, int num, int row, int col, int CurrentSkin, Skins S)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.Height = height;
            this.num = num;
            this.CurrentSkin = CurrentSkin;
            this.Row = row;
            this.Col = col;
            this.S = S;
        }

        public override void Draw(Canvas canvas)
        {
            Paint ccell = new Paint();
            switch (num)
            {
                case (int)Type.nothing:
                    ccell.Color = Color.WhiteSmoke;
                    break;
                case (int)Type.empty:
                    ccell.Color = Color.LightGray;
                    break;
                case (int)Type.userGreen:
                    ccell.Color = new Color(S.skins[CurrentSkin].Player);
                    break;
                case (int)Type.botBlue:
                    ccell.Color = new Color(S.skins[CurrentSkin].Bot1);
                    break;
                case (int)Type.botRed:
                    ccell.Color = new Color(S.skins[CurrentSkin].Bot2);
                    break;
            }

            Rect r = new Rect();

            r.Set((int)x, (int)y, (int)(x + width), (int)(y + Height));
            canvas.DrawRect(r, ccell);

        }

        public override bool DidUserTouchedMe(float x, float y)
        {
            if (this.x < x && this.x + this.width > x && this.y < y && this.y + this.Height > y)
                return true;
            return false;
        }

        public int GetColor()
        {
            return this.num;
        }

        public void ChangeType(int num)
        {
            this.num = num;
        }
    }
}