using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNH.DTO
{
    public class Bill
    {
        public Bill(int id, DateTime dateCheckin, DateTime dateCheckOut, int status)
        {
            this.ID = id;
            this.DateCheckIn = dateCheckin;
            this.DateCheckOut = dateCheckOut;
            this.Status = status;
        }

        public Bill(DataRow row)
        {
            this.ID = (int)row["bill_id"];
            this.DateCheckIn = (DateTime)row["bill_checkin_date"];

            var dateCheckOutTemp = row["bill_checkout_date"];
            if (dateCheckOutTemp.ToString() != "") { 
                this.DateCheckOut = (DateTime)dateCheckOutTemp;
            }

            this.Status = (int)row["bill_status"];
        }

        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private DateTime dateCheckIn;

        private DateTime DateCheckIn
        {
            get { return dateCheckIn; }
            set { dateCheckIn = value; }
        }

        private DateTime dateCheckOut;

        private DateTime DateCheckOut
        {
            get { return dateCheckOut; }
            set { dateCheckOut = value; }
        }

        private int status;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
