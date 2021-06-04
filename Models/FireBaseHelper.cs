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
using Firebase;
using Plugin.CloudFirestore;

namespace Clash_Of_Blocks.Droid
{
    class FireBaseHelper
    {
        public static FireBaseHelper Instance { get; set; }
        public static IFirestore MyFirestore { get; set; }

        public static ICollectionReference UsersCollection { get; set; }
        public static ICollectionReference RecordsCollection { get; set; }

        private FireBaseHelper(Context context)
        {
            FirebaseOptions options = new FirebaseOptions.Builder()//מגדיר את הנתונים של הפיירבייס שלי כדי שאוכל להתחבר אליו וליצור תקשורת 
                                            .SetApiKey("AIzaSyBK9sAkg5WA1JdhkanHnuH_nv6cYgrBByY")//current api key
                                            .SetProjectId("clash-of-blocks-58b95")
                                            .SetApplicationId("clash_of_blocks.clash_of_blocks")
                                            .SetDatabaseUrl("https://clash-of-blocks-58b95.firebaseio.com")//  הקישור למיקום של המסד נתונים של הפיירבייס 
                                            .SetStorageBucket("clash-of-blocks-58b95.appspot.com")
                                            .Build();

            FirebaseApp app = FirebaseApp.InitializeApp(context, options);//יוצר את החיבור  עם השרת של הפייר בייס ברגע שאני פונה למשתנה אפפ   
            MyFirestore = CrossCloudFirestore.Current.Instance; //עותק של הפיירבייס הנוכחי באפליקציה

            UsersCollection = MyFirestore.GetCollection("Users");
            RecordsCollection = MyFirestore.GetCollection("Records");
        }

        public static void Initialize(Context context)
        {
            if (Instance == null)
            {
                Instance = new FireBaseHelper(context);
            }
        }
    }
}