using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOut
{
    public partial class MainForm : Form
    {
        private const int GridOffset = 25;
        private const int GridLength = 200;
        static private int NumCells = 3;
        private int CellLength = GridLength / NumCells;
        public PaintEventArgs drawCanvas;


        private bool[,] grid;
        private Random rand;
        public MainForm()
        {
            InitializeComponent();

            rand = new Random();
            grid = new bool[NumCells, NumCells];

            //Turn entire grid on
            for(int r = 0; r < NumCells; r++)
            {
                for(int c = 0; c < NumCells; c++)
                {
                    grid[r, c] = true;
                }
            }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if(NumCells == 3)
                x3ToolStripMenuItem.Checked = true;
            else if (NumCells == 4)
                x4ToolStripMenuItem.Checked = true;
            else
                x5ToolStripMenuItem.Checked = true;

            drawCanvas = e;
            Graphics g = e.Graphics;

            for(int r = 0; r < NumCells; r++)
            {
                for(int c = 0; c < NumCells; c++)
                {
                    Brush brush;
                    Pen pen;

                    if(grid[r, c])
                    {
                        pen = Pens.AliceBlue;
                        brush = Brushes.Red;
                    }
                    else
                    {
                        pen = Pens.Purple;
                        brush = Brushes.ForestGreen;
                    }

                    int x = c * CellLength + GridOffset;
                    int y = r * CellLength + GridOffset;

                    g.DrawRectangle(pen, x, y, CellLength, CellLength);
                    g.FillRectangle(brush, x + 1, y + 1, CellLength - 1, CellLength - 1);
                }
            }
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X < GridOffset || e.X > CellLength * NumCells + GridOffset || e.Y < GridOffset || e.Y > CellLength * NumCells + GridOffset)
                return;

            //find row, col of mouse press
            int r = (e.Y - GridOffset) / CellLength;
            int c = (e.X - GridOffset) / CellLength;

            //Invert selected box and all surrounding boxes
            for (int i = r - 1; i <= r + 1; i++)
                for (int j = c - 1; j <= c + 1; j++)
                    if (i >= 0 && i < NumCells && j >= 0 && j < NumCells)
                        grid[i, j] = !grid[i, j];

            //Redraw grid
            this.Invalidate();

            //Check to see if puzzle has been solved
            if(PlayerWon())
            {
                //Display winner
                MessageBox.Show(this, "Congratulations! You've won!", "Lights Out!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private bool PlayerWon()
        {
            bool playerWon = true;

            for(int i = 0; i < NumCells; i++)
            {
                for(int j = 0; j < NumCells; j++)
                {
                    if(grid[i, j] == true)
                    {
                        playerWon = false;
                    }
                }
            }
            return playerWon;
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            //Fill grid with either white or black
            for (int r = 0; r < NumCells; r++)
                for (int c = 0; c < NumCells; c++)
                    grid[r, c] = rand.Next(2) == 1;

            //Redraw grid
            this.Invalidate();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGameButton_Click(sender, e);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutBox = new AboutForm();
            aboutBox.ShowDialog(this);
        }

        private void x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            x4ToolStripMenuItem.Checked = false;
            x5ToolStripMenuItem.Checked = false;
            
            NumCells = 3;
            CellLength = GridLength / NumCells;
            grid = new bool[NumCells, NumCells];

            for (int r = 0; r < NumCells; r++)
            {
                for (int c = 0; c < NumCells; c++)
                {
                    grid[r, c] = true;
                }
            }

            this.Invalidate();
        }

        private void x4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            x3ToolStripMenuItem.Checked = false;
            x5ToolStripMenuItem.Checked = false;

            NumCells = 4;
            CellLength = GridLength / NumCells;
            grid = new bool[NumCells, NumCells];

            for (int r = 0; r < NumCells; r++)
            {
                for (int c = 0; c < NumCells; c++)
                {
                    grid[r, c] = true;
                }
            }

            this.Invalidate();
        }

        private void x5ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            x3ToolStripMenuItem.Checked = false;
            x4ToolStripMenuItem.Checked = false;

            NumCells = 5;
            CellLength = GridLength / NumCells;
            grid = new bool[NumCells, NumCells];

            for (int r = 0; r < NumCells; r++)
            {
                for (int c = 0; c < NumCells; c++)
                {
                    grid[r, c] = true;
                }
            }

            this.Invalidate();
        }
    }
}
