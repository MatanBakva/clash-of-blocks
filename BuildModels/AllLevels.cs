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
using clash_of_blocks;
using Clash_of_blocks;

namespace Clash_Of_Blocks.Droid
{
    class AllLevels
    {
        private Activity activity;
        private int[,] change;
        private int[,] Hint;
        Skins S;
        List<GameBoard> levels;
        public GameBoard level1;
        public GameBoard level2;
        public GameBoard level3;
        public GameBoard level4;
        public GameBoard level5;
        public GameBoard level6;
        public GameBoard level7;
        public TextView tv { get; set; }
        public List<Level> level { get; set; }
        int userId;


        public AllLevels(Activity activity, TextView tv, Skins s, int userId)
        {
            level = new List<Level>();
            levels = new List<GameBoard>();
            this.activity = activity;
            this.tv = tv;
            S = s;
            this.userId = userId;
            CreateLevel1();
            CreateLevel2();
            Createlevel3();
            Createlevel4();
            CreateLevel5();
            CreateLevel6();
            CreateLevel7();
            SetLevels();
        }

        private void CreateLevel1()
        {
            change = new int[20, 20];
            Hint = new int[20, 20];
            for (int i = 0; i < 20; i++)//יצירת שוליים
            {
                change[i, 0] = 0;
                change[i, 19] = 0;
                change[0, i] = 0;
                change[19, i] = 0;
                Hint[i, 0] = 0;
                Hint[i, 19] = 0;
                Hint[0, i] = 0;
                Hint[19, i] = 0;
            }
            for (int i = 1; i < 19; i++)//הגדרת  כל השטח הנותר כפנוי
            {
                for (int j = 1; j < 19; j++)
                {
                    change[i, j] = 1;
                    Hint[i, j] = 1;
                }
            }
            for (int i = 7; i < 15; i++)
            {
                change[10, i] = 0;
            }
            Hint[9, 7] = 2;
            change[5, 7] = 3;
            level1 = new GameBoard(activity, change, 1, tv, Hint, S);
            levels.Add(level1);
            Level.AddLevel(userId, 1, Resource.Drawable.Level1, true);
        }

        private void CreateLevel2()
        {
            change = new int[20, 20];
            Hint = new int[20, 20];
            for (int i = 0; i < 20; i++)//יצירת שוליים
            {
                change[i, 0] = 0;
                change[i, 19] = 0;
                change[0, i] = 0;
                change[19, i] = 0;
                Hint[i, 0] = 0;
                Hint[i, 19] = 0;
                Hint[0, i] = 0;
                Hint[19, i] = 0;
            }
            for (int i = 1; i < 19; i++)//הגדרת  כל השטח הנותר כפנוי
            {
                for (int j = 1; j < 19; j++)
                {
                    change[i, j] = 1;
                    Hint[i, j] = 1;
                }
            }
            for (int i = 5; i < 14; i++)
            {
                for (int j = 10; j < 19; j++)
                {
                    change[j, i] = 0;
                }
            }
            Hint[8, 11] = 2;
            change[2, 5] = 3;
            level2 = new GameBoard(activity, change, 1, tv, Hint, S);
            levels.Add(level2);
            Level.AddLevel(userId, 2, Resource.Drawable.Level2, false);
        }

        private void Createlevel3()
        {
            change = new int[20, 20];
            Hint = new int[20, 20];
            for (int i = 0; i < 20; i++)//יצירת שוליים
            {
                change[i, 0] = 0;
                change[i, 19] = 0;
                change[0, i] = 0;
                change[19, i] = 0;
                Hint[i, 0] = 0;
                Hint[i, 19] = 0;
                Hint[0, i] = 0;
                Hint[19, i] = 0;
            }
            for (int i = 1; i < 19; i++)//הגדרת  כל השטח הנותר כפנוי
            {
                for (int j = 1; j < 19; j++)
                {
                    change[i, j] = 1;
                    Hint[i, j] = 1;
                }
            }
            for (int i = 7; i < 12; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    change[i, j] = 0;
                }
                for (int j = 7; j < 11; j++)
                {
                    change[i, j] = 0;
                }
                for (int j = 14; j < 19; j++)
                {
                    change[i, j] = 0;
                }
            }
            Hint[6, 7] = 2;
            Hint[15, 15] = 2;
            change[1, 2] = 4;
            change[15, 7] = 3;
            level3 = new GameBoard(activity, change, 2, tv, Hint, S);
            levels.Add(level3);
            Level.AddLevel(userId, 3, Resource.Drawable.Level3, false);
        }

