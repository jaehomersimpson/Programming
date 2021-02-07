using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Windows.Forms;
using Point = System.Drawing.Point;
using Pen = System.Drawing.Pen;
using Rectangle = System.Drawing.Rectangle;
using Color = System.Drawing.Color;

namespace Tetris
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Forms1 : Form
    {
        Game game;
        int bx;
        int by;
        int bwidth;
        int bheight;

        public Forms1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = Game.Singleton;
            bx = GameRule.BX;
            by = GameRule.BY;
            bwidth = GameRule.B_WIDTH;
            bheight = GameRule.B_HEIGHT;
            base.SetClientSizeCore(bx * bwidth, by * bheight);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawGraduation(e.Graphics);
            DrawDiagram(e.Graphics);
        }

        private void DrawDiagram(Graphics graphics)
        {
            Pen dpen = new Pen(Color.Red, 4);
            Point now = game.NowPosition;
            Rectangle now_rt = new Rectangle(now.X * bwidth + 2, now.Y * bheight + 2, bwidth - 4, bheight - 4);
            graphics.DrawRectangle(dpen, now_rt);
        }

        private void DrawGraduation(Graphics graphics)
        {
            DrawHorizons(graphics);
            DrawVerticals(graphics);
        }

        private void DrawVerticals(Graphics graphics)
        {
            Point st = new Point();
            Point et = new Point();
            for (int cx = 0; cx < bx; cx++)
            {
                st.X = cx * bwidth;
                st.Y = 0;
                et.X = st.X;
                et.Y = by * bheight;
                graphics.DrawLine(Pens.Purple, st, et);
            }
        }

        private void DrawHorizons(Graphics graphics)
        {
            Point st = new Point();
            Point et = new Point();
            for (int cy = 0; cy < by; cy++)
            {
                st.X = 0;
                st.Y = cy * bheight;
                et.X = bx * bwidth;
                et.Y = st.Y;
                graphics.DrawLine(Pens.Green, st, et);
            }
        }

        private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right: MoveRight(); return;
                case Keys.Left: MoveLeft(); return;
                case Keys.Space: MoveDown(); return;
                case Keys.Up: MoveTurn(); return;
            }
        }

        private void MoveTurn()
        {

        }

        private void MoveDown()
        {
            if (game.MoveDown())
            {
                Region rg = MakeRegion(0, -1);
                base.Invalidate(rg);
            }
        }

        private void MoveLeft()
        {
            if (game.MoveLeft())
            {
                Region rg = MakeRegion(1, 0);
                base.Invalidate(rg);
            }
        }

        private void MoveRight()
        {
            if (game.MoveRight())
            {
                Region rg = MakeRegion(-1, 0);
                base.Invalidate(rg);
            }
        }

        private Region MakeRegion(int cx, int cy)
        {
            Point now = game.NowPosition;
            Rectangle rect1 = new Rectangle(now.X * bwidth, now.Y * bheight, bwidth, bheight);
            Rectangle rect2 = new Rectangle((now.X + cx) * bwidth, (now.Y + cy) * bheight, bwidth, bheight);
            Region rg1 = new Region(rect1);
            Region rg2 = new Region(rect2);
            rg1.Union(rg2);
            return rg1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveDown();
        }


    }
}

