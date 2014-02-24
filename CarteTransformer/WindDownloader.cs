using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Net;

namespace CarteTransformer
{
    public static class WindDownloader
    {
        public static readonly int HourStep = 12; //3h

        public static int Year = 2013;
        public static int Mounth = 10;
        public static int Day = 5;
        public static int Hour = 0;

        //Made for october only !
        private static int internalStep = 0;
        public static void MoveOn(int step)
        {
            internalStep += step;
            if (internalStep >= HourStep)
            {
                internalStep = 0;
                Hour += HourStep / 4;
                if (Hour > 24)
                {
                    Hour -= 24;
                    Day += 1;
                    if (Day > 31)
                    {
                        Day -= 31;
                        Mounth += 1;
                    }

                }
            }
        }

        public static String GetImgName()
        {
            return "http://modeles2.meteociel.fr/modeles_gfs/archives/" + Year + Mounth.ToString("00") + Day.ToString("00") + "00/" + (Hour != 0 ? Hour.ToString() : "24") + "-602.GIF";
        }

        public static Bitmap GetWind()
        {
            String r = GetImgName();
            WebRequest requestPic = WebRequest.Create(r);
            WebResponse responsePic = requestPic.GetResponse();
            return new Bitmap(Bitmap.FromStream(responsePic.GetResponseStream()));
        }
    }
}
