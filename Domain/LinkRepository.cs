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
            if (obj == null)
            {
                return null;
            }

            return obj;
        }

        public async Task<DbLink?> Update(DbLink entity)
        {
            if (entity == null)
            {
                return null;
            }

            var obj = await _dataContext.Links.FirstOrDefaultAsync(x => x.Id == entity.Id);
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

        public async Task<IEnumerable<DbLink>> GetAll()
        {
            return await _dataContext.Links.ToListAsync();
        }
    }
}
