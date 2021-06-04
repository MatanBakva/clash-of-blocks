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
    abstract class Shape
    {
        public float x { get; set; }
        public float y { get; set; }

        public abstract void Draw(Canvas canvas);

        public abstract bool DidUserTouchedMe(float x, float y);
    }
}