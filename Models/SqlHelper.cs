using System;
using System.IO;
using Android.Graphics;
using Android.Util;
using SQLite;

namespace Clash_Of_Blocks.Droid
{
    class SqlHelper
    {
        private static SQLiteConnection connection = new SQLiteConnection(GetPath());
        public static SQLiteConnection GetConnection()
        {
            return connection;
        }

        public static string GetPath()
        {
            string dbName = "clash_of_blocksDB1";
            string directory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string fullPath = System.IO.Path.Combine(directory, dbName);
            return fullPath;
        }

        public static string BitmapToBase64(Bitmap bitmap)
        {
            string str = "";
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);

                byte[] bytes = stream.ToArray();
                str = Convert.ToBase64String(bytes);
            }
            return str;
        }

        public static Bitmap Base64ToBitmap(String base64String)
        {
            byte[] imageAsBytes = Base64.Decode(base64String, Base64Flags.Default);
            return BitmapFactory.DecodeByteArray(imageAsBytes, 0, imageAsBytes.Length);
        }
    }
}