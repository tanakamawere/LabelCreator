namespace LabelCreator;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
	}

    private void HowToClicked(object sender, EventArgs e)
    {
    }
    private void Donate(object sender, EventArgs e)
    {
        Browser.OpenAsync("https://www.paynow.co.zw/Payment/BillPaymentLink/?q=aWQ9MTU3MjgmYW1vdW50PTAuMDAmYW1vdW50X3F1YW50aXR5PTAuMDAmbD0w");
    }
    private void WhatsApp(object sender, EventArgs e)
    {
        Browser.OpenAsync("https://wa.me/263718086035");
    }
}
