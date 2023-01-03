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
            CustomerCb.SelectedIndex = -1;
            PhoneNumTb.Text = "";
            DevNameTb.Text = "";
            DevModelTb.Text = "";
            ProblemTb.Text = "";
            SpareCb.SelectedIndex = -1;
            SpareCostTb.Text = "";
            RepCostTb.Text = "";
            
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
    }
}
