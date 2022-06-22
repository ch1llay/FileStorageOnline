using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class DbFileInfo
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }
        [Required]
        public long SizeInByte { get; set; }
        [Required]
        public string? FileType { get; set; }

    }
}
