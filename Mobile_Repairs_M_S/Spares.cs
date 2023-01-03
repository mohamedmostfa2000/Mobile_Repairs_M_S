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
    public partial class Spares : Form
    {
        Functions Con;
        public Spares()
        {
            Con = new Functions();
            InitializeComponent();
            ShowSparesList();
        }

        private void ShowSparesList()
        {
            string Query = "Select * from SpareTbl";
            PartsList.DataSource = Con.GetData(Query);


        }

        private void Clear()
        {
            PartNameTb.Text = "";
            PartCostTb.Text = "";
           
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (PartNameTb.Text == "" || PartCostTb.Text == "")
                {
                    MessageBox.Show("Missing Data");
                }
                else
                {
                    string PName = PartNameTb.Text;
                    int PCost = Convert.ToInt32(PartCostTb.Text);

                    string Query = "insert into SpareTbl values('{0}',{1})";
                    Query = string.Format(Query, PName, PCost );
                    Con.SetData(Query);
                    ShowSparesList();
                    MessageBox.Show("Spare Added");
                    Clear();
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {

        }

        int Key = 0;

        private void PartsList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PartNameTb.Text = PartsList.SelectedRows[0].Cells[1].Value.ToString();
            PartCostTb.Text = PartsList.SelectedRows[0].Cells[2].Value.ToString();
            

            if (PartNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(PartsList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
