using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Threading;
using System.Xml.Linq;

namespace SfmlGravityWpf.Windows
{
    public partial class MainWindow : System.Windows.Window
    {
        private DateTime _d = DateTime.Now;

        private int _s = 0;
        private int _sh = 0;
        private int _k10 = 0;
        private int _k20 = 0;
        private int _k30 = 0;
        private int _k40 = 0;
        private int _k50 = 0;
        private int _k60 = 0;
        private int _k70 = 0;
        private int _k80 = 0;
        private int _k90 = 0;
        private int _k100 = 0;

        private int _h = 0;
        private int _radius = 300;
        private int _count = 1;
        private int _height = 700;
        private int _width = 800;

        private RenderWindow _renderWindow;
        private readonly DispatcherTimer _timer;


        public MainWindow()
        {
            this.InitializeComponent();


            this.CreateRenderWindow();

            this._timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 0) };
            this._timer.Tick += Timer_Tick;
            this._timer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*            foreach (var circle in this._circles)
                        {
                            var rand = new Random();
                            var color = new Color((byte)rand.Next(), (byte)rand.Next(), (byte)rand.Next());
                            circle.FillColor = color;
                        }*/
        }

        private void CreateRenderWindow()
        {
            if (this._renderWindow != null)
            {
                this._renderWindow.SetActive(false);
                this._renderWindow.Dispose();
            }

            var context = new ContextSettings { DepthBits = 24 };
            this._renderWindow = new RenderWindow(this.DrawSurface.Handle, context);
            this._renderWindow.MouseButtonPressed += RenderWindow_MouseButtonPressed;
            this._renderWindow.SetActive(true);
        }

        private void DrawSurface_SizeChanged(object sender, EventArgs e)
        {
            this.CreateRenderWindow();
        }

        private void RenderWindow_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            /*            var l = new List<CircleShape>();
                        var rand = new Random();
                        for (int i = 0; i < _count; i++)
                        {
                            var c = new CircleShape(_radius);
                            var color = new Color((byte)rand.Next(), (byte)rand.Next(), (byte)rand.Next());
                            c.FillColor = color;
                            c.Position = new Vector2f(rand.Next() % _width, rand.Next() % _height);
                            l.Add(c);
                        }
                        this._circles.Clear();
                        this._circles.AddRange(l);*/
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this._renderWindow.DispatchEvents();

            this._renderWindow.Clear(Color.Black);
            _h++;
            Random r = new Random();
            for (var i = 0; i < 100000; i++)
            {
                Vertex[] m = new[]
                {
                    new Vertex(
                        new Vector2f(r.Next() % _width, r.Next() % _height),
                        new Color((byte)r.Next(), (byte)r.Next(), (byte)r.Next())),
                    new Vertex(
                        new Vector2f(r.Next() % _width, r.Next() % _height),
                        new Color((byte)r.Next(), (byte)r.Next(), (byte)r.Next())),
                };
                this._renderWindow.Draw(m, PrimitiveType.Lines);
            }
            var d = DateTime.Now;
            var cadr = 1000 / ((d.Minute - _d.Minute) * 60 * 1000 + (d.Second - _d.Second) * 1000 + d.Millisecond - _d.Millisecond);
            _sh += cadr;
            _s++;
            if (0 < cadr && cadr < 10) _k10++; 
            if (10 < cadr && cadr < 20) _k20++; 
            if (20 < cadr && cadr < 30) _k30++; 
            if (30 < cadr && cadr < 40) _k40++; 
            if (40 < cadr && cadr < 50) _k50++; 
            if (50 < cadr && cadr < 60) _k60++; 
            if (60 < cadr && cadr < 70) _k70++; 
            if (70 < cadr && cadr < 80) _k80++; 
            if (80 < cadr && cadr < 90) _k90++; 
            if (90 < cadr && cadr < 100) _k100++; 

            tb.Text = $"{cadr}-{_sh/_s}\n10-{_k10}\n20-{_k20}\n30-{_k30}\n40-{_k40}\n50-{_k50}\n60-{_k60}\n70-{_k70}\n80-{_k80}\n90-{_k90}\n100-{_k100}\n" ;
            _d = d;

            this._renderWindow.Display();
        }

    }
}
