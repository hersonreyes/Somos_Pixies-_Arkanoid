using System;
using System.Windows.Forms;
using Arkanaoid_poo.Controlador;
using Arkanaoid_poo.Modelo;

namespace Arkanaoid_poo.Vista
{
    public partial class GameOptions : UserControl
    {   public Player Playerg;
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
            fmrRegister fr = new fmrRegister();
            
            fr.gn = (string nick) => 
            {
                if (PlayerController.CreatePlayer(nick))
                {
                    MessageBox.Show($"Bienvenido nuevamenete {nick}");
                }
                else
                {
                    MessageBox.Show($"Gracias por registrarte {nick}");
                }

                Playerg = new Player(nick, 0);

                fr.Dispose();
                
            };
            
            fr.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmList List = new frmList();
            List.Show();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
       
    }
}