        private void Createlevel4()
        {
            change = new int[20, 20];
            Hint = new int[20, 20];
            for (int i = 0; i < 20; i++)//יצירת שוליים
            {
                change[i, 0] = 0;
                change[i, 19] = 0;
                change[0, i] = 0;
                change[19, i] = 0;
                Hint[i, 0] = 0;
                Hint[i, 19] = 0;
                Hint[0, i] = 0;
                Hint[19, i] = 0;
            }
            for (int i = 1; i < 19; i++)//הגדרת  כל השטח הנותר כפנוי
            {
                for (int j = 1; j < 19; j++)
                {
                    change[i, j] = 1;
                    Hint[i, j] = 1;
                }
            }
            change[1, 1] = 3;
            Hint[10, 10] = 2;
            change[18, 18] = 4;
            level4 = new GameBoard(activity, change, 1, tv, Hint, S);
            levels.Add(level4);
            Level.AddLevel(userId, 4, Resource.Drawable.Level4, false);
        }

        private void CreateLevel5()
        {
            change = new int[20, 20];
            Hint = new int[20, 20];
            for (int i = 0; i < 20; i++)//יצירת שוליים
            {
                change[i, 0] = 0;
                change[i, 19] = 0;
                change[0, i] = 0;
                change[19, i] = 0;
                Hint[i, 0] = 0;
                Hint[i, 19] = 0;
                Hint[0, i] = 0;
                Hint[19, i] = 0;
            }
            for (int i = 1; i < 19; i++)//הגדרת  כל השטח הנותר כפנוי
            {
                for (int j = 1; j < 19; j++)
                {
                    change[i, j] = 1;
                    Hint[i, j] = 1;
                }
            }

            for (int i = 2; i < 18; i += 3)
            {
                for (int j = 2; j < 18; j += 3)
                {
                    change[i, j] = 0;
                }
            }
            Hint[9, 11] = 2;
            change[5, 3] = 3;
            change[15, 17] = 4;
            level5 = new GameBoard(activity, change, 1, tv, Hint, S);
            levels.Add(level5);
            Level.AddLevel(userId, 5, Resource.Drawable.Level5, false);
        }

        private void CreateLevel6()
        {
            change = new int[20, 20];
            Hint = new int[20, 20];
            for (int i = 0; i < 20; i++)//יצירת שוליים
            {
                change[i, 0] = 0;
                change[i, 19] = 0;
                change[0, i] = 0;
                change[19, i] = 0;
                Hint[i, 0] = 0;
                Hint[i, 19] = 0;
                Hint[0, i] = 0;
                Hint[19, i] = 0;
            }
            for (int i = 1; i < 19; i++)//הגדרת  כל השטח הנותר כפנוי
            {
                for (int j = 1; j < 19; j++)
                {
                    change[i, j] = 1;
                    Hint[i, j] = 1;
                }
            }

            for (int i = 5; i < 16; i++)
            {
                change[i, 5] = 0;
                change[i, 15] = 0;
                change[5, i] = 0;
                change[15, i] = 0;
                Hint[i, 5] = 0;
                Hint[i, 15] = 0;
                Hint[5, i] = 0;
                Hint[15, i] = 0;
            }
            for (int i = 9; i < 12; i++)
            {
                change[i, 5] = 1;
                change[i, 15] = 1;
                change[5, i] = 1;
                change[15, i] = 1;
                Hint[i, 5] = 1;
                Hint[i, 15] = 1;
                Hint[5, i] = 1;
                Hint[15, i] = 1;
            }
            change[4, 5] = 3;
            change[5, 16] = 4;
            Hint[16, 7] = 2;
            level6 = new GameBoard(activity, change, 1, tv, Hint, S);
            levels.Add(level6);
            Level.AddLevel(userId, 6, Resource.Drawable.Level6, false);

        }

