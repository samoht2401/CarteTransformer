using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.IO;

namespace CarteTransformer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Transformer transformer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            transformer = new Transformer();
        }

        /*/// <summary>
        /// Handles the OpenGLDraw event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="SharpGL.SceneGraph.OpenGLEventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
        {
            //  Get the OpenGL object

           
            //  Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Load the identity matrix.
            gl.LoadIdentity();

            //  Rotate around the Y axis.
            gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);

            //  Draw a coloured pyramid.
            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.End();

            //  Nudge the rotation.
            rotation += 3.0f;
        }

        /// <summary>
        /// Handles the OpenGLInitialized event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="SharpGL.SceneGraph.OpenGLEventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            //  TODO: Initialise OpenGL here.

            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Set the clear color.
            gl.ClearColor(0, 0, 0, 0);
        }

        /// <summary>
        /// Handles the Resized event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="SharpGL.SceneGraph.OpenGLEventArgs"/> instance containing the event data.</param>
        private void openGLControl_Resized(object sender, OpenGLEventArgs args)
        {
            //  TODO: Set the projection matrix here.

            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
        }

        /// <summary>
        /// The current rotation.
        /// </summary>
        private float rotation = 0.0f;*/

        private void Transform_StartWith_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.Key == Key.Enter)
            {
                List<String> files = Directory.EnumerateFiles(transformer.OriginDirectory, Transform_StartWith.Text).ToList<String>();
                if (files.Count > 0)
                {
                    transformer.OriginEauURI = files[0];
                    transformer.ReloadOriginBitmap();
                    Transform_Image.Source = new BitmapImage(new Uri(transformer.OriginEauURI));
                }
            }*/
        }

        private void Transform_Button_Click(object sender, RoutedEventArgs e)
        {
            //List<String> files = Directory.EnumerateFiles(transformer.OriginDirectory, Transform_StartWith.Text).ToList<String>();
            //files.RemoveAt(0);
            //transformer.Transform();
            //Transform_Image.Source = new BitmapImage(new Uri(transformer.OutputURI));
            DateTime start = DateTime.Now;
            int step = int.Parse(Step_Selector.Text.TrimEnd('m', 'i', 'n')) / 15;
            int duration = Duration_Selector.Text.EndsWith("day") ? 96 : 2880; // en quart d'heure
            for (int i = 0; i < duration; i += step)
            {
                transformer.ReloadOriginBitmap();
                transformer.Transform(step);
                WindDownloader.MoveOn(step);
                CourantDownloader.MoveOn(step);
            }
            Text_BluePoint.Text = transformer.BluePointVitesse;
            MessageBox.Show((DateTime.Now - start).TotalHours.ToString() + " - "
                + (DateTime.Now - start).TotalMinutes.ToString() + " - "
                + (DateTime.Now - start).TotalSeconds.ToString() + " - "
                + (DateTime.Now - start).TotalMilliseconds.ToString() + " - ");
            /*Bitmap img = WindDownloader.GetNextWind();
            String[] tab = WindDownloader.GetImgName().Split('/');
            String name = Directory.GetCurrentDirectory() + "\\Output\\" + tab[tab.Count() - 2] + tab.Last();
            img.Save(name);
            Transform_Image.Source = new BitmapImage(new Uri(name));*/
        }

        private void Step_Selector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
