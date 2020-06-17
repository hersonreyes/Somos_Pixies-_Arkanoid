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
        private UserControl current = null;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SoundPlayer player= new SoundPlayer();
            player.SoundLocation = "../../../Sounds/welcome.wav";
            player.Play();
            current= new GameOptions(this);
            current.Dock =  DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(current,1,1);
        }
    }
}