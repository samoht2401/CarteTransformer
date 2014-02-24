using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Net;

namespace CarteTransformer
{
    public static class CourantDownloader
    {
        public static readonly int HourStep = 1; //15min

        private static int Index = 1380924; // 5oct2013

        //Made for october only !
        public static void MoveOn(int step)
        {
            Index += step;
        }

        public static String GetImgName()
        {
            return "http://www.previmer.org/services/getResult/theme/courants/appli/l1/var/courant/type/map/date/" + Index + "000000/area/7149/lang/fr/";
        }

        public static Bitmap GetCourant()
        {
            String r = GetImgName();
            WebRequest requestPic = WebRequest.Create(r);
            WebResponse responsePic = requestPic.GetResponse();
            return new Bitmap(Bitmap.FromStream(responsePic.GetResponseStream()));
        }
    }
}
