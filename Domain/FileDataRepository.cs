using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces
{
    public class FileDataRepository : IFileDataRepository
    {

        private readonly DataContext _dataContext;

        public FileDataRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Guid> Create(DbFileData entity)
        {
            var obj = (await _dataContext.DataFiles.AddAsync(entity)).Entity;
            if (obj == null)
            {
                return Guid.Empty;
            }

            await _dataContext.SaveChangesAsync();

            return obj.Id;
        }
        public async Task<DbFileData?> Get(Guid id)
        {
            var obj = await _dataContext.DataFiles.FirstOrDefaultAsync(x => x.Id == id);
            if (obj == null)
            {
                return null;
            }

            return obj;
        }

        public Task<DbFileData> GetByFileInfoId(Guid id)
        {
            return _dataContext.DataFiles.FirstOrDefaultAsync(x=>x.FileInfoId == id);
        }
    }
}
