using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using Arkanaoid_poo.Controlador;

namespace Arkanaoid_poo.Vista
{
    public partial class fmrRegister : Form
    {
        SoundPlayer player= new SoundPlayer();
        private Form1 main = null;
        private List<string> balls = new List<string> {"Normal","Esfera del Dragon","Sharingan"};
        private string route = "";
        public delegate void GetNickname(string text);
        public GetNickname gn;
        public string nick;
        public int id;
        
        public fmrRegister()
        {
            InitializeComponent();
            


        }
        

        private void btnRegister_Click(object sender, EventArgs e)
        {
            
            try
            {    nick = txtUserRegister.Text;
                if (route.Equals(""))
                    throw new UnchargedBallException("Por favor, cargue una bola");
                 
                switch (txtUserRegister.Text)
                {
                    case string aux when aux.Length > 15:
                        throw new ExceededMaxCharactersException("No se puede introducir un nick de mas de 15 car");
                    case string aux when aux.Trim().Length == 0:
                        throw new EmptyNicknameException("No puede dejar campos vacios");
                    default:
                        gn?.Invoke(txtUserRegister.Text);
                        
                        var dt = DBConnection.ExecuteQuery($"SELECT idPlayer FROM PLAYER WHERE nickname = '{nick}'");
                        var dr = dt.Rows[0];
                         id =  Convert.ToInt32(dr[0].ToString());
                        
                        break;
                }
            
                
                FormGame game = new FormGame(route, id);
                game.Show();
                this.Hide();
            }
            catch(EmptyNicknameException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(ExceededMaxCharactersException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (UnchargedBallException esg)
            {
                MessageBox.Show(esg.Message);
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