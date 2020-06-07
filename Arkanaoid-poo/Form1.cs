using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arkanaoid_poo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form register = new fmrRegister(this);
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

        private void Form1_Load(object sender, EventArgs e)
        {
            SoundPlayer player= new SoundPlayer();
            player.SoundLocation = "../../../Sounds/welcome.wav";
            player.Play();
            
        }
    }
}