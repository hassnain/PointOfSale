using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PointOfSale
{
    public partial class formPOS : Form
    {
        public formPOS()
        {
            InitializeComponent();
        }

        private void btnCloseMainForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
