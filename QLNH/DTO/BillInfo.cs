using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNH.DTO
{
    public class BillInfo
    {
        public BillInfo(int id, int billID, int menuID, int quantity)
        {
            this.ID = id;
            this.BillID = billID;
            this.MenuID = menuID;
            this.Quantity = quantity;
        }

        public BillInfo(DataRow row)
        {
            this.ID = (int)row["bd_id"];
            this.BillID = (int)row["bill_id"];
            this.MenuID = (int)row["menu_id"];
            this.Quantity = (int)row["quantity"];
        }
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private int billID;

        public int BillID
        {
            get { return billID; }
            set { billID = value; }
        }

        private int menuID;

        public int MenuID
        {
            get { return menuID; }
            set { menuID = value; }
        }

        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
    }
}
