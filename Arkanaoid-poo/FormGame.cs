using System.Windows.Forms;

namespace Arkanaoid_poo
{
    public partial class FormGame : Form
    {
        private int speed;
        public FormGame()
        {
            InitializeComponent();
            speed = 5;
            WindowState = FormWindowState.Maximized;
            Height = Screen.PrimaryScreen.Bounds.Height;
            Width = Screen.PrimaryScreen.Bounds.Width;

        }
    }
}