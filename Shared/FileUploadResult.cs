namespace Shared
{
    public class FileUploadResult
    {
        public string FileName { get; set; } = null!;

        public string Url { get; set; } = null!;

        public long Size { get; set; }
    }
}
