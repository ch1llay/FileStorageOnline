using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class DbOneTimeLinkModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid FileInfoId { get; set; }
    }
}
