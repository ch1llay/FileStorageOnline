using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class DbFileData
    {
        [Key] public Guid Id { get; set; }
        [Required] public Guid FileInfoId { get; set; }
        [Required] public byte[] Content { get; set; }
    }
}