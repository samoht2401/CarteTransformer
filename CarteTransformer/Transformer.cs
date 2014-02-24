using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace CarteTransformer
{
    public class Transformer
    {
        public Dictionary<AverageColor, float> EchelleEau;
        public Dictionary<AverageColor, float> EchelleAir;
        public Dictionary<Color, Point> RedToWindPos;
        public List<AverageColor> SeuilEau;
        public List<AverageColor> SeuilAir;

        private int outputinc = 0;

        public Bitmap MaskBitmap { get; protected set; }
        public Bitmap OriginEauBitmap { get; protected set; }
        //public String OriginDirectory { get { return Directory.GetCurrentDirectory() + "\\Img\\"; } }
        //public String OriginEauURI { get; set; }
        public Bitmap OutputBitmap { get; protected set; }
        public String OutputURI { get { return Directory.GetCurrentDirectory() + "\\Output\\" + "output" + outputinc + ".png"; } }
        public Bitmap OriginAirBitmap { get; protected set; }

        private Dictionary<String, Bitmap> Horloges;
        private Bitmap HorlogeAM;
        private Bitmap HorlogePM;

        private Bitmap lune;

        private List<Point> BluePoint;
        private Point StartPoint;
        private Point EndPoint;
        private Point LuneStartPoint;
        private Point LuneEndPoint;
        private Point HorlogeStartPoint;
        private Point HorlogeMiddlePoint;
        private Point HorlogeEndPoint;

        public String BluePointVitesse = "";

        public Transformer()
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Output\\");
            MaskBitmap = new Bitmap(Directory.GetCurrentDirectory() + "\\Img\\" + "mask.png");
            OriginEauBitmap = new Bitmap(1, 1);
            OutputBitmap = (Bitmap)MaskBitmap.Clone();
            OriginAirBitmap = new Bitmap(1, 1);


            EchelleEau = new Dictionary<AverageColor, float>();
            EchelleEau.Add(new AverageColor(Color.FromArgb(247, 241, 255)), 0.0f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(235, 217, 255)), 0.1f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(224, 195, 255)), 0.2f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(211, 171, 255)), 0.3f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(206, 160, 255)), 0.4f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(192, 157, 255)), 0.5f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(155, 155, 255)), 0.6f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(125, 171, 255)), 0.7f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(117, 208, 255)), 0.8f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(109, 231, 255)), 0.9f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(95, 255, 238)), 1.0f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(79, 255, 205)), 1.2f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(71, 255, 169)), 1.4f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(63, 255, 132)), 1.6f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(47, 255, 72)), 1.8f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(86, 255, 33)), 2.0f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(121, 255, 25)), 2.2f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(159, 255, 17)), 2.4f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(198, 255, 1)), 2.6f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(255, 243, 1)), 2.8f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(255, 198, 1)), 3.0f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(255, 152, 1)), 3.2f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(255, 107, 1)), 3.4f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(255, 61, 1)), 3.6f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(241, 0, 0)), 3.8f);
            EchelleEau.Add(new AverageColor(Color.FromArgb(194, 0, 0)), 4.0f);


            EchelleAir = new Dictionary<AverageColor, float>();
            EchelleAir.Add(new AverageColor(Color.FromArgb(255, 255, 255)), 5.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(151, 230, 255)), 10.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(51, 204, 255)), 15.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(0, 153, 255)), 20.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(0, 255, 153)), 25.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(51, 204, 102)), 30.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(102, 204, 51)), 35.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(102, 255, 0)), 40.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(164, 242, 47)), 45.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(183, 207, 14)), 50.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(214, 240, 23)), 55.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(204, 153, 0)), 60.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(255, 153, 0)), 65.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(255, 153, 102)), 70.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(204, 153, 153)), 75.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(204, 102, 51)), 80.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(204, 51, 51)), 85.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(255, 13, 13)), 90.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(198, 0, 0)), 100.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(128, 0, 0)), 110.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(97, 0, 0)), 120.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(69, 0, 0)), 130.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(69, 0, 48)), 140.0f / 3.6f);
            EchelleAir.Add(new AverageColor(Color.FromArgb(69, 0, 96)), 150.0f / 3.6f);


            RedToWindPos = new Dictionary<Color, Point>();
            RedToWindPos.Add(Color.FromArgb(255, 0, 0), new Point(126, 298));
            RedToWindPos.Add(Color.FromArgb(254, 0, 0), new Point(127, 298));
            RedToWindPos.Add(Color.FromArgb(253, 0, 0), new Point(129, 300));
            RedToWindPos.Add(Color.FromArgb(252, 0, 0), new Point(130, 302));
            RedToWindPos.Add(Color.FromArgb(251, 0, 0), new Point(133, 303));

            RedToWindPos.Add(Color.FromArgb(250, 0, 0), new Point(147, 294));
            RedToWindPos.Add(Color.FromArgb(249, 0, 0), new Point(144, 294));
            RedToWindPos.Add(Color.FromArgb(248, 0, 0), new Point(143, 294));
            RedToWindPos.Add(Color.FromArgb(247, 0, 0), new Point(143, 294));
            RedToWindPos.Add(Color.FromArgb(246, 0, 0), new Point(142, 294));
            RedToWindPos.Add(Color.FromArgb(245, 0, 0), new Point(142, 294));

            RedToWindPos.Add(Color.FromArgb(244, 0, 0), new Point(144, 295));
            RedToWindPos.Add(Color.FromArgb(243, 0, 0), new Point(144, 295));
            RedToWindPos.Add(Color.FromArgb(242, 0, 0), new Point(143, 295));
            RedToWindPos.Add(Color.FromArgb(241, 0, 0), new Point(142, 295));
            RedToWindPos.Add(Color.FromArgb(240, 0, 0), new Point(142, 295));
            RedToWindPos.Add(Color.FromArgb(239, 0, 0), new Point(141, 295));
            RedToWindPos.Add(Color.FromArgb(238, 0, 0), new Point(140, 295));
            RedToWindPos.Add(Color.FromArgb(237, 0, 0), new Point(140, 295));

            RedToWindPos.Add(Color.FromArgb(236, 0, 0), new Point(147, 296));
            RedToWindPos.Add(Color.FromArgb(235, 0, 0), new Point(146, 296));
            RedToWindPos.Add(Color.FromArgb(234, 0, 0), new Point(144, 296));
            RedToWindPos.Add(Color.FromArgb(233, 0, 0), new Point(143, 296));
            RedToWindPos.Add(Color.FromArgb(232, 0, 0), new Point(143, 296));
            RedToWindPos.Add(Color.FromArgb(231, 0, 0), new Point(142, 296));
            RedToWindPos.Add(Color.FromArgb(230, 0, 0), new Point(141, 296));
            RedToWindPos.Add(Color.FromArgb(229, 0, 0), new Point(140, 296));
            RedToWindPos.Add(Color.FromArgb(228, 0, 0), new Point(139, 296));

            RedToWindPos.Add(Color.FromArgb(227, 0, 0), new Point(147, 297));
            RedToWindPos.Add(Color.FromArgb(226, 0, 0), new Point(146, 297));
            RedToWindPos.Add(Color.FromArgb(225, 0, 0), new Point(144, 297));
            RedToWindPos.Add(Color.FromArgb(224, 0, 0), new Point(143, 297));
            RedToWindPos.Add(Color.FromArgb(223, 0, 0), new Point(143, 297));
            RedToWindPos.Add(Color.FromArgb(222, 0, 0), new Point(142, 297));
            RedToWindPos.Add(Color.FromArgb(221, 0, 0), new Point(142, 297));
            RedToWindPos.Add(Color.FromArgb(220, 0, 0), new Point(141, 297));
            RedToWindPos.Add(Color.FromArgb(219, 0, 0), new Point(140, 297));
            RedToWindPos.Add(Color.FromArgb(218, 0, 0), new Point(139, 297));

            RedToWindPos.Add(Color.FromArgb(217, 0, 0), new Point(147, 298));
            RedToWindPos.Add(Color.FromArgb(216, 0, 0), new Point(146, 298));
            RedToWindPos.Add(Color.FromArgb(215, 0, 0), new Point(145, 298));
            RedToWindPos.Add(Color.FromArgb(214, 0, 0), new Point(144, 298));
            RedToWindPos.Add(Color.FromArgb(213, 0, 0), new Point(143, 298));
            RedToWindPos.Add(Color.FromArgb(212, 0, 0), new Point(143, 298));
            RedToWindPos.Add(Color.FromArgb(211, 0, 0), new Point(142, 298));
            RedToWindPos.Add(Color.FromArgb(210, 0, 0), new Point(142, 298));
            RedToWindPos.Add(Color.FromArgb(209, 0, 0), new Point(141, 298));
            RedToWindPos.Add(Color.FromArgb(208, 0, 0), new Point(140, 298));

            RedToWindPos.Add(Color.FromArgb(207, 0, 0), new Point(147, 299));
            RedToWindPos.Add(Color.FromArgb(206, 0, 0), new Point(146, 299));
            RedToWindPos.Add(Color.FromArgb(205, 0, 0), new Point(145, 299));
            RedToWindPos.Add(Color.FromArgb(204, 0, 0), new Point(144, 299));
            RedToWindPos.Add(Color.FromArgb(203, 0, 0), new Point(143, 299));
            RedToWindPos.Add(Color.FromArgb(202, 0, 0), new Point(143, 299));
            RedToWindPos.Add(Color.FromArgb(201, 0, 0), new Point(142, 299));
            RedToWindPos.Add(Color.FromArgb(200, 0, 0), new Point(141, 299));
            RedToWindPos.Add(Color.FromArgb(199, 0, 0), new Point(140, 299));
            RedToWindPos.Add(Color.FromArgb(198, 0, 0), new Point(139, 299));

            RedToWindPos.Add(Color.FromArgb(197, 0, 0), new Point(147, 300));
            RedToWindPos.Add(Color.FromArgb(196, 0, 0), new Point(146, 300));
            RedToWindPos.Add(Color.FromArgb(195, 0, 0), new Point(145, 300));
            RedToWindPos.Add(Color.FromArgb(194, 0, 0), new Point(144, 300));
            RedToWindPos.Add(Color.FromArgb(193, 0, 0), new Point(143, 300));
            RedToWindPos.Add(Color.FromArgb(192, 0, 0), new Point(142, 300));
            RedToWindPos.Add(Color.FromArgb(191, 0, 0), new Point(141, 300));
            RedToWindPos.Add(Color.FromArgb(190, 0, 0), new Point(140, 300));
            RedToWindPos.Add(Color.FromArgb(189, 0, 0), new Point(139, 300));
            RedToWindPos.Add(Color.FromArgb(188, 0, 0), new Point(138, 300));

            RedToWindPos.Add(Color.FromArgb(187, 0, 0), new Point(147, 301));
            RedToWindPos.Add(Color.FromArgb(186, 0, 0), new Point(146, 301));
            RedToWindPos.Add(Color.FromArgb(185, 0, 0), new Point(145, 301));
            RedToWindPos.Add(Color.FromArgb(184, 0, 0), new Point(144, 301));
            RedToWindPos.Add(Color.FromArgb(183, 0, 0), new Point(143, 301));
            RedToWindPos.Add(Color.FromArgb(182, 0, 0), new Point(142, 301));
            RedToWindPos.Add(Color.FromArgb(181, 0, 0), new Point(141, 301));
            RedToWindPos.Add(Color.FromArgb(180, 0, 0), new Point(140, 301));
            RedToWindPos.Add(Color.FromArgb(179, 0, 0), new Point(139, 301));
            RedToWindPos.Add(Color.FromArgb(178, 0, 0), new Point(138, 301));

            RedToWindPos.Add(Color.FromArgb(177, 0, 0), new Point(147, 302));
            RedToWindPos.Add(Color.FromArgb(176, 0, 0), new Point(146, 302));
            RedToWindPos.Add(Color.FromArgb(175, 0, 0), new Point(145, 302));
            RedToWindPos.Add(Color.FromArgb(174, 0, 0), new Point(144, 302));
            RedToWindPos.Add(Color.FromArgb(173, 0, 0), new Point(143, 302));
            RedToWindPos.Add(Color.FromArgb(172, 0, 0), new Point(142, 302));
            RedToWindPos.Add(Color.FromArgb(171, 0, 0), new Point(141, 302));
            RedToWindPos.Add(Color.FromArgb(170, 0, 0), new Point(140, 302));
            RedToWindPos.Add(Color.FromArgb(169, 0, 0), new Point(139, 302));
            RedToWindPos.Add(Color.FromArgb(168, 0, 0), new Point(138, 302));

            RedToWindPos.Add(Color.FromArgb(167, 0, 0), new Point(147, 303));
            RedToWindPos.Add(Color.FromArgb(166, 0, 0), new Point(146, 303));
            RedToWindPos.Add(Color.FromArgb(165, 0, 0), new Point(145, 303));
            RedToWindPos.Add(Color.FromArgb(164, 0, 0), new Point(144, 303));
            RedToWindPos.Add(Color.FromArgb(163, 0, 0), new Point(143, 303));
            RedToWindPos.Add(Color.FromArgb(162, 0, 0), new Point(142, 303));
            RedToWindPos.Add(Color.FromArgb(161, 0, 0), new Point(141, 303));
            RedToWindPos.Add(Color.FromArgb(160, 0, 0), new Point(140, 303));
            RedToWindPos.Add(Color.FromArgb(159, 0, 0), new Point(139, 303));
            RedToWindPos.Add(Color.FromArgb(158, 0, 0), new Point(138, 303));

            RedToWindPos.Add(Color.FromArgb(157, 0, 0), new Point(147, 304));
            RedToWindPos.Add(Color.FromArgb(156, 0, 0), new Point(145, 304));
            RedToWindPos.Add(Color.FromArgb(155, 0, 0), new Point(145, 304));
            RedToWindPos.Add(Color.FromArgb(154, 0, 0), new Point(144, 304));
            RedToWindPos.Add(Color.FromArgb(153, 0, 0), new Point(143, 304));
            RedToWindPos.Add(Color.FromArgb(152, 0, 0), new Point(142, 304));
            RedToWindPos.Add(Color.FromArgb(151, 0, 0), new Point(142, 304));
            RedToWindPos.Add(Color.FromArgb(150, 0, 0), new Point(139, 304));
            RedToWindPos.Add(Color.FromArgb(149, 0, 0), new Point(139, 304));
            RedToWindPos.Add(Color.FromArgb(148, 0, 0), new Point(138, 304));

            RedToWindPos.Add(Color.FromArgb(147, 0, 0), new Point(144, 305));
            RedToWindPos.Add(Color.FromArgb(146, 0, 0), new Point(144, 305));
            RedToWindPos.Add(Color.FromArgb(145, 0, 0), new Point(144, 305));
            RedToWindPos.Add(Color.FromArgb(144, 0, 0), new Point(142, 305));
            RedToWindPos.Add(Color.FromArgb(143, 0, 0), new Point(142, 305));
            RedToWindPos.Add(Color.FromArgb(142, 0, 0), new Point(142, 305));
            RedToWindPos.Add(Color.FromArgb(141, 0, 0), new Point(140, 305));
            RedToWindPos.Add(Color.FromArgb(140, 0, 0), new Point(140, 305));
            RedToWindPos.Add(Color.FromArgb(139, 0, 0), new Point(139, 305));
            RedToWindPos.Add(Color.FromArgb(138, 0, 0), new Point(139, 305));

            RedToWindPos.Add(Color.FromArgb(137, 0, 0), new Point(144, 305));
            RedToWindPos.Add(Color.FromArgb(136, 0, 0), new Point(142, 305));
            RedToWindPos.Add(Color.FromArgb(135, 0, 0), new Point(142, 305));
            RedToWindPos.Add(Color.FromArgb(134, 0, 0), new Point(141, 306));
            RedToWindPos.Add(Color.FromArgb(133, 0, 0), new Point(140, 305));
            RedToWindPos.Add(Color.FromArgb(132, 0, 0), new Point(139, 305));
            RedToWindPos.Add(Color.FromArgb(131, 0, 0), new Point(139, 305));

            RedToWindPos.Add(Color.FromArgb(130, 0, 0), new Point(146, 306));
            RedToWindPos.Add(Color.FromArgb(129, 0, 0), new Point(146, 306));
            RedToWindPos.Add(Color.FromArgb(128, 0, 0), new Point(146, 306));

            RedToWindPos.Add(Color.FromArgb(127, 0, 0), new Point(146, 306));
            RedToWindPos.Add(Color.FromArgb(126, 0, 0), new Point(146, 306));

            RedToWindPos.Add(Color.FromArgb(125, 0, 0), new Point(144, 310));
            RedToWindPos.Add(Color.FromArgb(124, 0, 0), new Point(144, 310));
            RedToWindPos.Add(Color.FromArgb(123, 0, 0), new Point(143, 310));
            RedToWindPos.Add(Color.FromArgb(122, 0, 0), new Point(143, 310));

            RedToWindPos.Add(Color.FromArgb(121, 0, 0), new Point(144, 310));
            RedToWindPos.Add(Color.FromArgb(120, 0, 0), new Point(144, 310));
            RedToWindPos.Add(Color.FromArgb(119, 0, 0), new Point(143, 310));
            RedToWindPos.Add(Color.FromArgb(118, 0, 0), new Point(143, 310));


            SeuilEau = new List<AverageColor>();
            SeuilEau.Add(new AverageColor(Color.FromArgb(247, 241, 255)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(235, 217, 255)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(224, 195, 255)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(211, 171, 255)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(206, 160, 255)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(192, 157, 255)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(155, 155, 255)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(125, 171, 255)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(117, 208, 255)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(109, 231, 255)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(95, 255, 238)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(79, 255, 205)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(71, 255, 169)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(63, 255, 132)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(47, 255, 72)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(86, 255, 33)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(121, 255, 25)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(159, 255, 17)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(198, 255, 1)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(255, 243, 1)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(255, 198, 1)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(255, 152, 1)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(255, 107, 1)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(255, 61, 1)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(241, 0, 0)));
            SeuilEau.Add(new AverageColor(Color.FromArgb(194, 0, 0)));


            SeuilAir = new List<AverageColor>();
            //SeuilAir.Add(new AverageColor(Color.FromArgb(247, 241, 255)));


            Horloges = new Dictionary<String, Bitmap>();
            Horloges.Add("03", new Bitmap(Directory.GetCurrentDirectory() + "\\Img\\" + "horloge03AM.png"));
            Horloges.Add("06", new Bitmap(Directory.GetCurrentDirectory() + "\\Img\\" + "horloge06AM.png"));
            Horloges.Add("09", new Bitmap(Directory.GetCurrentDirectory() + "\\Img\\" + "horloge09AM.png"));
            Horloges.Add("12", new Bitmap(Directory.GetCurrentDirectory() + "\\Img\\" + "horloge12AM.png"));
            Horloges.Add("15", new Bitmap(Directory.GetCurrentDirectory() + "\\Img\\" + "horloge03PM.png"));
            Horloges.Add("18", new Bitmap(Directory.GetCurrentDirectory() + "\\Img\\" + "horloge06PM.png"));
            Horloges.Add("21", new Bitmap(Directory.GetCurrentDirectory() + "\\Img\\" + "horloge09PM.png"));
            Horloges.Add("00", new Bitmap(Directory.GetCurrentDirectory() + "\\Img\\" + "horloge12PM.png"));

            HorlogeAM = new Bitmap(Directory.GetCurrentDirectory() + "\\Img\\" + "horlogeAM.png");
            HorlogePM = new Bitmap(Directory.GetCurrentDirectory() + "\\Img\\" + "horlogePM.png");

            lune = new Bitmap(Directory.GetCurrentDirectory() + "\\Img\\1005.png");

            FindZoneing();
        }

        private void FindZoneing()
        {
            BluePoint = new List<Point>();
            StartPoint = Point.Empty;
            EndPoint = Point.Empty;
            /*HorlogeStartPoint.X = MaskBitmap.Width / 2 - Horloges["00"].Width / 2;
            HorlogeStartPoint.Y = 60 - Horloges["00"].Height / 2;
            HorlogeEndPoint.X = HorlogeStartPoint.X + Horloges["00"].Width;
            HorlogeEndPoint.Y = HorlogeStartPoint.Y + Horloges["00"].Height;*/
            HorlogeStartPoint.X = MaskBitmap.Width / 2 + 100 - Horloges["00"].Width / 2;
            HorlogeStartPoint.Y = 60 - Horloges["00"].Height / 2;
            HorlogeEndPoint.X = HorlogeStartPoint.X + Horloges["00"].Width;
            HorlogeEndPoint.Y = HorlogeStartPoint.Y + Horloges["00"].Height;
            HorlogeMiddlePoint.X = MaskBitmap.Width / 2 + 100;
            HorlogeMiddlePoint.Y = 60;
            LuneStartPoint.X = MaskBitmap.Width / 2 - 100 - lune.Width / 2;
            LuneStartPoint.Y = 60 - lune.Height / 2;
            LuneEndPoint.X = LuneStartPoint.X + lune.Width;
            LuneEndPoint.Y = LuneStartPoint.Y + lune.Height;
            for (int y = 0; y < MaskBitmap.Height; y++)
            {
                for (int x = 0; x < MaskBitmap.Width; x++)
                {
                    if (MaskBitmap.GetPixel(x, y) == Color.FromArgb(0, 255, 0) ||
                        (MaskBitmap.GetPixel(x, y).R > 117 && MaskBitmap.GetPixel(x, y).G == 0 && MaskBitmap.GetPixel(x, y).B == 0))
                    {
                        if (StartPoint == Point.Empty)
                        {
                            StartPoint.X = x;
                            StartPoint.Y = y;
                        }
                        EndPoint.X = Math.Max(EndPoint.X, x);
                        EndPoint.Y = Math.Max(EndPoint.Y, y);
                    }
                    if (MaskBitmap.GetPixel(x, y) == Color.FromArgb(0, 0, 255))
                        BluePoint.Add(new Point(x, y));
                }
            }
        }

        public void ReloadOriginBitmap()
        {
            OriginAirBitmap = WindDownloader.GetWind();
            OriginEauBitmap = CourantDownloader.GetCourant();
            OutputBitmap = (Bitmap)MaskBitmap.Clone();
            lune = new Bitmap(Directory.GetCurrentDirectory() + "\\Img\\" + WindDownloader.Mounth.ToString("00") + WindDownloader.Day.ToString("00") + ".png");
        }

        private const float LimBeltz = 16f / 27f;
        private const float massVolEau = 1025f;
        private const float rayonEau = 5f;
        private const float min = 0f;
        private const float maxEau = LimBeltz * (1f / 2f) * massVolEau * rayonEau * rayonEau * (float)Math.PI * 4 * 4 * 4;
        private float pallierEau = (float)Math.Pow(maxEau, 1 / 4f) / 26f + 0.01f;

        private const float massVolAir = 1.2f;
        private const float rayonAir = 25f;
        private const float maxAir = LimBeltz * (1f / 2f) * massVolAir * rayonAir * rayonAir * (float)Math.PI * 4 * 4 * 4;
        private float pallierAir = (float)Math.Pow(maxAir, 1 / 4f) / 26f + 0.01f;

        private int TransformCount = 0;

        TimeSpan time = TimeSpan.Zero;
        public void Transform(int step)
        {
            //Add horloge
            Bitmap horloge = HorlogeAM;
            int hour = time.Hours;
            int minute = time.Minutes;
            if (hour >= 12)
            {
                horloge = HorlogePM;
                hour -= 12;
            }
            for (int y = HorlogeStartPoint.Y; y < HorlogeEndPoint.Y; y++)
                for (int x = HorlogeStartPoint.X; x < HorlogeEndPoint.X; x++)
                    OutputBitmap.SetPixel(x, y, horloge.GetPixel(x - HorlogeStartPoint.X, y - HorlogeStartPoint.Y));
            double hourX = Math.Sin((hour + minute / 60.0) / 12.0 * 2 * Math.PI);
            double hourY = -Math.Cos((hour + minute / 60.0) / 12.0 * 2 * Math.PI);
            double minuteX = Math.Sin(minute / 60.0 * 2 * Math.PI);
            double minuteY = -Math.Cos(minute / 60.0 * 2 * Math.PI);
            for (int i = -1; i < 50; i++)
            {
                for (double j = -2; j <= 2; j += 0.5)
                {
                    if (i <= 25)
                    {
                        double ax = HorlogeMiddlePoint.X + hourX * i - hourY * j;
                        double ay = HorlogeMiddlePoint.Y + hourY * i + hourX * j;
                        int x1 = (int)Math.Round(ax);
                        int y1 = (int)Math.Round(ay);
                        double alpha = Math.Abs((x1 - ax + y1 - ay) / 2.0);
                        if (alpha < 0.2)
                            alpha = 0;
                        Color c = OutputBitmap.GetPixel(x1, y1);
                        c = Color.FromArgb((int)(c.R * alpha), (int)(c.G * alpha), (int)(c.B * alpha));
                        OutputBitmap.SetPixel(x1, y1, c);
                    }
                    if (Math.Abs(j) <= 1)
                    {
                        int x = (int)Math.Round(HorlogeMiddlePoint.X + minuteX * i - (i < 49 ? minuteY * j : 0));
                        int y = (int)Math.Round(HorlogeMiddlePoint.Y + minuteY * i + (i < 49 ? minuteX * j : 0));
                        if (y < 0) break;
                        OutputBitmap.SetPixel(x, y, Color.Black);
                    }
                }
            }

            /*if (Horloges.ContainsKey(WindDownloader.Hour.ToString("00")))
                Horloge = Horloges[WindDownloader.Hour.ToString("00")];
            if (Horloge != null)
                for (int y = HorlogeStartPoint.Y; y < HorlogeEndPoint.Y; y++)
                    for (int x = HorlogeStartPoint.X; x < HorlogeEndPoint.X; x++)
                        OutputBitmap.SetPixel(x, y, Horloge.GetPixel(x - HorlogeStartPoint.X, y - HorlogeStartPoint.Y));*/
            //Add lune
            for (int y = LuneStartPoint.Y; y < LuneEndPoint.Y; y++)
                for (int x = LuneStartPoint.X; x < LuneEndPoint.X; x++)
                    OutputBitmap.SetPixel(x, y, lune.GetPixel(x - LuneStartPoint.X, y - LuneStartPoint.Y));
            //Transform (recolor)
            bool blueTook = false;
            for (int y = StartPoint.Y; y <= EndPoint.Y; y++)
            {
                for (int x = StartPoint.X; x <= EndPoint.X; x++)
                {
                    Point p = new Point(x, y);
                    if (!blueTook && BluePoint.Contains(p) && EchelleEau.ContainsKey(new AverageColor(OriginEauBitmap.GetPixel(x - 33, y - 70))))
                    {
                        BluePointVitesse += EchelleEau[new AverageColor(OriginEauBitmap.GetPixel(x - 33, y - 70))] + "\n";
                        blueTook = true;
                    }
                    else if (MaskBitmap.GetPixel(x, y) == Color.FromArgb(0, 255, 0))
                    {
                        AverageColor pixel = new AverageColor(OriginEauBitmap.GetPixel(x - 33, y - 70));
                        if (EchelleEau.ContainsKey(pixel))
                        {
                            float vitesse = EchelleEau[pixel];
                            float newVal = LimBeltz * (1f / 2f) * massVolEau * rayonEau * rayonEau * (float)Math.PI * vitesse * vitesse * vitesse;
                            int niv = (int)newVal;//(int)Math.Floor(Math.Pow(newVal, 1 / 4f) / pallier);
                            //OutputBitmap.SetPixel(x, y, Seuil[niv].Color);

                            if (niv > 1400000)
                                OutputBitmap.SetPixel(x, y, SeuilEau[25].Color);
                            else if (niv > 1200000)
                                OutputBitmap.SetPixel(x, y, SeuilEau[24].Color);
                            else if (niv > 1000000)
                                OutputBitmap.SetPixel(x, y, SeuilEau[23].Color);
                            else if (niv > 800000)
                                OutputBitmap.SetPixel(x, y, SeuilEau[22].Color);
                            else if (niv > 600000)
                                OutputBitmap.SetPixel(x, y, SeuilEau[21].Color);
                            else if (niv > 500000)
                                OutputBitmap.SetPixel(x, y, SeuilEau[20].Color);
                            else if (niv > 400000)
                                OutputBitmap.SetPixel(x, y, SeuilEau[19].Color);
                            else if (niv > 300000)
                                OutputBitmap.SetPixel(x, y, SeuilEau[18].Color);
                            else if (niv > 200000)
                                OutputBitmap.SetPixel(x, y, SeuilEau[17].Color);
                            else if (niv > 100000)
                                OutputBitmap.SetPixel(x, y, SeuilEau[16].Color);
                            else if (niv > 90000)
                                OutputBitmap.SetPixel(x, y, SeuilEau[15].Color);
                            else if (niv > 80000)
                                OutputBitmap.SetPixel(x, y, SeuilEau[14].Color);
                            else if (niv > 70000)
                                OutputBitmap.SetPixel(x, y, SeuilEau[13].Color);
                            else if (niv > 60000)
                                OutputBitmap.SetPixel(x, y, SeuilEau[12].Color);
                            else if (niv > 50000)
                                OutputBitmap.SetPixel(x, y, SeuilEau[11].Color);
                            else if (niv > 40000)
                                OutputBitmap.SetPixel(x, y, SeuilEau[10].Color);
                            else if (niv > 30000)
                                OutputBitmap.SetPixel(x, y, SeuilEau[9].Color);
                            else if (niv > 20000)
                                OutputBitmap.SetPixel(x, y, SeuilEau[8].Color);
                            else if (niv > 10000)
                                OutputBitmap.SetPixel(x, y, SeuilEau[7].Color);
                            else if (niv > 8000)
                                OutputBitmap.SetPixel(x, y, SeuilEau[6].Color);
                            else if (niv > 5000)
                                OutputBitmap.SetPixel(x, y, SeuilEau[5].Color);
                            else if (niv > 2000)
                                OutputBitmap.SetPixel(x, y, SeuilEau[4].Color);
                            else if (niv > 800)
                                OutputBitmap.SetPixel(x, y, SeuilEau[3].Color);
                            else if (niv > 500)
                                OutputBitmap.SetPixel(x, y, SeuilEau[2].Color);
                            else if (niv > 200)
                                OutputBitmap.SetPixel(x, y, SeuilEau[1].Color);
                            else
                                OutputBitmap.SetPixel(x, y, SeuilEau[0].Color);
                        }
                        else if (!pixel.Equals(Color.FromArgb(255, 255, 255)))
                        {
                            Color top = OutputBitmap.GetPixel(x, y - 1);
                            Color left = OutputBitmap.GetPixel(x - 1, y);
                            if (x > StartPoint.X && y > StartPoint.Y)
                                OutputBitmap.SetPixel(x, y, Color.FromArgb((top.R + left.R) / 2, (top.G + left.G) / 2, (top.B + left.B) / 2));
                            else if (x == StartPoint.X)
                                OutputBitmap.SetPixel(x, y, top);
                            else if (y == StartPoint.Y)
                                OutputBitmap.SetPixel(x, y, left);

                        }
                    }
                    else if (MaskBitmap.GetPixel(x, y).R > 117 && MaskBitmap.GetPixel(x, y).G == 0 && MaskBitmap.GetPixel(x, y).B == 0)
                    {
                        Point point = RedToWindPos[MaskBitmap.GetPixel(x, y)];
                        AverageColor pixel = new AverageColor(OriginAirBitmap.GetPixel(point.X, point.Y));
                        float vitesse = 0;
                        if (!EchelleAir.ContainsKey(pixel))
                        {
                            int i = 1;
                            while (true)
                            {
                                List<AverageColor> around = new List<AverageColor>();
                                AverageColor up = new AverageColor(OriginAirBitmap.GetPixel(point.X, point.Y - i));
                                AverageColor down = new AverageColor(OriginAirBitmap.GetPixel(point.X, point.Y + i));
                                AverageColor left = new AverageColor(OriginAirBitmap.GetPixel(point.X - i, point.Y));
                                AverageColor right = new AverageColor(OriginAirBitmap.GetPixel(point.X + i, point.Y));
                                if (EchelleAir.ContainsKey(up))
                                    around.Add(up);
                                if (EchelleAir.ContainsKey(down))
                                    around.Add(down);
                                if (EchelleAir.ContainsKey(left))
                                    around.Add(left);
                                if (EchelleAir.ContainsKey(right))
                                    around.Add(right);
                                foreach (AverageColor color in around)
                                    vitesse += EchelleAir[color];
                                if (around.Count > 0)
                                {
                                    vitesse /= around.Count;
                                    break;
                                }
                                i++;
                            }
                        }
                        else
                            vitesse = EchelleAir[pixel];

                        float newVal = LimBeltz * (1f / 2f) * massVolAir * rayonAir * rayonAir * (float)Math.PI * vitesse * vitesse * vitesse;
                        int niv = (int)newVal;

                        if (niv > 1400000)
                            OutputBitmap.SetPixel(x, y, SeuilEau[25].Color);
                        else if (niv > 1200000)
                            OutputBitmap.SetPixel(x, y, SeuilEau[24].Color);
                        else if (niv > 1000000)
                            OutputBitmap.SetPixel(x, y, SeuilEau[23].Color);
                        else if (niv > 800000)
                            OutputBitmap.SetPixel(x, y, SeuilEau[22].Color);
                        else if (niv > 600000)
                            OutputBitmap.SetPixel(x, y, SeuilEau[21].Color);
                        else if (niv > 500000)
                            OutputBitmap.SetPixel(x, y, SeuilEau[20].Color);
                        else if (niv > 400000)
                            OutputBitmap.SetPixel(x, y, SeuilEau[19].Color);
                        else if (niv > 300000)
                            OutputBitmap.SetPixel(x, y, SeuilEau[18].Color);
                        else if (niv > 200000)
                            OutputBitmap.SetPixel(x, y, SeuilEau[17].Color);
                        else if (niv > 100000)
                            OutputBitmap.SetPixel(x, y, SeuilEau[16].Color);
                        else if (niv > 90000)
                            OutputBitmap.SetPixel(x, y, SeuilEau[15].Color);
                        else if (niv > 80000)
                            OutputBitmap.SetPixel(x, y, SeuilEau[14].Color);
                        else if (niv > 70000)
                            OutputBitmap.SetPixel(x, y, SeuilEau[13].Color);
                        else if (niv > 60000)
                            OutputBitmap.SetPixel(x, y, SeuilEau[12].Color);
                        else if (niv > 50000)
                            OutputBitmap.SetPixel(x, y, SeuilEau[11].Color);
                        else if (niv > 40000)
                            OutputBitmap.SetPixel(x, y, SeuilEau[10].Color);
                        else if (niv > 30000)
                            OutputBitmap.SetPixel(x, y, SeuilEau[9].Color);
                        else if (niv > 20000)
                            OutputBitmap.SetPixel(x, y, SeuilEau[8].Color);
                        else if (niv > 10000)
                            OutputBitmap.SetPixel(x, y, SeuilEau[7].Color);
                        else if (niv > 8000)
                            OutputBitmap.SetPixel(x, y, SeuilEau[6].Color);
                        else if (niv > 5000)
                            OutputBitmap.SetPixel(x, y, SeuilEau[5].Color);
                        else if (niv > 2000)
                            OutputBitmap.SetPixel(x, y, SeuilEau[4].Color);
                        else if (niv > 800)
                            OutputBitmap.SetPixel(x, y, SeuilEau[3].Color);
                        else if (niv > 500)
                            OutputBitmap.SetPixel(x, y, SeuilEau[2].Color);
                        else if (niv > 200)
                            OutputBitmap.SetPixel(x, y, SeuilEau[1].Color);
                        else
                            OutputBitmap.SetPixel(x, y, SeuilEau[0].Color);
                    }
                }
            }
            outputinc++;
            TransformCount++;
            OutputBitmap.Save(OutputURI);

            //Time calcul
            time += TimeSpan.FromMinutes(step * 15);
        }
    }
}
