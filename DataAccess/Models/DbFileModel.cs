using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class DbFileModel
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string? Name { get; set; }
        [Required]
        public long SizeInByte { get; set; }
        [Required]
        public string? Path { get; set; }

    }
}
