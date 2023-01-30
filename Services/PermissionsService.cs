namespace LabelCreator.Services
{
    public class PermissionsService
    {
        public static async Task<bool> CheckForStoragePermission()
        {
            bool permitted = false;

            var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (status == PermissionStatus.Granted)
                permitted = true;
            else
            {
                if (Permissions.ShouldShowRationale<Permissions.StorageWrite>())
                {
                    await Shell.Current.DisplayAlert("Permission Needed", "To export question images to your device, please allow this permission.", "Ok");
                }
                status = await Permissions.RequestAsync<Permissions.StorageWrite>();
                if (status != PermissionStatus.Granted)
                {
                    //await Shell.Current.DisplayAlert("Permission Needed", "To export question images to your device, please allow this permission.", "Ok");
                }
            }
            return permitted;
        }
    }
}
