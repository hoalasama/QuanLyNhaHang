using Microsoft.Win32;
using QLNH.DAO;
using QLNH.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = QLNH.DTO.Menu;

namespace QLNH
{
    public partial class fTableManager : Form
    {
        private Account loginAccount;
        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.Role); }
        }
        public fTableManager(Account acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;
            LoadTable();
            LoadCategory();
        }

        void ChangeAccount(int role)
        {
            adminToolStripMenuItem.Visible = role == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += " (" + LoginAccount.DisplayName + ")";
        }
        
        void LoadTable() 
        {
            fTable.Controls.Clear();
            List<Table> tableList = TableDAO.Instance.LoadTableList();
            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += btn_Click;
                btn.Tag = item;

                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.LightBlue;
                        break;
                    default:
                        btn.BackColor = Color.Pink;
                        break;
                }

                fTable.Controls.Add(btn);
            }
        }

        void LoadCategory() 
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "CateName";
        }

        void LoadFoodListByCategoryID(int id) 
        {
            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "FoodName";
        }

        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);
            float totalPrice = 0;
            foreach (Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.MenuName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lsvBill.Items.Add(lsvItem);
            }
            string formatString = "#,0.## đ";
            CultureInfo culture = new CultureInfo("vi-VN");
            txbTotalPrice.Text = totalPrice.ToString(formatString, culture);
        }

        void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(tableID);
        }

        private void cbCatalog_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
            {
                return;
            }

            Category selected = cb.SelectedItem as Category;
            id = selected.ID;

            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryID(id);

            if (listFood.Count == 0)
            {
                cbFood.DataSource = null;  
                cbFood.Items.Add("Không có"); 
                cbFood.SelectedIndex = 0; 
            }
            else
            {
                cbFood.DataSource = listFood;
                cbFood.DisplayMember = "FoodName";
            }
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            double totalPrice = Convert.ToDouble(txbTotalPrice.Text.Split(' ')[0]);

            if (idBill != -1)
            {
                if (MessageBox.Show("Bạn xác nhận thanh toán hóa đơn cho " + table.Name, "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill, (float)totalPrice );
                    ShowBill(table.ID);
                    LoadTable();
                }
            }

        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            if (table == null)
            {
                MessageBox.Show("Chọn bàn");
                return;
            }

            if (cbFood.SelectedItem.ToString() == "Không có")
            {
                MessageBox.Show("Không có món ăn nào trong danh mục này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            int foodID = (cbFood.SelectedItem as Food).ID;
            int quantity = (int)nmFoodCount.Value;

            if (quantity == 0)
            {
                MessageBox.Show("Món ăn đó chưa được thêm vào và số lượng được thêm vào không được là số âm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (quantity < 0)
            {
                int currentQuantity = BillInfoDAO.Instance.GetQuantityByBillIDAndFoodID(idBill, foodID);
                if (quantity < -currentQuantity)
                {
                    MessageBox.Show("Số lượng giảm vượt quá số lượng món ăn hiện có trong hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), foodID, quantity);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, foodID, quantity);
            }

            ShowBill(table.ID);

            LoadTable();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile(LoginAccount);
            f.UpdateAccount += f_UpdateAccount;
            f.ShowDialog();
        }

        void f_UpdateAccount(object sender, AccountEvent e)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin tài khoản (" + e.Acc.DisplayName + ")";
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.InsertFood += f_InsertFood;
            f.DeleteFood += f_DeleteFood;
            f.UpdateFood += f_UpdateFood;

            f.ShowDialog();
        }

        void f_UpdateFood(object sender, EventArgs args)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }

        void f_DeleteFood(object sender, EventArgs args)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }

        void f_InsertFood(object sender, EventArgs args)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }

    }
}
