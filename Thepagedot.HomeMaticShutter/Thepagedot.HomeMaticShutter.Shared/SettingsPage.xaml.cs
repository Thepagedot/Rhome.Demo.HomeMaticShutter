using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Thepagedot.Rhome.HomeMatic.Models;
using Windows.UI.Popups;
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

            if (App.Address != null)
                tbxCcuAddress.Text = App.Address;

            //App.Channels.Add(new Shutter("Test Shutter", 0, 0, ""));
            lvShutters.ItemsSource = App.Channels;

            if (App.CurrentShutter != null)
            {
                var index = App.Channels.IndexOf(App.CurrentShutter);
                lvShutters.SelectedIndex = index;
            }
        }

        private async void tbxCcuAddress_LostFocus(object sender, RoutedEventArgs e)
        {
            var text = (sender as TextBox).Text;
            if (text.Length == 0)
                return;

            await App.InitHomeMatic(text);
            if (App.Channels.Any())
            {
                lvShutters.ItemsSource = App.Channels;
            }
        }

        private async void lvShutters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedIndex = (sender as ListView).SelectedIndex;
            if (selectedIndex != -1)
            {
                if (App.Channels.ElementAt(selectedIndex) is Shutter)
                {
                    App.CurrentShutter = (Shutter)App.Channels.ElementAt(selectedIndex);
                }
                else
                {
                    var dialog = new MessageDialog("The device you selected is not a shutter. Please select a shutter to control.", "Select a shutter");
                    await dialog.ShowAsync();
                    (sender as ListView).SelectedIndex = -1;
                }
            }
                
        }
    }
}
