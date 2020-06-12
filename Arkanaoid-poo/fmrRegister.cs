using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Arkanaoid_poo
{
    public partial class fmrRegister : Form
    {
        SoundPlayer player= new SoundPlayer();
        private Form1 main = null;
        private List<string> balls = new List<string> {"Normal","Esfera del Dragon","Sharingan"};
        private string route = "";
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
            try
            {
                DBConnection.ExecuteNonQuery($"INSERT INTO PLAYER(nickname) " +
                                             $"VALUES('{txtUserRegister.Text}')");

                if (route.Equals(""))
                    throw new UnchargedBallException("Por favor, cargue una bola");
                FormGame game = new FormGame(route);
                game.Show();
                main.Hide();
                this.Hide();


            }
            catch (UnchargedBallException esg)
            {
                MessageBox.Show(esg.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error, intente con otro nombre");
            }
            
        }

        private void fmrRegister_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = balls;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    route = "../../../Sprites/Ball.png";
                    pictureBox1.BackgroundImage=Image.FromFile(route);
                    pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                    break;
                 case 1:
                     route = "../../../Sprites/Ball2.png";
                     pictureBox1.BackgroundImage=Image.FromFile(route);
                     pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                     player.SoundLocation = "../../../Sounds/db.wav";
                     player.Play();
                     break;
                case 2:
                    route = "../../../Sprites/Ball3.png";
                    pictureBox1.BackgroundImage=Image.FromFile(route);
                    pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                    player.SoundLocation = "../../../Sounds/sharingan.wav";
                    player.Play();
                    break;
                 
            }
        }
    }
}