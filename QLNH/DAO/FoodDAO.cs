using QLNH.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNH.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO(); return FoodDAO.instance; }
            private set { FoodDAO.instance = value; }
        }

        private FoodDAO() { }

        public List<Food>GetFoodByCategoryID(int id)
        {
            List<Food> listFood = new List<Food>();

            string query = "SELECT *FROM MENU WHERE cate_id = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                listFood.Add(food);
            }
            return listFood;
        }

        public List<Food> GetListFood()
        {
            List<Food> list = new List<Food>();
            string query = "SELECT * FROM MENU";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                string fullPath = item["menu_img"].ToString();
                string imageName = Path.GetFileName(fullPath);
                Food food = new Food(item);
                food.FoodImg = imageName;
                list.Add(food);
            }
            return list;
        }

        public List<Food> SearchFoodByName(string name)
        {
            string query = string.Format("SELECT * FROM MENU WHERE menu_name LIKE N'%{0}%'", name);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<Food> list = new List<Food>();
            foreach (DataRow item in data.Rows)
            {
                string fullPath = item["menu_img"].ToString();
                string imageName = Path.GetFileName(fullPath);
                Food food = new Food(item);
                food.FoodImg = imageName;
                list.Add(food);
            }
            return list;
        }

        public bool InsertFood(string name, float price, int idcate, string imageName)
        {
            int lastBackSlashIndex = imageName.LastIndexOf('\\');
            string fileName = imageName.Substring(lastBackSlashIndex + 1);

            string query = string.Format("INSERT MENU (menu_name, menu_price, cate_id, menu_img) VALUES (N'{0}', {1}, {2}, N'{3}')", name, price, idcate, fileName);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool IsFoodNameExists(string name)
        {
            string query = string.Format("SELECT COUNT(*) FROM MENU WHERE menu_name = N'{0}'", name);
            int count = (int)DataProvider.Instance.ExecuteScalar(query);
            return count > 0;
        }

        public bool UpdateFood(int idFood, string name, float price, int idcate, string imageName)
        {
            string query;

            if (imageName == "")
            {
                query = string.Format("UPDATE MENU SET menu_name = N'{0}', menu_price = {1}, cate_id = {2} WHERE menu_id = {4}", name, price, idcate, idFood);
            }
            else
            {
                imageName = imageName.Replace("''", "''"); 
                int lastBackSlashIndex = imageName.LastIndexOf('\\'); 
                string fileName = imageName.Substring(lastBackSlashIndex + 1);
                query = string.Format("UPDATE MENU SET menu_name = N'{0}', menu_price = {1}, cate_id = {2}, menu_img = N'{3}' WHERE menu_id = {4}", name, price, idcate, imageName, idFood);
            }

            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }


        public bool DeleteFood(int idFood)
        {
            BillInfoDAO.Instance.DeleteBillInfoByFoodID(idFood);

            string query = string.Format("DELETE MENU WHERE menu_id = {0}" ,idFood);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

    }
}
