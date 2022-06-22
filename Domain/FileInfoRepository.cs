using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain
{

    public class FileInfoRepository : IFileInfoRepository
    {
        private readonly DataContext _dataContext;

        public FileInfoRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Guid> Create(DbFileInfo entity)
        {
            var obj = (await _dataContext.InfoFiles.AddAsync(entity)).Entity;
            if (obj == null)
            {
                return Guid.Empty;
            }

            await _dataContext.SaveChangesAsync();

            return obj.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            var obj = await _dataContext.InfoFiles.FirstOrDefaultAsync(x => x.Id == id);

            if (obj == null)
            {
                return false;
            }

            _dataContext.InfoFiles.Remove(obj);
            var changes = await _dataContext.SaveChangesAsync();

            return changes != 0;
        }

        public async Task<DbFileInfo?> Get(Guid id)
        {
            var obj = await _dataContext.InfoFiles.FirstOrDefaultAsync(x => x.Id == id);
            return obj;
        }

        public async Task<List<DbFileInfo>> GetAll()
        {
            return await _dataContext.InfoFiles.ToListAsync();
        }

    }
}