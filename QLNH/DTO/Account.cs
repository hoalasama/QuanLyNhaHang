using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QLNH.DTO
{
    public class Account
    {
        public Account(string userName, string displayName, string fullName, string phone, int role, string passWord = null)
        {
            this.UserName = userName;
            this.DisplayName = displayName;
            this.FullName = fullName;
            this.Phone = phone;
            this.Role = role;
            this.PassWord = passWord;
        }

        public Account(DataRow row)
        {
            this.UserName = row["user_name"].ToString();
            this.DisplayName = row["user_displayname"].ToString();
            this.FullName = row["user_fullname"].ToString();
            this.Phone = row["user_phone"].ToString();
            this.Role = (int)row["user_role"];
            this.PassWord = row["user_password"].ToString();
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string displayName;
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        private string passWord;
        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }

        private int role;
        public int Role
        {
            get { return role; }
            set { role = value; }
        }
    }
}
