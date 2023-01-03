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
        }

        private void ShowRepairsList()
        {
            string Query = "Select * from RepairTbl";
            RepairsList.DataSource = Con.GetData(Query);


        }

    }
}
