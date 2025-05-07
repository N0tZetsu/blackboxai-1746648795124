using System;
using System.IO;
using Microsoft.Win32;

namespace FiveM_Spoofer.Services
{
    public class CleanerService
    {
        public void Clean()
        {
            try
            {
                // Delete FiveM registry key
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", writable: true))
                {
                    if (key != null)
                    {
                        key.DeleteSubKeyTree("CitizenFX", throwOnMissingSubKey: false);
                    }
                }

                // Delete FiveM cache folder (example path, adjust as needed)
                string fiveMCachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FiveM", "FiveM.app", "cache");
                if (Directory.Exists(fiveMCachePath))
                {
                    Directory.Delete(fiveMCachePath, true);
                }

                // Delete system temp files (optional, can be expanded)
                string tempPath = Path.GetTempPath();
                if (Directory.Exists(tempPath))
                {
                    Directory.Delete(tempPath, true);
                }

                // Delete DigitalEntitlements folder
                string digitalEntitlementsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData", "Local", "DigitalEntitlements");
                if (Directory.Exists(digitalEntitlementsPath))
                {
                    Directory.Delete(digitalEntitlementsPath, true);
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed
                throw new Exception("Error during cleaning process: " + ex.Message, ex);
            }
        }
    }
}
