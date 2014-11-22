using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace _1000Best.Common
{
    public class BackgroundImageRotator
    {
        private const int NumberOfBackgroundImages = 4;

        private int currentBackgroundIndex;

        // Detecting the current theme.
        private readonly Color lightThemeBackground = Color.FromArgb(255, 255, 255, 255);
        private readonly Color darkThemeBackground = Color.FromArgb(255, 0, 0, 0);
        private SolidColorBrush backgroundBrush;
        
        // An enum to specify the theme.
        public enum AppTheme
        {
            Dark = 0,
            Light = 1
        }
        
        internal AppTheme CurrentTheme
        {
            get
            {
                if (backgroundBrush == null)
                {
                    backgroundBrush = Application.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush;
                }

                if (backgroundBrush.Color == lightThemeBackground)
                {
                    return AppTheme.Light;
                }

                if (backgroundBrush.Color == darkThemeBackground)
                {
                    return AppTheme.Dark;
                }

                return AppTheme.Dark;
            }
        }


        public ImageBrush Rotate()
        {
            if (CurrentTheme == AppTheme.Dark)
            {
                string backgroundImageLocation = string.Format("/Assets/Panorama{0}.jpg", this.currentBackgroundIndex + 1);
                var backgroundImageBrush = new ImageBrush{ ImageSource = new BitmapImage(new Uri(backgroundImageLocation, UriKind.Relative)) };

                this.currentBackgroundIndex++;
                if (this.currentBackgroundIndex >= NumberOfBackgroundImages)
                {
                    this.currentBackgroundIndex = 0;
                }

                return backgroundImageBrush;
            }
            return null;
        }
    }
}