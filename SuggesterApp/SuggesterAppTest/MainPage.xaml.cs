using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace SuggesterAppTest
{
    using Microsoft.Phone.Shell;
    using Microsoft.Silverlight.Testing;
    using System.Reflection;
    using System.Windows.Data;
    using System.Globalization;

    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SystemTray.IsVisible = false;
                UnitTestSettings settings = UnitTestSystem.CreateDefaultSettings();
                //settings.TestService = null;
                settings.TestAssemblies.Add(Assembly.GetExecutingAssembly());
                var rawTestPage = UnitTestSystem.CreateTestPage(settings);
                var testPage = rawTestPage as IMobileTestPage;
                //var testPage = UnitTestSystem.CreateTestPage(settings) as IMobileTestPage;
                BackKeyPress += (x, xe) => xe.Cancel = testPage.NavigateBack();
                (Application.Current.RootVisual as PhoneApplicationFrame).Content = testPage;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
    public class typeNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class fontWeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}