using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class FileRepository : IFileRepository
    {
        private DataContext _dataContext;

        public FileRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Guid> Create(DbFileInfo model)
        {
            var obj = (await _dataContext.Files.AddAsync(model)).Entity;
            if (obj == null)
            {
                return Guid.Empty;
            }

            _dataContext.SaveChanges();

            return obj.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            var obj = await _dataContext.Files.FirstOrDefaultAsync(x => x.Id == id);

            if (obj == null)
            {
                return false;
            }

            _dataContext.Files.Remove(obj);
            int changes = await _dataContext.SaveChangesAsync();

            return changes != 0;
        }

        public async Task<DbFileInfo?> Get(Guid id)
        {
            var obj = await _dataContext.Files.FirstOrDefaultAsync(x => x.Id == id);
            if (obj == null)
            {
                return null;
            }

            return obj;
        }

        public async Task<IEnumerable<DbFileInfo>> GetAll()
        {
            return await _dataContext.Files.ToListAsync();
        }

        public async Task<IQueryable<DbFileInfo>> GetAllAsQuaryable()
        {
            return _dataContext.Files.AsQueryable();
        }

        //public async Task<IQueryable<DbFileModel>> GetAllAsWithoutUrl()
        //{
        //    return _dataContext.Files.AsQueryable().Select(x => new { name = x.Name, type = x.FileType, uri = $"getFile/{x.Uri}" });
        //}

        public async Task<DbFileInfo?> Update(DbFileInfo model)
        {
            if (model == null)
            {
                return null;
            }

            var obj = await _dataContext.Files.FirstOrDefaultAsync(x => x.Id == model.Id);
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

            return _dataContext.SaveChanges() != 0
                ? updatedModel.Entity
                : null;
        }
        public async Task<int> Save()
        {
            return await _dataContext.SaveChangesAsync();
        }

        public async Task<Guid> CreateLink(DbOneTimeLinkModel model)
        {
            var obj = (await _dataContext.Links.AddAsync(model)).Entity;
            if (obj == null)
            {
                return Guid.Empty;
            }

            _dataContext.SaveChanges();

            return obj.Id;
        }

        public async Task<bool> DeleteLink(Guid id)
        {
            var obj = await _dataContext.Links.FirstOrDefaultAsync(x => x.Id == id);

            if (obj == null)
            {
                return false;
            }

            _dataContext.Links.Remove(obj);
            int changes = await _dataContext.SaveChangesAsync();

            return changes != 0;
        }


        public async Task<DbOneTimeLinkModel> GetLink(Guid id)
        {
            var obj = await _dataContext.Links.FirstOrDefaultAsync(x => x.Id == id);
            if (obj == null)
            {
                return null;
            }

            return obj;
        }

        public async Task<DbFileInfo> GetFileByLink(Guid linkId)
        {
            var link = await GetLink(linkId);
            if (link == null)
            {
                return null;
            }

            var file = await Get(link.FileId);
            if (file == null)
            {
                return null;
            }

            return file;
        }


    }
}
