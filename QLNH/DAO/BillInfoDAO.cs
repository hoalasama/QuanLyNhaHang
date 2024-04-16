using QLNH.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNH.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return instance; }
            private set { BillInfoDAO.instance = value; }
        }

        private BillInfoDAO() { }

        public List<BillInfo> GetListBillInfo(int id)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();

            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT *FROM BILL_DETAIL WHERE bd_id = " + id);
            
            foreach (DataRow item in data.Rows) 
            {
                BillInfo info = new BillInfo(item);
                listBillInfo.Add(info);

            }

            return listBillInfo;
        }

        public void InsertBillInfo(int idBill, int idFood, int quantity)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_InsertBillInfo  @idBill , @idFood , @quantity", new object[] { idBill, idFood, quantity });
        }

        public void DeleteBillInfoByFoodID(int id)
        {
            DataProvider.Instance.ExecuteQuery("DELETE BILL_DETAIL WHERE menu_id = " + id);

        }

        public int GetQuantityByBillIDAndFoodID(int idBill, int idFood)
        {
            string query = string.Format("SELECT quantity FROM BILL_DETAIL WHERE bill_id = {0} AND menu_id = {1}", idBill, idFood);
            int currentQuantity = (int)DataProvider.Instance.ExecuteScalar(query);
            return currentQuantity;
        }

    }
}
