using Microsoft.AspNetCore.Http;

namespace TaskManagementSystem.BL.Extensions
{
    public static class FileExtension
    {
        public static async Task<string> UploadAsync(this IFormFile file, string fileName)
        {
            string path = Path.Combine(fileName);

            string newFilePath = Path.GetRandomFileName() + Path.GetFileName(file.FileName);
            if (!File.Exists(newFilePath))
            {
                File.Create(newFilePath);
            }
            using (Stream st = File.Create(Path.Combine(path, newFilePath)))
            {
                await file.CopyToAsync(st);
            }
            return newFilePath;
        }
    }
}