        private void CreateLevel7()
        {
            change = new int[20, 20];
            Hint = new int[20, 20];
            for (int i = 0; i < 20; i++)//יצירת שוליים
            {
                change[i, 0] = 0;
                change[i, 19] = 0;
                change[0, i] = 0;
                change[19, i] = 0;
                Hint[i, 0] = 0;
                Hint[i, 19] = 0;
                Hint[0, i] = 0;
                Hint[19, i] = 0;
            }
            for (int i = 1; i < 19; i++)//הגדרת  כל השטח הנותר כפנוי
            {
                for (int j = 1; j < 19; j++)
                {
                    change[i, j] = 1;
                    Hint[i, j] = 1;
                }
            }
            for (int i = 6; i < 15; i++)
            {
                change[i, 10] = 0;
                change[10, i] = 0;
                Hint[i, 10] = 0;
                Hint[10, i] = 0;
            }
            for (int i = 0; i < 6; i++)
            {
                change[i, 5] = 0;
                change[5, i] = 0;
                change[14, i] = 0;
                change[i, 14] = 0;
                Hint[i, 5] = 0;
                Hint[5, i] = 0;
                Hint[14, i] = 0;
                Hint[i, 14] = 0;
                change[5, 14 + i] = 0;
                change[14 + i, 5] = 0;
                change[14, 14 + i] = 0;
                change[14 + i, 14] = 0;
                Hint[5, 14 + i] = 0;
                Hint[14 + i, 5] = 0;
                Hint[14, 14 + i] = 0;
                Hint[14 + i, 14] = 0;
            }

            change[1, 5] = 1;
            change[5, 1] = 1;
            change[1, 14] = 1;
            change[14, 1] = 1;
            change[14, 18] = 1;
            change[18, 14] = 1;
            change[18, 5] = 1;
            change[5, 18] = 1;

            Hint[1, 5] = 1;
            Hint[5, 1] = 1;
            Hint[1, 14] = 1;
            Hint[14, 1] = 1;
            Hint[14, 18] = 1;
            Hint[18, 14] = 1;
            Hint[18, 5] = 1;
            Hint[5, 18] = 1;


            Hint[4, 6] = 2;
            Hint[11, 11] = 2;
            change[15, 15] = 3;
            change[4, 4] = 3;
            change[11, 9] = 4;
            level7 = new GameBoard(activity, change, 2, tv, Hint, S);
            levels.Add(level7);
            Level.AddLevel(userId, 7, Resource.Drawable.Level7, true);
        }

        public List<GameBoard> GetLevels()
        {
            return levels;
        }

        public void SetLevels()
        {
            level.Add(new Level(1, userId, Resource.Drawable.Level1, true));
            level.Add(new Level(2, userId, Resource.Drawable.Level2, Level.GetDoneById(2, userId)));
            level.Add(new Level(3, userId, Resource.Drawable.Level3, Level.GetDoneById(3, userId)));
            level.Add(new Level(4, userId, Resource.Drawable.Level4, Level.GetDoneById(4, userId)));
            level.Add(new Level(5, userId, Resource.Drawable.Level5, Level.GetDoneById(5, userId)));
            level.Add(new Level(6, userId, Resource.Drawable.Level6, Level.GetDoneById(6, userId)));
            level.Add(new Level(7, userId, Resource.Drawable.Level7, Level.GetDoneById(7, userId)));
        }
    }
}