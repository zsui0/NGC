using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NGC
{
    internal class NGC
    {
        string fajta;
        int jokedv;
        Rectangle Rectangle;

        public NGC(string faj)
        {
            Random r = new Random();
            if (faj == "atlagos")
            {
                fajta = "atlagos";
                jokedv = r.Next(0, 101);
            }
            else if (faj == "optimista")
            {
                fajta = "optimista";
                jokedv = r.Next(60, 101);
            }
            else if (faj == "pesszimista")
            {
                fajta = "pesszimista";
                jokedv = r.Next(0, 21);
            }
            else {
                fajta = "semmi";
                jokedv = r.Next(0, 101);
            }


            Rectangle.X = r.Next(10, 951);
            Rectangle.Y = r.Next(10, 951);
            Rectangle.Width = 10;
            Rectangle.Height = 10;
        }

        public Rectangle GetRectangle()
        {
            return Rectangle;
        }

        public void SetRectangleX(int x)
        { 
            Rectangle.X = x;    
        }

        public void SetRectangleY(int y)
        {
            Rectangle.Y = y;
        }

        public string GetFaj()
        {
            return fajta;
        }
    }
}
