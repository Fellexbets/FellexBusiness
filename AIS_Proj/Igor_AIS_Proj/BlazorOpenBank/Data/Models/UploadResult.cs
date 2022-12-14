namespace BlazorOpenBank.Data.Models
{
    public class UploadResult
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? StoredFileName { get; set; }
        public string? ContentType { get; set; }

        public int UserId { get; set; }
    }
}
