namespace LabelCreator.Popups;

public partial class NotificationPopup 
{ 
	public NotificationPopup(string text)
	{
		InitializeComponent();
        notificationPopupLabel.Text = text;
    }

    private void DonateButton_Clicked(object sender, EventArgs e) =>
        Browser.OpenAsync("https://www.paynow.co.zw/Payment/BillPaymentLink/?q=aWQ9MTU3MjgmYW1vdW50PTAuMDAmYW1vdW50X3F1YW50aXR5PTAuMDAmbD0w");

    private void ClosePopupButton_Clicked(object sender, EventArgs e) => Close();
}