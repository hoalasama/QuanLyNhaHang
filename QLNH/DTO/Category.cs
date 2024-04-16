using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNH.DTO
{
    public class Category
    {
        public Category(int id, string catename)
        {
            this.ID = id;
            this.CateName = catename;
        }

        public Category(DataRow row)
        {
            this.ID = (int)row["cate_id"];
            this.CateName = row["cate_name"].ToString();
        }

        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string cateName;
        public string CateName
        {
            get { return cateName; }
            set { cateName = value; }
        }

        
    }
}
