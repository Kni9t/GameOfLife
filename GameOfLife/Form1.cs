using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const int SIZE = 5;
        int MapHeight, MapWidth, Mode = 0;
        bool Time = true;

        SquareGrid SG;
        private void Form1_Load(object sender, EventArgs e)
        {
            MapHeight = pictureBox1.Height / SIZE;
            MapWidth = pictureBox1.Width / SIZE;

            SG = new SquareGrid(MapHeight, MapWidth, SIZE);

            timer1.Enabled = Time;
            if (Mode == 0) label1.Text = "Режим: Стандартный";
            else label1.Text = "Режим: Фрактальный";
            label3.Text = "Пробел - пауза\nМ - смера режима симуляции\nExc - очистка экрана\nF - заполнение экрана";

            label2.Visible = !Time;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            SG.MouseActive(e);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Random R = new Random();
            switch (e.KeyCode)
            {
                case Keys.Space: 
                    {
                        Time = !Time;
                        timer1.Enabled = Time;
                        label2.Visible = !Time;
                        break; 
                    }

                case Keys.M: 
                    {
                        if (Mode == 0) Mode = 1;
                        else Mode = 0;

                        if (Mode == 0) label1.Text = "Режим: Стандартный";
                        if (Mode == 1) label1.Text = "Режим: Фрактальный";
                        break; 
                    }

                case Keys.Escape: 
                    {
                        Bitmap BitMap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                        Graphics G = Graphics.FromImage(BitMap);

                        SG.ClearMap();
                        SG.UpdatePrint(G);

                        pictureBox1.Image = BitMap;
                        break; 
                    }
                case Keys.F:
                    {
                        SG.RandomFillScreen();
                        break;
                    }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Bitmap BitMap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics G = Graphics.FromImage(BitMap);

            SG.UpdatePrint(G);

            pictureBox1.Image = BitMap;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SG.UpdateLogic(Mode);
        }
    }
}
