using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public class SquareGrid
    {
        Random R = new Random();
        bool[,] Map; // Height на Width
        int SquareSize;

        public SquareGrid(int MapHeight, int MapWidth, int SquareSize = 5)
        {
            Map = new bool[MapHeight, MapWidth];
            this.SquareSize = SquareSize;

            for (int i = 0; i < Map.GetLength(0); i++)
                for (int j = 0; j < Map.GetLength(1); j++) Map[i, j] = false;
        }
        int StringCheck(int id)
        {
            if (id >= Map.GetLength(0)) return 0;
            if (id < 0) return (Map.GetLength(0) - 1);
            return id;
        }
        int ColumCheck(int id)
        {
            if (id >= Map.GetLength(1)) return 0;
            if (id < 0) return (Map.GetLength(1) - 1);
            return id;
        }
        public void UpdatePrint(Graphics G)
        {
            for (int i = 0; i < Map.GetLength(0); i++)
                for (int j = 0; j < Map.GetLength(1); j++)
                    if (Map[i, j]) G.FillRectangle(Brushes.White, 0 + SquareSize * j, 0 + SquareSize * i, SquareSize, SquareSize);
                    else G.FillRectangle(Brushes.Black, 0 + SquareSize * j, 0 + SquareSize * i, SquareSize, SquareSize);
        }
        public void UpdateLogic(int Mode = 0)
        {
            bool[,] Buf = new bool[Map.GetLength(0), Map.GetLength(1)];

            
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    int bufcount = 0;

                    if (Map[StringCheck(i - 1), ColumCheck(j - 1)]) bufcount++;
                    if (Map[StringCheck(i - 1), ColumCheck(j)]) bufcount++;
                    if (Map[StringCheck(i - 1), ColumCheck(j + 1)]) bufcount++;

                    if (Map[StringCheck(i), ColumCheck(j - 1)]) bufcount++;
                    if (Map[StringCheck(i), ColumCheck(j + 1)]) bufcount++;

                    if (Map[StringCheck(i + 1), ColumCheck(j - 1)]) bufcount++;
                    if (Map[StringCheck(i + 1), ColumCheck(j)]) bufcount++;
                    if (Map[StringCheck(i + 1), ColumCheck(j + 1)]) bufcount++;

                    switch (Mode)
                    {
                        case 0: // Стандартный режим
                            if (Map[i, j] == false)
                            {
                                if (bufcount == 3) Buf[i, j] = true;
                                else Buf[i, j] = false;
                            }
                            else
                            {
                                if ((bufcount == 2) || (bufcount == 3)) Buf[i, j] = true;
                                else Buf[i, j] = false;
                            }
                            break;

                        case 1: // Фрактальный узор
                            if (Map[i, j] == false) 
                            {
                                if (bufcount == 1) Buf[i, j] = true;
                                else Buf[i, j] = false;
                            }
                            else
                            {
                                if ((bufcount >= 0) || (bufcount <= 8)) Buf[i, j] = true;
                                else Buf[i, j] = false;
                            }
                            break;
                    }
                }
            }

            Map = Buf;
        }
        public void MouseActive(MouseEventArgs e)
        {
            Random R = new Random();
            if (e.Button == MouseButtons.Left)
            {
                int PositionX = e.X / SquareSize, PositionY = e.Y / SquareSize;

                if (R.Next(0, 2) == 1) Map[StringCheck(PositionY - 1), ColumCheck(PositionX - 1)] = true;
                if (R.Next(0, 2) == 1) Map[StringCheck(PositionY - 1), ColumCheck(PositionX)] = true;
                if (R.Next(0, 2) == 1) Map[StringCheck(PositionY - 1), ColumCheck(PositionX + 1)] = true;

                if (R.Next(0, 2) == 1) Map[StringCheck(PositionY), ColumCheck(PositionX - 1)] = true;
                if (R.Next(0, 2) == 1) Map[StringCheck(PositionY), ColumCheck(PositionX)] = true;
                if (R.Next(0, 2) == 1) Map[StringCheck(PositionY), ColumCheck(PositionX + 1)] = true;

                if (R.Next(0, 2) == 1) Map[StringCheck(PositionY + 1), ColumCheck(PositionX - 1)] = true;
                if (R.Next(0, 2) == 1) Map[StringCheck(PositionY + 1), ColumCheck(PositionX)] = true;
                if (R.Next(0, 2) == 1) Map[StringCheck(PositionY + 1), ColumCheck(PositionX + 1)] = true;
            }

            if (e.Button == MouseButtons.Right) Map[StringCheck(e.Y / SquareSize), ColumCheck(e.X / SquareSize)] = true;
        }
        public void ClearMap()
        {
            Map = new bool[Map.GetLength(0), Map.GetLength(1)];

            for (int i = 0; i < Map.GetLength(0); i++)
                for (int j = 0; j < Map.GetLength(1); j++) Map[i, j] = false;
        }
        public void RandomFillScreen()
        {
            for (int i = 0; i < Map.GetLength(0); i++)
                for (int j = 0; j < Map.GetLength(1); j++)
                    if (R.Next(0, 101) <= 25) Map[i, j] = true;
                    else Map[i, j] = false;
        }
    }
}
