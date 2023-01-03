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
                if (CustNameTb.Text == "" || CustPhoneTb.Text=="" || CustAddTb.Text=="")
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
                    Clear();
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        int Key = 0;
        private void CustomersList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustNameTb.Text = CustomersList.SelectedRows[0].Cells[1].Value.ToString();
            CustPhoneTb.Text = CustomersList.SelectedRows[0].Cells[2].Value.ToString();
            CustAddTb.Text=CustomersList.SelectedRows[0].Cells[3].Value.ToString();
            
            if (CustNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CustomersList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (CustNameTb.Text == "" || CustPhoneTb.Text == "" || CustAddTb.Text == "")
                {
                    MessageBox.Show("Missing Data");
                }
                else
                {
                    string CName = CustNameTb.Text;
                    string CPhone = CustPhoneTb.Text;
                    string CAdd = CustAddTb.Text;
                    string Query = "Update CustomerTbl set CustName = '{0}',CustPhone = '{1}',CustAdd = '{2}' where CustCode ={3}";
                    Query = string.Format(Query, CName, CPhone, CAdd,Key);
                    Con.SetData(Query);
                    ShowCustomersList();
                    MessageBox.Show("Customer Updated");
                    Clear();
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
                    MessageBox.Show("Select a customer");
                }
                else
                {
                    
                    string Query = "delete from CustomerTbl where CustCode ={0}";
                    Query = string.Format(Query, Key);
                    Con.SetData(Query);
                    ShowCustomersList();
                    MessageBox.Show("Customer Deleted");
                    Clear();
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
         private void Clear()
        {
            CustNameTb.Text = "";
            CustPhoneTb.Text = "";
            CustAddTb.Text = "";
        }
    }
}
