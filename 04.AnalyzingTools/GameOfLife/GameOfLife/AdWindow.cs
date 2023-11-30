using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace GameOfLife
{
    class AdWindow : Window
    {
        private readonly DispatcherTimer adTimer;
        private int imgNmb;     // the number of the image currently shown
        private string link;    // the URL where the currently shown ad leads to
        private ImageBrush imageBrush;
        private BitmapImage[] bitmapImage;

        public AdWindow(Window owner)
        {
            InitWindow(owner);

            adTimer = new DispatcherTimer();

            InitTimer();

            InitSources();

            SetInitialImageNumber();

            ChangeAds(this, EventArgs.Empty);
        }

        private void InitSources()
        {
            link = "http://example.com";
            imageBrush = new ImageBrush();
            bitmapImage = new BitmapImage[]
            {
                new BitmapImage(new Uri("ad1.jpg", UriKind.Relative)),
                new BitmapImage(new Uri("ad2.jpg", UriKind.Relative)),
                new BitmapImage(new Uri("ad3.jpg", UriKind.Relative))
            };
        }

        private void InitWindow(Window owner)
        {
            Owner = owner;
            Width = 350;
            Height = 100;
            ResizeMode = ResizeMode.NoResize;
            WindowStyle = WindowStyle.ToolWindow;
            Title = "Support us by clicking the ads";
            Cursor = Cursors.Hand;
            ShowActivated = false;
            MouseDown += OnClick;
        }

        private void InitTimer()
        {
            // Run the timer that changes the ad's image 
            adTimer.Interval = TimeSpan.FromSeconds(3);
            adTimer.Tick += ChangeAds;
            adTimer.Start();
        }

        private void SetInitialImageNumber()
        {
            Random rnd = new Random();
            imgNmb = rnd.Next(1, 3);
        }

        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(link);
            Close();
        }

        public void Unsubscribe()
        {
            adTimer.Tick -= ChangeAds;
        }

        private void ChangeAds(object sender, EventArgs eventArgs)
        {
            switch (imgNmb)
            {
                case 1:
                case 2:
                    imgNmb++;
                    break;
                case 3:
                    imgNmb = 1;
                    break;
            }

            imageBrush.ImageSource = bitmapImage[imgNmb - 1];
            Background = imageBrush;
        }
    }
}