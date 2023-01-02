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
    }
}
