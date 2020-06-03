using System;
using System.Windows.Forms;

namespace Arkanaoid_poo
{
    public partial class fmrRegister : Form
    {
        private Form1 main = null;
        public fmrRegister()
        {
            InitializeComponent();
        }
        public fmrRegister(Form1 main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
           
                DBConnection.ExecuteNonQuery($"INSERT INTO PLAYER(nickname) " +
                                             $"VALUES('{txtUserRegister.Text}')");
                this.main.Hide();
                


        }
    }
}