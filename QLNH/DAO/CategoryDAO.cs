using QLNH.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNH.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance
        {
            get { if (instance == null) instance = new CategoryDAO(); return CategoryDAO.instance; }
            private set { CategoryDAO.instance = value; }
        }

        private CategoryDAO() { }

        public List<Category> GetListCategory() 
        {
            List<Category> listCategory = new List<Category>();

            string query = "SELECT *FROM CATEGORY";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Category category= new Category(item);
                listCategory.Add(category);
            }
            return listCategory;
        }

        public Category GetCategoryByID(int id)
        {
            Category category = null;
            
            string query = "SELECT *FROM CATEGORY WHERE cate_id = " + id;
            
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach(DataRow item in data.Rows)
            {
                category = new Category(item);
                return category;
            }
            return category;
        }

        public bool InsertCategory(string name)
        {
            string query = string.Format("INSERT CATEGORY (cate_name) VALUES (N'{0}')", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool IsCategoryNameExists(string name)
        {
            string query = string.Format("SELECT COUNT(*) FROM CATEGORY WHERE cate_name = N'{0}'", name);
            int count = (int)DataProvider.Instance.ExecuteScalar(query);
            return count > 0;
        }

        public bool UpdateCategory(int idCate, string name)
        {
            string query = string.Format("UPDATE CATEGORY SET cate_name = N'{0}' WHERE cate_id = {1}", name, idCate);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteCategory(int idCate)
        {
            int count = (int)DataProvider.Instance.ExecuteScalar($"SELECT COUNT(*) FROM MENU WHERE cate_id = {idCate}");
            if (count > 0)
            {
                MessageBox.Show("Không thể xóa danh mục này vì còn món ăn thuộc danh mục này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string query = string.Format("DELETE CATEGORY WHERE cate_id = {0}", idCate);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
