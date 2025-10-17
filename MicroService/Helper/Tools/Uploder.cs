using Microsoft.AspNetCore.Http;

public class Uploder
{
    private static string[] extImage = { ".jpg", ".png", ".jpeg" };
    private static string[] extMovie = { ".mp4", ".mkv", ".3gp" };
    private static string[] extDocument = { ".pdf", ".doc", ".3gp", ".docx", ".txt", ".xlsx", ".xls", ".xlt" };
    public static async Task<UploderResult> UploadFile(IFormFile userfile, string fileName, List<FileSizeType> fileSizeTypes)
    {
        UploderResult res = new UploderResult();
        var serverPath = Directory.GetCurrentDirectory() + "\\wwwroot";
        var ext = Path.GetExtension(userfile.FileName);
        bool validFile = false;
        try
        {
            if (extImage.Contains(ext) && fileSizeTypes.Any(x => x.Type == FileType.Image))
            {
                var fileSetting = fileSizeTypes.First(x => x.Type == FileType.Image);
                if (userfile.Length / 1024f < fileSetting.Size)
                    validFile = true;
                else
                    res.ErrorMessage = Message.InvalidFileSize;

            }
            if (extMovie.Contains(ext) && fileSizeTypes.Any(x => x.Type == FileType.Video))
            {
                var fileSetting = fileSizeTypes.First(x => x.Type == FileType.Video);
                if (userfile.Length / 1024f < fileSetting.Size)
                    validFile = true;
                else
                    res.ErrorMessage = Message.InvalidFileSize;
            }
            if (extDocument.Contains(ext) && fileSizeTypes.Any(x => x.Type == FileType.Document))
            {
                var fileSetting = fileSizeTypes.First(x => x.Type == FileType.Document);
                if (userfile.Length / 1024f < fileSetting.Size)
                    validFile = true;
                else
                    res.ErrorMessage = Message.InvalidFileSize;
            }

            if (validFile)
            {
                string file = serverPath + fileName + ext;

                using (var stream = new FileStream(file, FileMode.Create))
                {
                    await userfile.CopyToAsync(stream);
                }
                res.isSuccess = true;
                res.Url = fileName + ext;
                return res;
            }
            if (string.IsNullOrEmpty(res.ErrorMessage))
                res.ErrorMessage = res.ErrorMessage + " " + Message.InvalidFile;

            return res;
        }
        catch (Exception ex)
        {
            res.ErrorMessage = ex.Message;
            res.isSuccess = false;
            return res;
        }
    }

    public static void checkDirectoryIsExist(string filePath)
    {
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
    }
    public static UploderResult DeleteFile(string filePath)
    {
        UploderResult res = new UploderResult();
        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                res.isSuccess = true;
                return res;
            }
            res.ErrorMessage = "فایل یافت نشد";
            res.isSuccess = true;
            return res;
        }
        catch (Exception ex)
        {
            res.ErrorMessage = ex.Message;
            res.isSuccess = false;
            return res;
        }
    }

}
public class UploderResult
{
    public bool isSuccess { get; set; }
    public string ErrorMessage { get; set; }
    public string Url { get; set; }

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