using System;

namespace Service.Models
{
    public enum LoadingStatus
    {
        Success,
        Failed
    }

    public class FileInfoServiceModel
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
        public long SizeInByte { get; set; }
        public string? FileType { get; set; }
        public LoadingStatus Status { get; set; }
    }
}