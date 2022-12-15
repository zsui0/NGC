using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace NGC
{
    public partial class Form1 : Form
    {
        List<NGC> NgcLakok = new List<NGC>();

        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] fajok = new string[3] { "atlagos", "optimista", "pesszimista" };
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                //Thread.Sleep(500);
                int index = rand.Next(fajok.Length);
                NgcLakok.Add(new NGC(fajok[index],ref rand)); // %-os arányuk még nincs a feladat alapján
            }
            label2.Text = NgcLakok.Count.ToString();

        }

        private void Form1_Click(object sender, EventArgs e)
        {


        }



        private void PaintCircle(object sender, PaintEventArgs e)
        {

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            e.Graphics.Clear(BackColor);
            foreach (NGC circle in NgcLakok)
            {
                if (circle.GetSzin() == "blue")
                    e.Graphics.FillEllipse(Brushes.Blue, circle.GetPosX(), circle.GetPosY(), circle.GetBallWidth(), circle.GetBallHeight());
                else if (circle.GetSzin() == "green")
                    e.Graphics.FillEllipse(Brushes.Green, circle.GetPosX(), circle.GetPosY(), circle.GetBallWidth(), circle.GetBallHeight());
                else if (circle.GetSzin() == "red")
                    e.Graphics.FillEllipse(Brushes.Red, circle.GetPosX(), circle.GetPosY(), circle.GetBallWidth(), circle.GetBallHeight());
                else
                    e.Graphics.FillEllipse(Brushes.Gray, circle.GetPosX(), circle.GetPosY(), circle.GetBallWidth(), circle.GetBallHeight());

                e.Graphics.DrawString(Convert.ToString(circle.GetJokedv()),new Font("Jokedv",10,FontStyle.Regular) ,Brushes.White,circle.GetPosX()+(circle.GetBallWidth()/2),circle.GetPosY()+(circle.GetBallHeight()/2));
                e.Graphics.DrawEllipse(Pens.Black, circle.GetPosX(), circle.GetPosY(), circle.GetBallWidth(), circle.GetBallHeight());
            }
        }

        private void MoveBall(object sender, EventArgs e) //Tick Timer
        {
            //update coordinates
            foreach (NGC circle in NgcLakok)
            {
                circle.AddPosX(circle.GetSpeedX());
                if (circle.GetPosX() < 0 || circle.GetPosX() + circle.GetBallWidth() > this.ClientSize.Width)
                { 
                    circle.SetSpeedX(-circle.GetSpeedX());
                }


                circle.AddPosY(circle.GetSpeedY());
                if (circle.GetPosY() < 0 || circle.GetPosY() + circle.GetBallHeight() > this.ClientSize.Height)
                {
                    circle.SetSpeedY(-circle.GetSpeedY());
                }
            }

            for (int i = 0; i < NgcLakok.Count; i++)
            {
                for (int j = 0; j < NgcLakok.Count; j++)
                {
                    if (j != i)
                    {
                        NgcLakok[i].Collided(NgcLakok[j]);
                    }
                }
            }

            //update paintings
            Refresh();
        }
        private void BallCollision(object sender, EventArgs e)
        {


        }

    }

}