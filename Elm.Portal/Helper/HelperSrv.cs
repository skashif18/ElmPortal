using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Portal.API.Helper
{
    public class HelperSrv:IHelperSrv
    {
        private readonly IWebHostEnvironment env;

        public HelperSrv(IWebHostEnvironment _env)
        {
            env = _env;
        }

        public string GetFilePath(string UploadDirectory)
        {
            string contentPath = env.ContentRootPath;

            var filePath = Path.Combine(contentPath, UploadDirectory);

            return filePath;
        }

        public string UploadFileBase64(string base64, string fileName, string UploadDirectory)
        {
            var directory = GetFilePath(UploadDirectory);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var path = Path.Combine(directory, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            //base64 = base64.Replace("data:image/png;base64,", "");

            File.WriteAllBytes(path, Convert.FromBase64String(base64));

            return fileName;
        }
        //public Boolean UploadFile(IFormFile file, string filePath, string fileName, string UploadDirectory)
        //{

        //    var directory = GetFilePath(UploadDirectory);

        //    if (!Directory.Exists(directory))
        //    {
        //        Directory.CreateDirectory(directory);
        //    }

        //    var path = Path.Combine(directory, fileName);


        //    using (FileStream stream = new FileStream(path, FileMode.Create))
        //    {
        //        file.CopyTo(stream);
        //    }

        //    return true;
        //}
    }
}
