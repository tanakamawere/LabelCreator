﻿using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace LabelCreator;

public partial class App : Application
{
	public App()
    {
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHRqVVhjVFpFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF5jSH9QdkdnUX1fc3xSQA==;Mgo+DSMBPh8sVXJ0S0J+XE9HflRDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS31Td0RgWX1aeHZURWlVVw==;ORg4AjUWIQA/Gnt2VVhkQlFadVdJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxQdkRiWn9cdHxXR2RdU0I=;OTMyNzM5QDMyMzAyZTM0MmUzMEx1WEMrWkIwZmdYOXpYUnZsZHp3RVZoZ2liWFJKb0c0ZUZTUkV5U3VmQnM9;OTMyNzQwQDMyMzAyZTM0MmUzMGZKR1AyejFQVzVHTWdGT2xXajdVV0w4N2k0VEl4TlhxaXFPZkpzV2I4dTA9;NRAiBiAaIQQuGjN/V0Z+WE9EaFxKVmJLYVB3WmpQdldgdVRMZVVbQX9PIiBoS35RdUViWHxecnBdRGFYVk1w;OTMyNzQyQDMyMzAyZTM0MmUzME5VMFdSRkFhTDIxQ0pHZUhKS1plOHV5RUtpUDB0eTVrWFdZZWs4RmZ4RFU9;OTMyNzQzQDMyMzAyZTM0MmUzMGlrQmRhbTFBR1g1eitTZ2QvQmcyWVJLbjFxd1o0RWVKYmhKUHZ0VEMyRFU9;Mgo+DSMBMAY9C3t2VVhkQlFadVdJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxQdkRiWn9cdHxXR2VYVUI=;OTMyNzQ1QDMyMzAyZTM0MmUzMEx3VHE0aHpQaFVNbTl2aU40VWZQSnhIQzdia1NsRFB5WlRNcVpjbXVZNXc9;OTMyNzQ2QDMyMzAyZTM0MmUzMFRsb2lxSkdXeVRISzd0aDdxY0VONTgvUUVmT2JKKytoam94UU9xaThGbXM9;OTMyNzQ3QDMyMzAyZTM0MmUzME5VMFdSRkFhTDIxQ0pHZUhKS1plOHV5RUtpUDB0eTVrWFdZZWs4RmZ4RFU9");

        InitializeComponent();

		MainPage = new AppShell();
	}

    protected override void OnStart()
    {
        AppCenter.Start("android=8368a73e-6e37-4eba-97f9-cdc4f8862358;" +
                  "ios=8368a73e-6e37-4eba-97f9-cdc4f8862358;",
                  typeof(Analytics), typeof(Crashes));
    }
}
