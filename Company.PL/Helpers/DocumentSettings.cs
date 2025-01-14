using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Company.PL.MappingProfilesss
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string FolderName)
        {
            //1.Get Located Folder Path
            //string folderpath = "D:\\Work\\CMSSolution\\Company.PL\\wwwroot\\Files\\";
            //string FolderPath = Directory.GetCurrentDirectory()+ @"\wwwroot\Files\" + FolderName;
            string folderpath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", FolderName);
            //2. Get File Name And Make It Unique
            string filename = $"{Guid.NewGuid()}{file.FileName}";
            //3.Get File Path 
            string filepath = Path.Combine(folderpath, filename);
            //4.save file as streaming : [data per time] 
           using var filestream = new FileStream(filepath, FileMode.Create);
            file.CopyTo(filestream);
            return filename;
        }
        public static void DeleteFile(string FileName ,string FolderName) 
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", FolderName, FileName);
            if(File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
    }
}