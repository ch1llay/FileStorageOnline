using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFileInfoRepository : IRepository<DbFileInfo>, IRepositoryGetAllable<DbFileInfo>
    {

    }
}
