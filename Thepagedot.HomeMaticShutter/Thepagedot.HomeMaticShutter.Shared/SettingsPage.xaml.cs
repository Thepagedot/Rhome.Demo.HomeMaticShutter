using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Thepagedot.Rhome.HomeMatic.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Thepagedot.HomeMaticShutter
{
    public partial class SettingsPage
    {
        public ObservableCollection<Shutter> Shutters { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);

            App.Shutters.Add(new Shutter("Test Shutter", 0, 0, ""));
            lvShutters.ItemsSource = App.Shutters;

            if (App.CurrentShutter != null)
            {
                var index = App.Shutters.IndexOf(App.CurrentShutter);
                lvShutters.SelectedIndex = index;
            }
        }

        private async void tbxCcuAddress_LostFocus(object sender, RoutedEventArgs e)
        {
            await App.InitHomeMatic((sender as TextBox).Text);
            if (App.Shutters.Any())
            {
                lvShutters.ItemsSource = App.Shutters;
            }
        }

        private void lvShutters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedIndex = (sender as ListView).SelectedIndex;
            if (selectedIndex != -1)
                App.CurrentShutter = App.Shutters.ElementAt(selectedIndex);
        }
    }
}
