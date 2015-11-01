# Rhome Demo: HomeMatic Shutter
This is a simple demo application for the Rhome framework and shows how to work with it. It simply initializes a HomeHatic API, searches for all shutters inside a house and lets the user select one of them to remote control it with three simple buttons.
## Code explaination
### Add Properties
```c#
public static HomeMaticXmlApi HomeMaticApi;
public static List<HomeMaticChannel> Shutters;
public static HomeMaticChannel CurrentShutter;
```

### Initialize the API
```c#
public static async Task InitHomeMatic(string address)
{
    HomeMaticApi = new HomeMaticXmlApi(new Ccu("HomeMatic CCU", address));
    if (await HomeMaticApi.CheckConnectionAsync())
    {
        var devices = await HomeMaticApi.GetDevicesAsync();
        foreach(var device in devices)
        {
            var homeMaticDevice = device as HomeMaticDevice;
            var channels = homeMaticDevice.ChannelList.Where(c => c.Type == 36);
            Shutters.AddRange(channels.ToList());
        }               
    }
}
```

### React on button clicks
```c#
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
```
