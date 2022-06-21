using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class LinkRepository : ILinkRepository
    {
        private readonly DataContext _dataContext;

        public LinkRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Guid> Create(DbLink entity)
        {
            var obj = (await _dataContext.Links.AddAsync(entity)).Entity;
            if (obj == null)
            {
                return Guid.Empty;
            }

            await _dataContext.SaveChangesAsync();

            return obj.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            var obj = await _dataContext.Links.FirstOrDefaultAsync(x => x.Id == id);

            if (obj == null)
            {
                return false;
            }

            _dataContext.Links.Remove(obj);
            var changes = await _dataContext.SaveChangesAsync();

            return changes != 0;
        }

        public async Task<DbLink?> Get(Guid id)
        {
            var obj = await _dataContext.Links.FirstOrDefaultAsync(x => x.Id == id);
            return obj;
        }

        public Task<DbLink?> GetByFileId(Guid fileId)
        {
            return _dataContext.Links.FirstOrDefaultAsync(x => x.FileInfoId == fileId);
        }
    }
}
