using QLNH.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
            string query = "SELECT *FROM MENU";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }

        public bool InsertFood(string name, float price, int idcate)
        {
            string query = string.Format("INSERT MENU (menu_name, menu_price, cate_id ) VALUES (N'{0}', {1}, {2})", name, price, idcate);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool IsFoodNameExists(string name)
        {
            string query = string.Format("SELECT COUNT(*) FROM MENU WHERE menu_name = N'{0}'", name);
            int count = (int)DataProvider.Instance.ExecuteScalar(query);
            return count > 0;
        }

        public bool UpdateFood(int idFood, string name, float price, int idcate)
        {
            string query = string.Format("UPDATE MENU SET menu_name = N'{0}', menu_price = {1}, cate_id = {2} WHERE menu_id = {3}", name, price, idcate, idFood);
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
