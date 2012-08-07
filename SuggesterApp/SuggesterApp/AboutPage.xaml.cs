using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace SuggesterApp
{
    public partial class AboutPage : PhoneApplicationPage
    {
        public AboutPage()
        {
            InitializeComponent();

            _txtProductCopyright.Text = AssemblyCopyright;

            Assembly asm = Assembly.GetExecutingAssembly();
            string[] parts = asm.FullName.Split(',');
            _txtProductName.Text = parts[0];
            string[] subParts = parts[1].Split('=');
            _lblVersion.Text = subParts[1];

            if (CurrentTheme == Theme.Light)
            {
                _imgIcon.Source = new BitmapImage(new Uri("Background_4_white.png", UriKind.Relative));
                System.Diagnostics.Debug.WriteLine("Light");
            }
        }

        private void _btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void _btnSubmitReview_Click(object sender, RoutedEventArgs e)
        {
            MarketplaceReviewTask review = new MarketplaceReviewTask();
            review.Show();
        }


        /// <summary>
        /// Gets the assembly copyright.
        /// </summary>
        /// <value>The assembly copyright.</value>
        public string AssemblyCopyright
        {
            get
            {
                // Get all Copyright attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public Theme CurrentTheme
        {
            get { return (Visibility)Application.Current.Resources["PhoneLightThemeVisibility"] == Visibility.Visible ? Theme.Light : Theme.Dark; }
        }
    }
    public enum Theme
    {
        Light,
        Dark
    }
}