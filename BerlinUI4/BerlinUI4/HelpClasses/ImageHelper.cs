using Berlin.Domain.Entities;
using Microsoft.AspNetCore.Components.Forms;
using System.IO;

namespace BerlinUI4.HelpClasses
{
    public static class ImageHelper
    {
        public static async Task<string> SaveUserImg(IBrowserFile browserFile, string webRootPath)
        {
            return await SaveImg(browserFile, webRootPath, "users");
        }
        public static async Task<string> SaveInvoiceBackgroundImg(IBrowserFile browserFile, string webRootPath)
        {
            return await SaveImg(browserFile, webRootPath, "invoicebackgrounds");
        }
        public static async Task<string> SaveInvoiceLogoImg(IBrowserFile browserFile, string webRootPath)
        {
            return await SaveImg(browserFile, webRootPath, "invoicelogos");
        }

        private static async Task<string> SaveImg(IBrowserFile browserFile, string webRootPath, string path)
        {
            if (browserFile != null)
            {
                // Ensure the folder exists
                var uploads = Path.Combine(webRootPath, path);
                Directory.CreateDirectory(uploads);
                // Save the file
                var filePath = Path.Combine(uploads, browserFile.Name);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await browserFile.OpenReadStream().CopyToAsync(stream);
                }
                return $"{path}/{browserFile.Name}";
            }

            return null;
        }

    }
}
