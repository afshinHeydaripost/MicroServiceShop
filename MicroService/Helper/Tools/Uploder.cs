using Helper;
using Microsoft.AspNetCore.Http;

public static class Uploder
{
    private static string[] extImage = { ".jpg", ".png", ".jpeg" };
    private static string[] extMovie = { ".mp4", ".mkv", ".3gp" };
    private static string[] extDocument = { ".pdf", ".doc", ".3gp", ".docx", ".txt", ".xlsx", ".xls", ".xlt" };
    public static async Task<GeneralResponse<string>> UploadFile(this IFormFile userfile, string fileName, List<FileSizeType> fileSizeTypes, string domainName = "")
    {
        var serverPath = Directory.GetCurrentDirectory() + "\\wwwroot\\Upload";
        checkDirectoryIsExist(serverPath);
        var ext = Path.GetExtension(userfile.FileName);
        bool validFile = false;
        var errorMessage = "";
        try
        {
            if (extImage.Contains(ext) && fileSizeTypes.Any(x => x.Type == FileType.Image))
            {
                var fileSetting = fileSizeTypes.First(x => x.Type == FileType.Image);
                if (userfile.Length / 1024f < fileSetting.Size)
                    validFile = true;
                else
                    errorMessage = Message.InvalidFileSize;

            }
            if (extMovie.Contains(ext) && fileSizeTypes.Any(x => x.Type == FileType.Video))
            {
                var fileSetting = fileSizeTypes.First(x => x.Type == FileType.Video);
                if (userfile.Length / 1024f < fileSetting.Size)
                    validFile = true;
                else
                    errorMessage = Message.InvalidFileSize;
            }
            if (extDocument.Contains(ext) && fileSizeTypes.Any(x => x.Type == FileType.Document))
            {
                var fileSetting = fileSizeTypes.First(x => x.Type == FileType.Document);
                if (userfile.Length / 1024f < fileSetting.Size)
                    validFile = true;
                else
                    errorMessage = Message.InvalidFileSize;
            }

            if (validFile)
            {
                var dirName = Guid.NewGuid().ToString().Replace("-", "");
                serverPath = serverPath + "\\" + dirName;
                checkDirectoryIsExist(serverPath);
                string file = serverPath + "\\" + fileName + ext;
                using (var stream = new FileStream(file, FileMode.Create))
                {
                    await userfile.CopyToAsync(stream);
                }
                if (!string.IsNullOrEmpty(domainName))
                {
                    file = domainName + $"Upload/{dirName}/{fileName}{ext}";
                }
                return GeneralResponse<string>.Success(file);
            }
            if (string.IsNullOrEmpty(errorMessage))
                errorMessage = errorMessage + " " + Message.InvalidFile;
            return GeneralResponse<string>.Fail(errorMessage);
        }
        catch (Exception ex)
        {
            return GeneralResponse<string>.Fail(ex);
        }
    }
    public static async Task<GeneralResponse<string>> FileToBase64(this IFormFile userfile, List<FileSizeType> fileSizeTypes)
    {
        var ext = Path.GetExtension(userfile.FileName);
        bool validFile = false;
        var errorMessage = "";
        try
        {
            if (extImage.Contains(ext) && fileSizeTypes.Any(x => x.Type == FileType.Image))
            {
                var fileSetting = fileSizeTypes.First(x => x.Type == FileType.Image);
                if (userfile.Length / 1024f < fileSetting.Size)
                    validFile = true;
                else
                    errorMessage = Message.InvalidFileSize;

            }
            if (extMovie.Contains(ext) && fileSizeTypes.Any(x => x.Type == FileType.Video))
            {
                var fileSetting = fileSizeTypes.First(x => x.Type == FileType.Video);
                if (userfile.Length / 1024f < fileSetting.Size)
                    validFile = true;
                else
                    errorMessage = Message.InvalidFileSize;
            }
            if (extDocument.Contains(ext) && fileSizeTypes.Any(x => x.Type == FileType.Document))
            {
                var fileSetting = fileSizeTypes.First(x => x.Type == FileType.Document);
                if (userfile.Length / 1024f < fileSetting.Size)
                    validFile = true;
                else
                    errorMessage = Message.InvalidFileSize;
            }

            if (validFile)
            {
                string fileString = "";
                if (userfile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        userfile.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        fileString = Convert.ToBase64String(fileBytes);
                        // act on the Base64 data
                    }
                }
                return GeneralResponse<string>.Success("data:image/jpeg;base64," + fileString);
            }
            if (string.IsNullOrEmpty(errorMessage))
                errorMessage = errorMessage + " " + Message.InvalidFile;
            return GeneralResponse<string>.Fail(errorMessage);
        }
        catch (Exception ex)
        {
            return GeneralResponse<string>.Fail(ex);
        }
    }

    public static void checkDirectoryIsExist(string filePath)
    {
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
    }
    public static GeneralResponse DeleteFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return GeneralResponse.Success();
            }
            return GeneralResponse.NotFound();
        }
        catch (Exception ex)
        {
            return GeneralResponse.Fail(ex);
        }
    }

}
public class FileSizeType
{
    public FileType Type { get; set; }
    public long Size { get; set; }

}
public enum FileType
{
    Image,
    Document,
    Video
}