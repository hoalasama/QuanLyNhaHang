﻿using QLNH.DAO;
using QLNH.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNH
{
    public partial class fAdmin : Form
    {
        BindingSource foodList = new BindingSource();
        BindingSource categoryList = new BindingSource();
        BindingSource tableList = new BindingSource();  

        public fAdmin()
        {
            InitializeComponent();
            LoadData();
            txbFoodID.Enabled = false;
            txbCategoryID.Enabled = false;
            cbTableStatus.Enabled = false;
        }

        void LoadData()
        {
            dtgvFood.DataSource = foodList;
            dtgvCategory.DataSource = categoryList;
            dtgvTable.DataSource = tableList;
            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            LoadListFood();
            LoadListCategory();
            LoadListTable();
            LoadAccountList();
            AddFoodBinding();
            AddCategoryBinding();
            AddTableBinding();
            LoadCategoryToCombobox(cbCategory);
        }

        void LoadAccountList()
        {
            string query = "SELECT * FROM USERS";

            dtgvAccount.DataSource = DataProvider.Instance.ExecuteQuery(query);

        }

        void LoadFoodList()
        {
            string query = "SELECT * FROM MENU";

            foodList.DataSource = DataProvider.Instance.ExecuteQuery(query);

        }

        void LoadCategoryList()
        {
            string query = "SELECT * FROM CATEGORY";

            categoryList.DataSource = DataProvider.Instance.ExecuteQuery(query);

        }

        void LoadTableList()
        {
            string query = "SELECT * FROM TABLES";

            categoryList.DataSource = DataProvider.Instance.ExecuteQuery(query);

        }

        void AddFoodBinding()
        {
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "FoodName", true, DataSourceUpdateMode.Never));
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }

        void AddCategoryBinding() 
        {
            txbCategoryName.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "CateName", true, DataSourceUpdateMode.Never));
            txbCategoryID.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "ID", true, DataSourceUpdateMode.Never));
        }

        void AddTableBinding()
        {
            txbTableName.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbTableID.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "ID", true, DataSourceUpdateMode.Never));
        }

        void LoadCategoryToCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "CateName";
        }

        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }

        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);    
        }

        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }

        void LoadListCategory()
        {
            categoryList.DataSource = CategoryDAO.Instance.GetListCategory();
        }

        void LoadListTable()
        {
            tableList.DataSource = TableDAO.Instance.GetListTable();
        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }

        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadListTable();
        }

        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            if (dtgvFood.SelectedCells.Count > 0)
            {
                int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["CateID"].Value;
                Category category = CategoryDAO.Instance.GetCategoryByID(id);
                cbCategory.SelectedItem = category;

                int index = -1;
                int i = 0;
                foreach (Category item in cbCategory.Items)
                {
                    if (item.ID == category.ID)
                    {
                        index = i; break;
                    }
                    i++;
                }
                cbCategory.SelectedIndex = index;
            }
            
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            float price = (float)nmFoodPrice.Value;
            int categoryID = (cbCategory.SelectedItem as Category).ID;

            if (FoodDAO.Instance.IsFoodNameExists(name))
            {
                MessageBox.Show("Tên món ăn đã tồn tại. Vui lòng điền tên khác!", "Lỗi trùng tên", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (FoodDAO.Instance.InsertFood(name, price, categoryID))
            {
                MessageBox.Show("Thêm thành công");
                LoadListFood();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm vào");
            }
        }

        private void btnUpdateFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);
            string name = txbFoodName.Text;
            float price = (float)nmFoodPrice.Value;
            int categoryID = (cbCategory.SelectedItem as Category).ID;

            if (FoodDAO.Instance.UpdateFood(id, name, price, categoryID))
            {
                MessageBox.Show("Chỉnh sửa thông tin thành công");
                LoadListFood();
                if (updateFood != null)
                {
                    updateFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi chỉnh sửa");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xóa thành công");
                LoadListFood();
                if (deleteFood != null)
                {
                    deleteFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa");
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;

            if (CategoryDAO.Instance.IsCategoryNameExists(name))
            {
                MessageBox.Show("Tên danh mục đã tồn tại. Vui lòng điền tên khác!", "Lỗi trùng tên", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CategoryDAO.Instance.InsertCategory(name))
            {
                MessageBox.Show("Thêm thành công");
                LoadListCategory();
                LoadCategoryToCombobox(cbCategory);
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm vào");
            }
        }

        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCategoryID.Text);
            string name = txbCategoryName.Text;

            if (CategoryDAO.Instance.IsCategoryNameExists(name))
            {
                MessageBox.Show("Tên danh mục đã tồn tại. Vui lòng điền tên khác!", "Lỗi trùng tên", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CategoryDAO.Instance.UpdateCategory(id, name))
            {
                MessageBox.Show("Chỉnh sửa thông tin thành công");
                LoadListCategory();
                LoadCategoryToCombobox(cbCategory);
                if (updateCategory != null)
                {
                    updateCategory(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi chỉnh sửa");
            }

        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCategoryID.Text);

            if (CategoryDAO.Instance.DeleteCategory(id))
            {
                MessageBox.Show("Xóa thành công");
                LoadListCategory();
                LoadCategoryToCombobox(cbCategory);
                if (deleteCategory != null)
                {
                    deleteCategory(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa");
            }
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;

            if (TableDAO.Instance.IsTableNameExists(name))
            {
                MessageBox.Show("Tên bàn ăn đã tồn tại. Vui lòng điền tên khác!", "Lỗi trùng tên", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (TableDAO.Instance.InsertTable(name))
            {
                MessageBox.Show("Thêm thành công");
                LoadListTable();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm vào");
            }
        }

        private void btnUpdateTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbTableID.Text);
            string name = txbTableName.Text;

            if (TableDAO.Instance.IsTableNameExists(name))
            {
                MessageBox.Show("Tên bàn ăn đã tồn tại. Vui lòng điền tên khác!", "Lỗi trùng tên", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (TableDAO.Instance.UpdateTable(id, name))
            {
                MessageBox.Show("Chỉnh sửa thông tin thành công");
                LoadListTable();
                if (updateTable != null)
                {
                    updateTable(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi chỉnh sửa");
            }

        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbTableID.Text);

            if (TableDAO.Instance.DeleteTable(id))
            {
                MessageBox.Show("Xóa thành công");
                LoadListTable();
                if (deleteTable != null)
                {
                    deleteTable(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa");
            }
        }

        private event EventHandler insertFood;

        public event EventHandler InsertFood
        {
            add { insertFood += value; } 
            remove { insertFood -= value; }
        }

        private event EventHandler deleteFood;

        public event EventHandler DeleteFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }

        private event EventHandler updateFood;

        public event EventHandler UpdateFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }

        private event EventHandler insertCategory;

        public event EventHandler InserCategory
        {
            add { insertCategory += value; }
            remove { insertCategory -= value; }
        }

        private event EventHandler deleteCategory;

        public event EventHandler DeleteCategory
        {
            add { deleteCategory += value; }
            remove { deleteCategory -= value; }
        }

        private event EventHandler updateCategory;

        public event EventHandler UpdateCategory
        {
            add { updateCategory += value; }
            remove { updateCategory -= value; }
        }

        private event EventHandler insertTable;

        public event EventHandler InserTable
        {
            add { insertTable += value; }
            remove { insertTable -= value; }
        }

        private event EventHandler deleteTable;

        public event EventHandler DeleteTable
        {
            add { deleteTable += value; }
            remove { deleteTable -= value; }
        }

        private event EventHandler updateTable;

        public event EventHandler UpdateTable
        {
            add { updateTable += value; }
            remove { updateTable -= value; }
        }

    }
}
