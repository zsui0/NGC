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
        int id;
        string fajta;
        string szin;
        double jokedv;

        double ballWidth = 50;
        double ballHeight = 50;
        double ballPosX = 0;
        double ballPosY = 0;
        double moveSpeedX = 4;
        double moveSpeedY = 4;


        List<NGC> recentlyCollidedList;

        public NGC(int index,string faj,ref Random r)
        {
            if (faj == "atlagos")
            {
                szin = "blue";
                fajta = "atlagos";
                jokedv = r.Next(1, 101);
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
                jokedv = r.Next(1, 21);
            }
            else {
                szin = "grey";
                fajta = "semmi";
                jokedv = r.Next(1, 101);
            }

            ballPosX = r.Next(100, 1000);
            ballPosY = r.Next(100, 600);
            moveSpeedX = r.Next(-6, 6);
            moveSpeedY = r.Next(-6, 6);
            if (moveSpeedX == 0 && moveSpeedY == 0)
            {
                moveSpeedX = r.Next(1, 6);
                moveSpeedY = r.Next(1, 6);
            }
            id = index;
            recentlyCollidedList = new List<NGC>();
        }

        public void Collided(NGC n)
        {

            double Y1 = n.GetPosY()+(n.GetBallHeight()/2);
            double X1 = n.GetPosX()+(n.GetBallWidth()/2);

            double Y2 = ballPosY + (ballHeight / 2);
            double X2 = ballPosX + (ballWidth / 2);


            double distance = Math.Sqrt(Math.Pow(Math.Abs(Y1-Y2),2)+Math.Pow(Math.Abs(X1-X2),2));

            


            bool van = false;
            if (recentlyCollidedList.Count > 0)
            { 
                    foreach (NGC item in recentlyCollidedList)
                    {
                        if (n == item)
                            van = true;
                    }
            }

            if (!van)
            {
                if (distance <= Convert.ToDouble((ballWidth / 2) + (n.GetBallWidth() / 2)))
                {
                    if (fajta == "optimista" && n.GetFaj() == "optimista" || n.GetFaj() == "atlagos" || n.GetFaj() == "pesszimista")
                    {
                        n.SetJokedv(jokedv);
                    }
                    else if (fajta == "atlagos" && n.GetFaj() == "optimista" || n.GetFaj() == "atlagos" || n.GetFaj() == "pesszimista")
                    {
                        n.SetJokedv((jokedv + n.GetJokedv()) / 2);
                    }
                    else if (fajta == "pesszimista" && n.GetFaj() == "optmista")
                    {
                        n.SetJokedv(jokedv / 2);
                    }
                    else if (fajta == "pesszimista" && n.GetFaj() == "atlagos" || n.GetFaj() == "pesszimista")
                    {
                        n.SetJokedv(jokedv);
                    }


                    recentlyCollidedList.Add(n);
                    moveSpeedY = -moveSpeedY;
                    moveSpeedX = -moveSpeedX;

                    n.SetSpeedX(-n.GetSpeedX());
                    n.SetSpeedY(-n.GetSpeedY());
                }
            }

        }

        public void SetJokedv(double _jokedv)
        {
            jokedv = _jokedv;
        }

        public int CountRecently()
        {
            return recentlyCollidedList.Count;
        }

        public void PopFirstRecently()
        {
            recentlyCollidedList.RemoveAt(0);
        }

        public void IncreaseJokedv()
        {
            jokedv++;
        }

        public double GetJokedv()
        {
            return jokedv;
        }

        public double Radius()
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

        public void SetSpeedX(double x)
        {
            moveSpeedX = x;
        }
        public void SetSpeedY(double y)
        {
            moveSpeedY = y;
        }

        public void AddPosX(double x)
        {
            ballPosX += x; 
        }
        public void AddPosY(double y)
        {
            ballPosY += y;
        }

        public double GetBallWidth()
        {
            return ballWidth;
        }
        public double GetBallHeight()
        {
            return ballHeight;
        }
        public double GetPosX()
        {
            return ballPosX;
        }
        public double GetPosY()
        {
            return ballPosY;
        }
        public double GetSpeedX()
        {
            return moveSpeedX;
        }
        public double GetSpeedY()
        {
            return moveSpeedY;
        }
    }
}
