using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Entity
{
    public class Helper
    {
        public static async Task<string> UploadImage(List<IFormFile> Files, string FolderName)
        {
            foreach (var file in Files)
            {
                if (file.Length > 0)
                {
                    string ImageName = Guid.NewGuid().ToString() + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + ".jpg";
                    var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Images/" + FolderName + "/" + ImageName);
                    using (var stream = System.IO.File.Create(filePaths))
                    {
                        await file.CopyToAsync(stream);
                        return ImageName;
                    }
                }
            }
            return string.Empty;
        }

        public static async Task<string> UploadPdf(List<IFormFile> Files, string FolderName)
        {
            foreach (var file in Files)
            {
                if (file.Length > 0)
                {
                    // استخدم GUID مع الامتداد الصحيح
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/PdfFiles/" + FolderName);

                    // تأكد من وجود المجلد
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    var filePath = Path.Combine(folderPath, fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                        return fileName; // إعادة اسم الملف
                    }
                }
            }
            return string.Empty;
        }

        public const string Success = "success";
        public const string Error = "error";
        public const string msgType = "msgType";
        public const string tittle = "tittle";
        public const string msg = "msg";

        public enum eCurrentStatu
        {
            Active = 1,
            Delete = 0
        }

        public const string Delete="Delete";
        public const string Create = "Create";
        public const string Update = "Update";

        //Data DefaultRole
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string Customer = "Customer";



        //Data DefaultSuperAdmin
        public const string Name= "superAdmin";
        public const string Email ="superAdmin@domain.com";
        public const string UserName ="superAdmin@domain.com";
        public const string ImageName = "default.jpg";
        public const string Password = "123456$$ww";

        //Data DefaultCustomer
        public const string CustomerName = "Customer";
        public const string CustomerEmail = "Customer@domain.com";
        public const string CustomerUserName = "Customer@domain.com";
        public const string CustomerPassword = "123456$$mm";

    }
}
