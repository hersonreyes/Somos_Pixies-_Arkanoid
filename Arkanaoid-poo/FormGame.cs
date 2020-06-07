using System;
using System.Drawing;
using System.Windows.Forms;

namespace Arkanaoid_poo
{
    public partial class FormGame : Form
    {
        private int speed;
        private CustomPictureBox[,] tiles;
        private PictureBox ball;
        public FormGame()
        {
            InitializeComponent();
            speed = 5;
            WindowState = FormWindowState.Maximized;
            Height = ClientSize.Height;
            Width = ClientSize.Width;

        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage=Image.FromFile("../../../Sprites/Player.png");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Top = (Height - pictureBox1.Height) - 50;
            pictureBox1.Left = (Width / 2) - (pictureBox1.Width / 2);
            
            ball = new PictureBox();
            ball.Width = ball.Height = 20;
            ball.BackgroundImage = Image.FromFile("../../../Sprites/Ball.png");
            ball.BackgroundImageLayout = ImageLayout.Stretch;
            ball.Top = pictureBox1.Top - ball.Height;
            ball.Left = pictureBox1.Left + (pictureBox1.Width /2) - (ball.Width / 2);
            Controls.Add(ball);
            LoadTiles();
        }

        private void LoadTiles()
        {
            int xAxis = 10;
            int yAxis = 6;
            
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
                    tiles[i, j].Top = i * pHeight;
                    
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
    }
}