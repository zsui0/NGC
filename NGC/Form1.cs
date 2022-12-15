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
        // Ha scalelve van a visual studio, akkor a méret beállítások miatt rossz lehet!

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
            for (int i = 0; i < 100; i++)
            {
                //Thread.Sleep(500);
                int index = rand.Next(fajok.Length);
                NgcLakok.Add(new NGC(NgcLakok.Count,fajok[index],ref rand)); // %-os arányuk még nincs a feladat alapján
            }
            label1.Text = "Átlagos: Kék";
            label2.Text = "Optimista: Zöld";
            label3.Text = "Pesszimista: Piros";

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
                    e.Graphics.FillEllipse(Brushes.Blue, Convert.ToInt32(circle.GetPosX()), Convert.ToInt32(circle.GetPosY()), Convert.ToInt32(circle.GetBallWidth()), Convert.ToInt32(circle.GetBallHeight()));
                else if (circle.GetSzin() == "green")
                    e.Graphics.FillEllipse(Brushes.Green, Convert.ToInt32(circle.GetPosX()), Convert.ToInt32(circle.GetPosY()), Convert.ToInt32(circle.GetBallWidth()), Convert.ToInt32(circle.GetBallHeight()));
                else if (circle.GetSzin() == "red")
                    e.Graphics.FillEllipse(Brushes.Red, Convert.ToInt32(circle.GetPosX()), Convert.ToInt32(circle.GetPosY()), Convert.ToInt32(circle.GetBallWidth()), Convert.ToInt32(circle.GetBallHeight()));
                else
                    e.Graphics.FillEllipse(Brushes.Gray, Convert.ToInt32(circle.GetPosX()), Convert.ToInt32(circle.GetPosY()), Convert.ToInt32(circle.GetBallWidth()), Convert.ToInt32(circle.GetBallHeight()));

                e.Graphics.DrawString(Convert.ToString(Math.Round(circle.GetJokedv(),2)),new Font("Jokedv",10,FontStyle.Regular) ,Brushes.White, Convert.ToInt32(circle.GetPosX()+(circle.GetBallWidth()/2)-15), Convert.ToInt32(circle.GetPosY()+(circle.GetBallHeight()/2)-7));
                e.Graphics.DrawEllipse(Pens.Black, Convert.ToInt32(circle.GetPosX()), Convert.ToInt32(circle.GetPosY()), Convert.ToInt32(circle.GetBallWidth()), Convert.ToInt32(circle.GetBallHeight()));
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

        private void PopFirstNgc(object sender, EventArgs e)
        {
            foreach (NGC item in NgcLakok)
            {
                if (item.CountRecently() > 0)
                {
                    item.PopFirstRecently();
                }
            }
        }
    }

}