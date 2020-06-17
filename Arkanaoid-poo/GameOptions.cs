using System;
using System.Windows.Forms;

namespace Arkanaoid_poo
{
    public partial class GameOptions : UserControl
    {
        private Form1 main = null;
        public GameOptions()
        {
            InitializeComponent();
        }
        public GameOptions(Form1 main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form register = new fmrRegister(main);
            register.WindowState = FormWindowState.Maximized;
            register.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form List = new frmList();
            List.WindowState = FormWindowState.Maximized;
            List.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}