using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.Helpers.File_Extencions;

public static class File_Extencion
{
    public static bool IsValidType(this IFormFile file, string type)
        => file.ContentType.StartsWith(type);

    public static bool IsValidSize(this IFormFile file, int kb)
        => file.Length <= kb * 1024;

    public static async Task<string> UploadAsync(this IFormFile file, params string[] paths)
    {
        string uploadPath = Path.Combine(paths);
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }
        string fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
        string filePath = Path.Combine(uploadPath, fileName);
        using (Stream stream = File.Create(filePath))
        {
            await file.CopyToAsync(stream);
        }
        return fileName;
    }
    public static void Delete(params string[] paths)
    {
        string filePath = Path.Combine(paths);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
