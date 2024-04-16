using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNH.DAO
{
    public class Food
    {
        public Food(int id, string foodname, float price, int cateID, string foodImg)
        {
            this.ID = id;
            this.FoodName = foodname;
            this.Price = price;
            this.CateID = cateID;
            this.FoodImg = foodImg;
        }

        public Food(DataRow row)
        {
            this.ID = (int)row["menu_id"];
            this.FoodName = row["menu_name"].ToString();
            this.Price = (float)Convert.ToDouble(row["menu_price"].ToString());
            this.CateID = (int)row["cate_id"];
            this.FoodImg = row["menu_img"].ToString();
        }

        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string foodName;
        public string FoodName
        {
            get { return foodName; }
            set { foodName = value; }
        }

        private float price;
        public float Price
        { 
            get { return price; }
            set { price = value; }
        }

        private int cateID;
        public int CateID
        {
            get { return cateID; }
            set { cateID = value; }
        }

        public string foodImg;
        public string FoodImg
        {
            get { return foodImg; }
            set
            {
                foodImg = value;
            }
        }
    }
}
