using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using Arkanaoid_poo.Controlador;
using Arkanaoid_poo.Modelo;

namespace Arkanaoid_poo.Vista
{
    public partial class FormGame : Form
    {
        //Aun no se implementa ningun userControl, pero se hará luego
        private CustomPictureBox[,] tiles;
        SoundPlayer music=new SoundPlayer();
        private PictureBox ball;
        private string route;
        private string username;
        private Panel scores;
        private Label remainingHearts, score;
        private PictureBox heart;
        private int id;
        
        
        
       private int remainingtiles = 0;
       
        public FormGame(string route, int id)
        {
            InitializeComponent();
           
            WindowState = FormWindowState.Maximized;
            Height = ClientSize.Height;
            Width = ClientSize.Width;
            this.route = route;
            this.id= id;


        }

        private void FormGame_Load(object sender, EventArgs e)
        {  ScoresPanel();
            //seteando atributos para pictureBox del jugador
            pictureBox1.BackgroundImage=Image.FromFile("../../../../Sprites/Player.png");
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
            //seteando musica de fondo
            if (route.Equals("../../../../Sprites/Ball3.png"))
            {
                music.SoundLocation = "../../../../Sounds/naruto.wav";
                music.PlayLooping();
            }
            else  if (route.Equals("../../../../Sprites/Ball.png"))
            {
                music.SoundLocation = "../../../../Sounds/Nball.wav";
                music.PlayLooping();
            }
            else  if (route.Equals("../../../../Sprites/Ball2.png"))
            {
                music.SoundLocation = "../../../../Sounds/dragonball.wav";
                music.PlayLooping();
            }
            timer1.Start();
        }

        private void LoadTiles()
        {
            int xAxis = 10;
            int yAxis = 5;
            int LevelTop = 50;
            remainingtiles = xAxis * yAxis;
            
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
                    tiles[i, j].Top = i * pHeight + scores.Height;
                    
                    tiles[i, j].Height = pHeight;
                    tiles[i, j].Width = pWidth;
                    tiles[i,j].BackgroundImage=Image.FromFile("../../../../" + "Sprites/"+i+".png");
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
                return;
            
            GameData.ticksCount += 0.01;
            ball.Left += GameData.dirX;
            ball.Top += GameData.dirY;
            
            bounceBall();
        }

        private void FormGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                GameData.gameStarted = true;
                timer1.Start();
            }
        }

        private void bounceBall()
        {
            
             
            //si la bola toca el fondo de la pantalla
            if (ball.Bottom > Height)
            {
                GameData.hearts--;
                GameData.gameStarted = false;
                timer1.Stop();
                ElementsReposition();
                UpdateElements();
                if (GameData.hearts == 0)
                {
                    timer1.Stop();
                    MessageBox.Show("Has perdido");
                    music.Stop();
                    this.Hide();
                    GameData.hearts = 3;
                    GameData.score = 0;
                    GameData.ticksCount = 0;
                    
                }
            }
                
                 
            //rebote con la izquierda y derecha de la pantalla
            if (ball.Left < 0 || ball.Right > Width)
            {
                GameData.dirX = -GameData.dirX;
                return;
            }
            //rebote de la parte de arriba
            if (ball.Top < 50) 
                
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
                    {  GameData.score += (int)(tiles[i, j].Hits * GameData.ticksCount);
                        
                        tiles[i, j].Hits--;

                        if (tiles[i, j].Hits == 0)
                        {   
                            
                            Controls.Remove(tiles[i, j]);
                            tiles[i, j] = null;
                            remainingtiles--;
                        }
                        else
                        {   //si el bloque es golpeado y aun tiene vida, se cambia su diseño
                            tiles[i,j].BackgroundImage=Image.FromFile("../../../../" + "Sprites/Tile - blinded broken.png");
                            tiles[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                        }


                        GameData.dirY = -GameData.dirY;

                        score.Text = GameData.score.ToString();

                        if (remainingtiles == 0)
                        {
                            
                            timer1.Stop();
                            
                            PlayerController.CreateNewScore(id, GameData.score);
                           
                            MessageBox.Show("Has ganado!");
                            music.Stop();
                            this.Hide();
                            GameData.hearts = 3;
                            GameData.score = 0;
                            GameData.ticksCount = 0;
                           
                        }
                        return;
                    }
                }
            }
        }
          private void ScoresPanel()
        {
            // Instanciar panel
            scores = new Panel();

            // Setear elementos del panel
            scores.Width = Width;
            scores.Height = (int)(Height * 0.07);

            scores.Top = scores.Left = 0;

            scores.BackColor = Color.Black;

            #region Label + PictureBox
            // Instanciar pb
            heart = new PictureBox();

            heart.Height = heart.Width = scores.Height;

            heart.Top = 0;
            heart.Left = 20;

            heart.BackgroundImage = Image.FromFile("../../../../" + "Sprites/Heart.png");
            heart.BackgroundImageLayout = ImageLayout.Stretch;
            #endregion

            

            // Instanciar labels
            remainingHearts = new Label();
            score = new Label();

            // Setear elementos de los labels
            remainingHearts.ForeColor = score.ForeColor = Color.White;

            remainingHearts.Text = " x " + GameData.hearts.ToString();
            score.Text = GameData.score.ToString();

            remainingHearts.Font = score.Font = new Font("Microsoft YaHei", 24F);
            remainingHearts.TextAlign = score.TextAlign = ContentAlignment.MiddleCenter;

            remainingHearts.Left = heart.Right + 5;
            score.Left = Width - 100;

            remainingHearts.Height = score.Height = scores.Height;

            scores.Controls.Add(heart);
            scores.Controls.Add(remainingHearts);
            scores.Controls.Add(score);

          

            Controls.Add(scores);
        }

        private void ElementsReposition()
        {
            pictureBox1.Left = (Width / 2) - (pictureBox1.Width / 2);
            ball.Top = pictureBox1.Top - ball.Height;
            ball.Left = pictureBox1.Left + (pictureBox1.Width / 2) - (ball.Width / 2);
        }

        private void UpdateElements()
        {
            remainingHearts.Text = " x " + GameData.hearts.ToString();
            
        }
        
    }
}