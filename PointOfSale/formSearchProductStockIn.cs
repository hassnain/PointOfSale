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
    public partial class formSearchProductStockIn : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        string stitle = "POS System";
        formStockIn slist;
        public formSearchProductStockIn(formStockIn flist)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            slist = flist;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void formSearchProductStockIn_Load(object sender, EventArgs e)
        {

        }
        public void LoadProduct()
        {
            int i = 0;
            dataGridProduct.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("select pcode, pdesc, qty from tblProduct where pdesc like '%" + txtSearchProduct.Text + "%' order by pdesc", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void dataGridProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridProduct.Columns[e.ColumnIndex].Name;
            if (colName == "colSelect")
            {
                if (slist.txtRefNo.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter Reference No", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    slist.txtRefNo.Focus();
                    return;
                }
                if (slist.txtStockInBy.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter Stock In By", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    slist.txtStockInBy.Focus();
                    return;
                }
                if (MessageBox.Show("Add This Item ?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    string productPcode = dataGridProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                    cn.Open();
                    cm = new SqlCommand("insert into tblStockIn(refno, pcode, sdate, stockinby)values(@refno, @pcode, @sdate, @stockinby)", cn); //* from tblProduct pcode like '" + dataGridProduct.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", cn);
                    cm.Parameters.AddWithValue("@refno", slist.txtRefNo.Text);
                    cm.Parameters.AddWithValue("@pcode", productPcode);
                    cm.Parameters.AddWithValue("@sdate", slist.date1.Value);
                    cm.Parameters.AddWithValue("@stockinby", slist.txtStockInBy.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    MessageBox.Show("successfully Added", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                   slist.loadStockIn();
                }


            }
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }
    }
}
