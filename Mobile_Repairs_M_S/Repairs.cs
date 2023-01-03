using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mobile_Repairs_M_S
{
    public partial class Repairs : Form
    {
        Functions Con;
        public Repairs()
        {
            Con = new Functions();
            InitializeComponent();
            ShowRepairsList();
            GetCustomers();
            GetSpares();
            Clear();
        }

        private void ShowRepairsList()
        {
            string Query = "Select * from RepairTbl";
            RepairsList.DataSource = Con.GetData(Query);


        }

        private void GetCustomers()
        {
            string Query = "Select * from CustomerTbl";
            CustomerCb.DisplayMember = Con.GetData(Query).Columns["CustName"].ToString();
            CustomerCb.ValueMember = Con.GetData(Query).Columns["CustCode"].ToString();
            CustomerCb.DataSource = Con.GetData(Query);
        }
        private void GetSpares()
        {
            string Query = "Select * from SpareTbl";
            SpareCb.DisplayMember = Con.GetData(Query).Columns["SpareName"].ToString();
            SpareCb.ValueMember = Con.GetData(Query).Columns["SpareCode"].ToString();
            SpareCb.DataSource = Con.GetData(Query);
        }

        private void GetCost()
        {
            string Query = "Select * from SpareTbl where SpareCode = {0} ";
            Query = string.Format(Query, SpareCb.SelectedValue.ToString());
            foreach (DataRow dr in Con.GetData(Query).Rows)
            {
                SpareCostTb.Text = dr["SpareCost"].ToString();

            }
        }

        private void Clear()
        {
            RepDate.Text = "";
          
            PhoneNumTb.Text = "";
            DevNameTb.Text = "";
            DevModelTb.Text = "";
            ProblemTb.Text = "";
            
            SpareCostTb.Text = "";
            RepCostTb.Text = "";
            Key = 0;
            
        }


        private void SpareCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCost();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (CustomerCb.SelectedIndex == -1 || SpareCb.SelectedIndex == -1 || DevModelTb.Text == "" || DevNameTb.Text == "" || PhoneNumTb.Text == "" || ProblemTb.Text == "" || SpareCostTb.Text == "" || RepCostTb.Text == "")
                {
                    MessageBox.Show("Missing Data");
                }
                else
                {
                    string RDate = RepDate.Value.Date.ToString();
                    int Customer = Convert.ToInt32(CustomerCb.SelectedValue.ToString());
                    string CPhone = PhoneNumTb.Text;
                    string DeviceName = DevNameTb.Text;
                    string DeviceModel = DevModelTb.Text;
                    string Problem = ProblemTb.Text;
                    int Spare = Convert.ToInt32(SpareCb.SelectedValue.ToString());
                    int RepCost = Convert.ToInt32(RepCostTb.Text);
                    int Total = Convert.ToInt32(SpareCostTb.Text) + RepCost;

                    string Query = "insert into RepairTbl values('{0}',{1},'{2}','{3}','{4}','{5}',{6},{7})";
                    Query = string.Format(Query, RDate, Customer,CPhone,DeviceName,DeviceModel,Problem,Spare,Total);
                    Con.SetData(Query);
                    ShowRepairsList();
                    Clear();
                    MessageBox.Show("Repair Added");

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        int Key = 0;

        private void RepairsList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RepDate.Text = RepairsList.SelectedRows[0].Cells[1].Value.ToString();
            CustomerCb.SelectedValue = RepairsList.SelectedRows[0].Cells[2].Value.ToString();
            PhoneNumTb.Text = RepairsList.SelectedRows[0].Cells[3].Value.ToString();
            DevNameTb.Text = RepairsList.SelectedRows[0].Cells[4].Value.ToString();
            DevModelTb.Text = RepairsList.SelectedRows[0].Cells[5].Value.ToString();
            ProblemTb.Text = RepairsList.SelectedRows[0].Cells[6].Value.ToString();
            SpareCb.SelectedValue = RepairsList.SelectedRows[0].Cells[7].Value.ToString();
            GetCost();
            RepCostTb.Text = (((int)RepairsList.SelectedRows[0].Cells[8].Value) - (Convert.ToInt32(SpareCostTb.Text))).ToString();
            




            if (CustomerCb.SelectedIndex == -1)
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(RepairsList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (CustomerCb.SelectedIndex == -1 || SpareCb.SelectedIndex == -1 || DevModelTb.Text == "" || DevNameTb.Text == "" || PhoneNumTb.Text == "" || ProblemTb.Text == "" || SpareCostTb.Text == "" || RepCostTb.Text == "")
                {
                    MessageBox.Show("Missing Data");
                }
                else
                {
                    string RDate = RepDate.Value.Date.ToString();
                    int Customer = Convert.ToInt32(CustomerCb.SelectedValue.ToString());
                    string CPhone = PhoneNumTb.Text;
                    string DeviceName = DevNameTb.Text;
                    string DeviceModel = DevModelTb.Text;
                    string Problem = ProblemTb.Text;
                    int Spare = Convert.ToInt32(SpareCb.SelectedValue.ToString());
                    int RepCost = Convert.ToInt32(RepCostTb.Text);
                    int Total = Convert.ToInt32(SpareCostTb.Text) + RepCost;

                    string Query = "Update RepairTbl set RepDate ='{0}',Customer = {1},PhoneNumber = '{2}', DeviceName = '{3}',DeviceModel = '{4}', Problem = '{5}',Spare = {6},TotalCost = {7} where RepCode={8}";
                    Query = string.Format(Query, RDate, Customer, CPhone, DeviceName, DeviceModel, Problem, Spare, Total,Key);
                    Con.SetData(Query);
                    ShowRepairsList();
                    Clear();
                    MessageBox.Show("Repair Updated");

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Key == 0)
                {
                    MessageBox.Show("Select a Repair");
                }
                else
                {
                    
                    string Query = "delete from RepairTbl where RepCode={0}";
                    Query = string.Format(Query,Key);
                    Con.SetData(Query);
                    ShowRepairsList();
                    Clear();
                    MessageBox.Show("Repair Deleted");

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void CustomersPic_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void SparesPic_Click(object sender, EventArgs e)
        {
            Spares obj = new Spares();
            obj.Show();
            this.Hide();
        }

        private void RepairesPic_Click(object sender, EventArgs e)
        {
            Repairs obj = new Repairs();
            obj.Show();
            this.Hide();
        }

        private void LogoutPic_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }
    }
}
