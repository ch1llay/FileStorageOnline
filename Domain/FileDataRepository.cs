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

        public async Task<DbFileData?> Update(DbFileData entity)
        {
            if (entity == null)
            {
                return null;
            }

            var obj = await _dataContext.DataFiles.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (obj == null)
            {
                return null;
            }

            var updatedModel = _dataContext.Entry(obj);
            updatedModel.State = EntityState.Modified;
            if (updatedModel.Entity == null)
            {
                return null;
            }

            return await _dataContext.SaveChangesAsync() != 0
                ? updatedModel.Entity
                : null;
        }

        public async Task<bool> Delete(Guid id)
        {
            var obj = await _dataContext.DataFiles.FirstOrDefaultAsync(x => x.Id == id);

            if (obj == null)
            {
                return false;
            }
            var changes = await _dataContext.SaveChangesAsync();

            return changes != 0;
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
    }
}
