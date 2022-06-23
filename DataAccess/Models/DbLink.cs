using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class DbLink
    {
        [Key] public Guid Id { get; set; }
        [Required] public Guid FileInfoId { get; set; }
        [Required] public DateTime UploadDate { get; set; }
    }
}