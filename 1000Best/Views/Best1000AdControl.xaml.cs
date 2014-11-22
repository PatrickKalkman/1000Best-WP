using System.Windows;
using System.Windows.Controls;

namespace _1000Best.Views
{
    public partial class Best1000AdControl : UserControl
    {
        public Best1000AdControl()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
            this.AdRotator.Log += AdRotator_Log;
        }

        void AdRotator_Log(string message)
        {
            int i = 10;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            AdRotator.DefaultHouseAdBody = "_1000Best.Views.DefaultAd";
            AdRotator.Invalidate(true);
        }
    }
}
