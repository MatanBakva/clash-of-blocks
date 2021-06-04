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
using SQLite;

namespace Clash_Of_Blocks.Droid
{
    [Table("tblSkins")]
    class Skin
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Player { get; set; }
        public int Bot1 { get; set; }
        public int Bot2 { get; set; }
        public string Icon { get; set; }
        public int Price { get; set; }
        public bool Sold { get; set; }

        public Skin() { }

        public Skin(int userid, int player, int bot1, int bot2, string icon, int price, bool sold)
        {
            UserId = userid;
            Player = player;
            Bot1 = bot1;
            Bot2 = bot2;
            Icon = icon;
            Price = price;
            Sold = sold;
        }

        public static bool AddSkin(int userid, int player, int bot1, int bot2, string icon, int price, bool sold)
        {
            if (!Exists(player, bot1, bot2, userid))
            {
                Skin newSkin = new Skin(userid, player, bot1, bot2, icon, price, sold);
                SqlHelper.GetConnection().Insert(newSkin);
                return true;
            }
            return false;
        }

        public static bool Exists(int player, int bot1, int bot2, int userid)
        {
            string Query = string.Format("SELECT tblSkins.* FROM tblSkins WHERE(((tblSkins.Player) = \"{0}\") AND ((tblSkins.Bot1) = \"{1}\") And ((tblSkins.Bot2) = \"{2}\")) AND ((tblSkins.UserId) = \"{3}\");", player, bot1, bot2, userid);
            List<Skin> skins = SqlHelper.GetConnection().Query<Skin>(Query);
            if (skins.Count == 0)
            {
                return false;
            }
            return true;
        }

        public static bool GetSoldById(int id)
        {
            string Query = string.Format("SELECT tblSkins.* FROM tblSkins WHERE (((tblSkins.Id)=\"{0}\"));", id);
            List<Skin> skins = SqlHelper.GetConnection().Query<Skin>(Query);
            if (skins.Count == 1)
            {
                return skins[0].Sold;
            }
            return false;
        }

    }
}