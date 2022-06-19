using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class FileRepository : IFileRepository
    {
        DataContext _dataContext;
        public FileRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Guid> Create(DbFileModel model)
        {
            var obj = (await _dataContext.Files.AddAsync(model)).Entity;
            if(obj == null) return Guid.Empty;
            _dataContext.SaveChanges();
            return obj.Id;

        }

        public async Task<bool> Delete(Guid id)
        {
            var obj = await _dataContext.Files.FirstOrDefaultAsync(x=>x.Id == id);
            if(obj == null)
                return false;
            _dataContext.Files.Remove(obj);
            int changes = await _dataContext.SaveChangesAsync();
            return changes != 0;
        }

        public async Task<DbFileModel?> Get(Guid id)
        {
            var obj = await _dataContext.Files.FirstOrDefaultAsync(x => x.Id == id);
            if (obj == null) return null;
            return obj;
        }

        public async Task<IEnumerable<DbFileModel>> GetAll()
        {
            return await _dataContext.Files.ToListAsync();
        }
        public async Task<IQueryable<DbFileModel>> GetAllAsQuaryable()
        {
            return _dataContext.Files.AsQueryable();
        }

        public async Task<DbFileModel?> Update(DbFileModel model)
        {
            if(model == null) return null;
            var obj = await _dataContext.Files.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (obj == null) return null;
            var updatedModel = _dataContext.Entry(obj);
            updatedModel.State = EntityState.Modified;
            if (updatedModel.Entity == null) return null;
            
            return _dataContext.SaveChanges() != 0 ? updatedModel.Entity : null; 
        }
        public async Task<int> Save()
        {
            return await _dataContext.SaveChangesAsync();
        }
    }
}
