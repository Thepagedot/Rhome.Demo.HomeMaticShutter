# Rhome Demo: HomeMatic Shutter
This is a simple demo application for the [Rhome framework](https://github.com/Thepagedot/Rhome) and shows how to work with it. It simply initializes a HomeHatic API, searches for all shutters inside a house and lets the user select one of them to remote control it with three simple buttons.
## Code explanation
### Add Properties
First, we added three properties to the main app file: One for Rhome's HomeMatic API service, a list of all shutters in the system and a variable for the currently selected shutter.
```c#
public static HomeMaticXmlApi HomeMaticApi;
public static List<HomeMaticChannel> Shutters;
public static HomeMaticChannel CurrentShutter;
```

### Initialize the API
The main app class also provides a method to initialize everything. Here we create a new instance of the API with the provided IP-Address string and search for shutters that we can add to the global list of shutters.
This method can be called whenever the user changes or enters an IP-Adress in the settings screen or directly after the application start if the address could be restored from previous sessions.
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
At our user interface we show three buttons to control the currently selected shutter. To rise it up we need to set the value of the shutter (wich is an ```HomeMaticChannel```) to 1. For this the Rhome HomeMatic API has implemented the ```SendChannelUpdateAsync``` method. It can be called when the user sets the value by clicking one of the buttons. 
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
