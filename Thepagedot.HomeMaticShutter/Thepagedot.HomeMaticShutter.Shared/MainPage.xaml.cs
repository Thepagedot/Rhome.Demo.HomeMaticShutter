using System;
using System.Collections.Generic;
using System.Text;
using Thepagedot.Rhome.HomeMatic.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Thepagedot.HomeMaticShutter
{
    public sealed partial class MainPage
    {
        public Shutter Shutter { get; set; }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }

        private async void btnUp_Click(object sender, RoutedEventArgs e)
        {
            var button = (sender as Button);
            if (button != null && App.HomeMaticApi != null)
            {
                button.IsEnabled = false;
                await App.HomeMaticApi.SendChannelUpdateAsync(Shutter.IseId, 1);
                button.IsEnabled = true;
            }
        }

        private async void btnStop_Click(object sender, RoutedEventArgs e)
        {
            var button = (sender as Button);
            if (button != null && App.HomeMaticApi != null)
            {
                button.IsEnabled = false;
                await App.HomeMaticApi.SendChannelUpdateAsync(Shutter.StopIseId, 1);
                button.IsEnabled = true;
            }
        }

        private async void btnDown_Click(object sender, RoutedEventArgs e)
        {
            var button = (sender as Button);
            if (button != null && App.HomeMaticApi != null)
            {
                button.IsEnabled = false;
                await App.HomeMaticApi.SendChannelUpdateAsync(Shutter.IseId, 0);
                button.IsEnabled = true;
            }
        }
    }
}
