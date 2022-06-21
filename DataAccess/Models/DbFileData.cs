using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class DbFileData
    {
        public Guid Id { get; set; }
        public Guid FileInfoId { get; set; }
        public byte[] Content { get; set; }

    }
}
