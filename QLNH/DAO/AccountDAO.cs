using QLNH.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QLNH.DTO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }

        private AccountDAO() { }

        public bool Login(string userName, string passWord)
        {
            string query = "USP_Login @userName , @passWord";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { userName, passWord });
            return result.Rows.Count > 0;
        }

        public List<Account> GetListAccount()
        {
            List<Account> listAccount = new List<Account>();

            string query = "SELECT *FROM USERS WHERE user_role = 0";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Account account = new Account(item);
                listAccount.Add(account);
            }
            return listAccount;
        }

        public bool IsUserNameExists(string userName)
        {
            string query = string.Format("SELECT COUNT(*) FROM USERS WHERE user_name = {0}", userName);
            int count = (int)DataProvider.Instance.ExecuteScalar(query);
            return count > 0;
        }

        public bool InsertAccount(string userName, string passWord, string displayName, string fullName, string phone)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("exec USP_InsertAccount @userName , @passWord , @displayName , @fullName , @phone ", new object[] { userName, passWord, displayName, fullName, phone }) ;
            return result > 0;
        }

        public bool UpdateAccount(string userName, string displayName, string fullName, string passWord, string newPassword)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("exec USP_UpdateAccount @userName , @displayName , @fullName , @password , @newPassword ", new object[] { userName, displayName, fullName, passWord, newPassword });
            return result > 0;
        }

        public bool UpdateAccountInfo(int id, string userName, string password, string displayName, string fullName, string phone)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("exec USP_UpdateAccountInfo @userID , @userName , @passWord , @displayName , @fullName , @phone ",
                                                               new object[] { id, userName, password, displayName, fullName, phone });
            return result > 0;
        }

        public bool DeleteAccount(int id)
        {
            string query = string.Format("DELETE USERS WHERE user_id = {0}", id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public Account GetAccountByUserName(string userName)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM USERS WHERE user_name = '" + userName +"'");

            foreach (DataRow item in data.Rows)
            {
                return new Account(data.Rows[0]);
            }
            return null;
        }

    }
}
