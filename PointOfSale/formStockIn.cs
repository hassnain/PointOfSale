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

        //public void LoadProduct()
        //{
        //    int i = 0;
        //    dataGridProduct.Rows.Clear();
        //    cn.Open();
        //    cm = new SqlCommand("select pcode, pdesc, qty from tblProduct where pdesc like '%" + txtSearchProduct.Text + "%' order by pdesc",cn);
        //    dr = cm.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        i++;
        //        dataGridProduct.Rows.Add(i,dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
        //    }
        //    dr.Close();
        //    cn.Close();
        //}
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void formStockIn_Load(object sender, EventArgs e)
        {

        }

        private void dataGridProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //string colName = dataGridProduct.Columns[e.ColumnIndex].Name;
            //if (colName=="colSelect")
            //{
            //    if(txtRefNo.Text== string.Empty)
            //    {
            //        MessageBox.Show("Please Enter Reference No", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        txtRefNo.Focus();
            //        return;
            //    }
            //    if (txtStockInBy.Text == string.Empty)
            //    {
            //        MessageBox.Show("Please Enter Stock In By", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        txtStockInBy.Focus();
            //        return;
            //    }
            //    if (MessageBox.Show("Add This Item ?",stitle, MessageBoxButtons.YesNo ,MessageBoxIcon.Question)== DialogResult.Yes)

            //    {
            //        string productPcode= dataGridProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            //        cn.Open();
            //        cm = new SqlCommand("insert into tblStockIn(refno, pcode, sdate, stockinby)values(@refno, @pcode, @sdate, @stockinby)", cn); //* from tblProduct pcode like '" + dataGridProduct.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", cn);
            //        cm.Parameters.AddWithValue("@refno",txtRefNo.Text);
            //        cm.Parameters.AddWithValue("@pcode",productPcode );
            //        cm.Parameters.AddWithValue("@sdate",date1.Value);
            //        cm.Parameters.AddWithValue("@stockinby",txtStockInBy.Text);
            //        cm.ExecuteNonQuery();
            //        cn.Close();

            //        MessageBox.Show("successfully Added",stitle,MessageBoxButtons.OK,MessageBoxIcon.Information);
            //        loadStockIn();
            //    }
               

            //}
        }
        public void loadStockIn()
        {
            int i=0;
            dataGridShowStock.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("select * from vwStockIn where refno like '" +txtRefNo.Text+ "'",cn);
            dr = cm.ExecuteReader();
            while(dr.Read())
            {
                i++;
                dataGridShowStock.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridShowStock_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridShowStock.Columns[e.ColumnIndex].Name;
            if (colName=="colDelete")
            {
                if (MessageBox.Show("Remove This Item?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm=new SqlCommand("delete from tblstockin where id ='" + dataGridShowStock.Rows[e.RowIndex].Cells[1].Value.ToString() + "'",cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Successfully Deleted", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadStockIn();
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            formSearchProductStockIn frmSearchProductStockIn = new formSearchProductStockIn(this);
            frmSearchProductStockIn.LoadProduct();
            frmSearchProductStockIn.ShowDialog();
        }
    }
}
