using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NGC
{
    public partial class Form1 : Form
    {
        bool end = false;
        List<NGC> NgcLakok = new List<NGC>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] fajok = new string[3]{"atlagos","optimista","pesszimista" };      
            Random rand = new Random();
            for (int i = 0; i < 50; i++)
            {
                int index = rand.Next(fajok.Length);
                NgcLakok.Add(new NGC(fajok[index])); // %-os arányuk még nincs a feladat alapján
            }

        }

        private void Form1_Click(object sender, EventArgs e)
        {
            while (true)
            { 
                _Update();
            }
            
        }


        private void _Update()
        {
            while(!end)
            {
                foreach (NGC n in NgcLakok)
                {
                    MoveRectangle(n);
                    DrawRectangle(n);
                }
            }
        }


        private void MoveRectangle(NGC n)
        {
            Random r = new Random();
            double distance = r.Next(50,400);
            double angleRadians = (Math.PI * (r.Next(0, 30) / 180.0));
            n.SetRectangleX((int)((double)n.GetRectangle().X - (Math.Cos(angleRadians) * distance)));
            n.SetRectangleY((int)((double)n.GetRectangle().Y - (Math.Sin(angleRadians) * distance)));

            
            
        }
        
        private void DrawRectangle(NGC n)
        {
            Graphics g = GameBoard.CreateGraphics();
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                if (n.GetFaj() == "atlagos")
                {
                    g.FillEllipse(new SolidBrush(Color.Blue), n.GetRectangle());
                    
                }
                if (n.GetFaj() == "optimista")
                {
                    g.FillEllipse(new SolidBrush(Color.Green), n.GetRectangle());
                }
                if (n.GetFaj() == "pesszimista")
                {
                    g.FillEllipse(new SolidBrush(Color.Red), n.GetRectangle());
                }

            }
        }
}

