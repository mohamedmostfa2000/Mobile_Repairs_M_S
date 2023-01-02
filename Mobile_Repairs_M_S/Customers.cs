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
    public partial class Customers : Form
    {
        Functions Con;
        public Customers()
        {
            Con = new Functions();
            InitializeComponent();
            ShowCustomersList();
        }

        private void ShowCustomersList()
        {
            string Query = "Select * from CustomerTbl";
            CustomersList.DataSource = Con.GetData(Query);


        }

        private void Customers_Load(object sender, EventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (CustNameTb.Text == "")
                {
                    MessageBox.Show("Missing Data");
                }
                else
                {
                    string CName = CustNameTb.Text;
                    string CPhone = CustPhoneTb.Text;
                    string CAdd = CustAddTb.Text;
                    string Query = "insert into CustomerTbl values('{0}','{1}','{2}')";
                    Query = string.Format(Query, CName,CPhone,CAdd);
                    Con.SetData(Query);
                    ShowCustomersList();
                    MessageBox.Show("Customer Added");
                    CustNameTb.Text = "";
                    CustPhoneTb.Text = "";
                    CustAddTb.Text = "";
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
    }
}
