using System;
using System.Drawing;
using System.Windows.Forms;

namespace Arkanaoid_poo
{
    public partial class FormGame : Form
    {
        //Aun no se implementa ningun userControl, pero se hará luego
        private CustomPictureBox[,] tiles;
        private PictureBox ball;
        private string route;
        public FormGame()
        {
            InitializeComponent();
            
            Height = ClientSize.Height;
            Width = ClientSize.Width;
            WindowState = FormWindowState.Maximized;

        }
        public FormGame(string route)
        {
            InitializeComponent();
           
            WindowState = FormWindowState.Maximized;
            Height = ClientSize.Height;
            Width = ClientSize.Width;
            this.route = route;

        }

        private void FormGame_Load(object sender, EventArgs e)
        {   //seteando atributos para pictureBox del jugador
            pictureBox1.BackgroundImage=Image.FromFile("../../../Sprites/Player.png");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Top = (Height - pictureBox1.Height) - 50;
            pictureBox1.Left = (Width / 2) - (pictureBox1.Width / 2);
            //seteando atibutos  para pictureBox de la pelota
            ball = new PictureBox();
            ball.Width = ball.Height = 30;
            ball.BackgroundImage = Image.FromFile(route);
            ball.BackgroundImageLayout = ImageLayout.Stretch;
            ball.Top = pictureBox1.Top - ball.Height;
            ball.Left = pictureBox1.Left + (pictureBox1.Width /2) - (ball.Width / 2);
            Controls.Add(ball);
            LoadTiles();
            timer1.Start();
        }

        private void LoadTiles()
        {
            int xAxis = 10;
            int yAxis = 5;
            
            int pHeight = (int) (Height * 0.29) / yAxis;
            int pWidth = (Width - (xAxis-5)) / xAxis;
            tiles= new CustomPictureBox[yAxis,xAxis];

            for (int i = 0; i < yAxis; i++)
            {
                for (int j = 0; j < xAxis; j++)
                {
                    tiles[i,j]= new CustomPictureBox();
                    if (i == 0)
                        tiles[i, j].Hits = 2;
                    else
                        tiles[i, j].Hits = 1;


                    tiles[i, j].Left = j * pWidth;
                    //top se cambia para dejar espacio para las vidas
                    tiles[i, j].Top = (i * pHeight) + 50;
                    
                    tiles[i, j].Height = pHeight;
                    tiles[i, j].Width = pWidth;
                    tiles[i,j].BackgroundImage=Image.FromFile("../../../" + "Sprites/"+i+".png");
                    tiles[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    
                    tiles[i, j].Tag = "tileTag";
                    Controls.Add((tiles[i,j]));

                }
            }
                
        }

        private void FormGame_MouseMove(object sender, MouseEventArgs e)
        {
            if (!GameData.gameStarted)
            {
                if(e.X < (Width - pictureBox1.Width))
                {
                    pictureBox1.Left = e.X;
                    ball.Left = pictureBox1.Left + (pictureBox1.Width / 2) - (ball.Width / 2);
                }
            }
            else
            {
                if (e.X < (Width - pictureBox1.Width))
                    pictureBox1.Left = e.X;
            }
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!GameData.gameStarted)
            {
                return;
            }

            ball.Left += GameData.dirX;
            ball.Top += GameData.dirY;
            
            bounceBall();
        }

        private void FormGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                GameData.gameStarted = true;
            }
        }

        private void bounceBall()
        {   //si la bola toca el fondo de la pantalla
            if (ball.Bottom > Height)
                //provisional hasta diseñar sistema de vidas
                Application.Exit(); 
            //rebote con la izquierda y derecha de la pantalla
            if (ball.Left < 0 || ball.Right > Width)
            {
                GameData.dirX = -GameData.dirX;
                return;
            }
            //Provisional, pues hay que considerar espacio para las vidas 
            if (ball.Top < 0)
            {
                GameData.dirY = -GameData.dirY;
                return;
            }
            //rebote con el jugador
            if (ball.Bounds.IntersectsWith(pictureBox1.Bounds))
            {
                GameData.dirY = -GameData.dirY;
            }
            
            for (int i = 4; i >= 0; i--)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (tiles[i, j] != null && ball.Bounds.IntersectsWith(tiles[i, j].Bounds))
                    {
                        tiles[i, j].Hits--;

                        if (tiles[i, j].Hits == 0)
                        {   
                            Controls.Remove(tiles[i, j]);
                            tiles[i, j] = null;
                        }
                        else
                        {   //si el bloque es golpeado y aun tiene vida, se cambia su diseño
                            tiles[i,j].BackgroundImage=Image.FromFile("../../../" + "Sprites/Tile - blinded broken.png");
                            tiles[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                        }


                        GameData.dirY = -GameData.dirY;
                        return;
                    }
                }
            }
        }
        
    }
}