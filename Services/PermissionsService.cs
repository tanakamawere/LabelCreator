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
                status = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }
            return permitted;
        }
    }
}
