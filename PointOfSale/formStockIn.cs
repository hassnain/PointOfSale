using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PointOfSale
{
    
    public partial class formStockIn : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        string stitle = "POS System";
        public formStockIn()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
        }

        public void LoadProduct()
        {
            dataGridProduct.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("select pcode,pdesc,price from tblProduct where pdesc like '%" + txtSearchProduct.Text + "%' order by pdesc",cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                dataGridProduct.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            }
            dr.Close();
            cn.Close();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void formStockIn_Load(object sender, EventArgs e)
        {

        }
    }
}
