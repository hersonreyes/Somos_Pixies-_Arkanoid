using System;
using System.Windows.Forms;

namespace Arkanaoid_poo
{
    public partial class frmList : Form
    {
        public frmList()
        {
            InitializeComponent();
        }

        private void frmList_Load(object sender, EventArgs e)
        {
            try
            {
                var dt = DBConnection.ExecuteQuery("SELECT * FROM SCORE order by score desc LIMIT 10");
                dataGridView1.DataSource = dt;
            
            }
            catch (Exception esg)
            {
                MessageBox.Show("Ha cocurrido un problema");
            }
        }
    }
}