namespace Service.Models
{
    public class FileFullModel
    {
        public string? Name { get; set; }
        public long SizeInByte { get; set; }
        public string? FileType { get; set; }
        public byte[] Content { get; set; }
    }
}