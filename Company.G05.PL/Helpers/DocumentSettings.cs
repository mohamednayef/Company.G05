namespace Company.G05.PL.Helpers;

public class DocumentSettings
{
    // 1. UploadFile function, return fileName
    public static string UploadFile(IFormFile file, string folderName)
    {
        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(
            Directory.GetCurrentDirectory(), 
            "wwwroot/Files", 
            folderName, 
            fileName 
            );
        
        var fileStream = new FileStream(filePath, FileMode.Create);
        file.CopyTo(fileStream);
        
        return fileName;
    }
    
    // 2. DeleteFile
    public static void DeleteFile(string fileName, string folderName)
    {
        var filePath = Path.Combine(
            Directory.GetCurrentDirectory(), 
            "wwwroot/Files", 
            folderName, 
            fileName 
        );
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}