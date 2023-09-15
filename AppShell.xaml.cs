namespace LabelCreator;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
	}

    private async void HowToClicked(object sender, EventArgs e)
    {
        await Browser.OpenAsync("https://tanakamawere.notion.site/How-To-Use-Label-Creator-fc0f7dd113e34d93a79c360c95f783bb?pvs=4");
    }

    private void DeveloperWebsite(object sender, EventArgs e)
    {
        Browser.OpenAsync("https://tanakamawere.co.zw");
    }
}
