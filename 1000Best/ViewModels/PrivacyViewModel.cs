using System.Windows.Media;
using Caliburn.Micro;
using _1000Best.Common;
using _1000Best.Resources;

namespace _1000Best.ViewModels
{
    public class PrivacyViewModel : Screen
    {
        private readonly BackgroundImageBrush backgroundImageBrush;

        public PrivacyViewModel(BackgroundImageBrush backgroundImageBrush)
        {
            this.backgroundImageBrush = backgroundImageBrush;
        }

        public ImageBrush BackgroundImageBrush
        {
            get { return backgroundImageBrush.GetBackground(); }
        }

        public string ApplicationName
        {
            get { return AppResources.ApplicationTitle; }
        }

        public string PageTitle
        {
            get { return AppResources.PrivacyPageTitle; }
        }
    }
}