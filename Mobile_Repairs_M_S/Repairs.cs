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
    }
}
