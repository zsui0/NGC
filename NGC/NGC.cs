using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NGC
{
    internal class NGC
    {
        string fajta;
        string szin;
        int jokedv;

        int ballWidth = 50;
        int ballHeight = 50;
        int ballPosX = 0;
        int ballPosY = 0;
        int moveSpeedX = 4;
        int moveSpeedY = 4;


        public NGC(string faj,ref Random r)
        {
            if (faj == "atlagos")
            {
                szin = "blue";
                fajta = "atlagos";
                jokedv = r.Next(0, 101);
            }
            else if (faj == "optimista")
            {
                szin = "green";
                fajta = "optimista";
                jokedv = r.Next(60, 101);
            }
            else if (faj == "pesszimista")
            {
                szin = "red";
                fajta = "pesszimista";
                jokedv = r.Next(0, 21);
            }
            else {
                szin = "grey";
                fajta = "semmi";
                jokedv = r.Next(0, 101);
            }

            ballPosX = r.Next(200, 500);
            ballPosY = r.Next(200, 400);
            moveSpeedX = r.Next(-6, 6);
            moveSpeedY = r.Next(-6, 6);
            if (moveSpeedX == 0 && moveSpeedY == 0)
            {
                moveSpeedX = r.Next(1, 6);
                moveSpeedY = r.Next(1, 6);
            }

        }

        public void Collided(NGC n)
        {

            double Y1 = n.GetPosY()+(n.GetBallHeight()/2);
            double X1 = n.GetPosX()+(n.GetBallWidth()/2);

            double Y2 = ballPosY + (ballHeight / 2);
            double X2 = ballPosX + (ballWidth / 2);


            double distance = Math.Sqrt(Math.Pow(Math.Abs(Y1-Y2),2)+Math.Pow(Math.Abs(X1-X2),2));

            if (distance <= Convert.ToDouble((ballWidth/2) + (n.GetBallWidth()/2)))
            {
                jokedv++;
                moveSpeedX = -moveSpeedX;
                moveSpeedY = -moveSpeedY;

                n.IncreaseJokedv();
                n.SetSpeedX(-n.GetSpeedX());
                n.SetSpeedY(-n.GetSpeedY());  
            }

        }

        public void IncreaseJokedv()
        {
            jokedv++;
        }

        public int GetJokedv()
        {
            return jokedv;
        }

        public int Radius()
        {
            return ballWidth / 2;
        }

        public string GetFaj()
        {
            return fajta;
        }

        public string GetSzin()
        {
            return szin;
        }

        public void SetSpeedX(int x)
        {
            moveSpeedX = x;
        }
        public void SetSpeedY(int y)
        {
            moveSpeedY = y;
        }

        public void AddPosX(int x)
        {
            ballPosX += x; 
        }
        public void AddPosY(int y)
        {
            ballPosY += y;
        }

        public int GetBallWidth()
        {
            return ballWidth;
        }
        public int GetBallHeight()
        {
            return ballHeight;
        }
        public int GetPosX()
        {
            return ballPosX;
        }
        public int GetPosY()
        {
            return ballPosY;
        }
        public int GetSpeedX()
        {
            return moveSpeedX;
        }
        public int GetSpeedY()
        {
            return moveSpeedY;
        }
    }
}
