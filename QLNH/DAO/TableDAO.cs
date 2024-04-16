using QLNH.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNH.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get {if (instance == null) instance = new TableDAO(); return instance; } 
            private set { TableDAO.instance = value; }
        }

        public static int TableWidth = 70;
        public static int TableHeight = 70;

        private TableDAO() { }

        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();
            
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM TABLES");
            
            foreach (DataRow item in data.Rows) 
            {
                Table table = new Table(item);
                tableList.Add(table);
            }
            return tableList;
        }

        public List<Table> GetListTable()
        {
            List<Table> list = new List<Table>();
            string query = "SELECT *FROM TABLES";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                list.Add(table);
            }
            return list;
        }

        public bool InsertTable(string name)
        {
            string query = string.Format("INSERT TABLES (table_name) VALUES (N'{0}')", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool IsTableNameExists(string name)
        {
            string query = string.Format("SELECT COUNT(*) FROM TABLES WHERE table_name = N'{0}'", name);
            int count = (int)DataProvider.Instance.ExecuteScalar(query);
            return count > 0;
        }

        public bool UpdateTable(int idTable, string name)
        {
            string query = string.Format("UPDATE TABLES SET table_name = N'{0}' WHERE table_id = {1}", name, idTable);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteTable(int idTable)
        {
            string query = string.Format("DELETE TABLES WHERE table_id = {0}", idTable);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
