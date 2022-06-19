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

        public async Task<Guid> CreateFileInfo(DbFileInfo model)
        {
            var obj = (await _dataContext.InfoFiles.AddAsync(model)).Entity;
            if (obj == null)
            {
                return Guid.Empty;
            }

            _dataContext.SaveChanges();

            return obj.Id;
        }

        public async Task<bool> DeleteFileInfo(Guid id)
        {
            var obj = await _dataContext.InfoFiles.FirstOrDefaultAsync(x => x.Id == id);

            if (obj == null)
            {
                return false;
            }

            _dataContext.InfoFiles.Remove(obj);
            int changes = await _dataContext.SaveChangesAsync();

            return changes != 0;
        }

        public async Task<DbFileInfo?> GetFileInfo(Guid id)
        {
            var obj = await _dataContext.InfoFiles.FirstOrDefaultAsync(x => x.Id == id);
            if (obj == null)
            {
                return null;
            }

            return obj;
        }

        public async Task<IEnumerable<DbFileInfo>> GetAllFileInfo()
        {
            return await _dataContext.InfoFiles.ToListAsync();
        }

        public async Task<IQueryable<DbFileInfo>> GetAllAsQuaryable()
        {
            return _dataContext.InfoFiles.AsQueryable();
        }

        //public async Task<IQueryable<DbFileModel>> GetAllAsWithoutUrl()
        //{
        //    return _dataContext.Files.AsQueryable().Select(x => new { name = x.Name, type = x.FileType, uri = $"getFile/{x.Uri}" });
        //}

        public async Task<DbFileInfo?> UpdateFileInfo(DbFileInfo model)
        {
            if (model == null)
            {
                return null;
            }

            var obj = await _dataContext.InfoFiles.FirstOrDefaultAsync(x => x.Id == model.Id);
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

        public async Task<DbFileData> GetFileByLink(Guid linkId)
        {
            var link = await GetLink(linkId);
            if (link == null)
            {
                return null;
            }

            var file = await GetFileData(link.FileInfoId);
            if (file == null)
            {
                return null;
            }

            return file;
        }

        public async Task<Guid> CreateFileData(DbFileData model)
        {
            var obj = (await _dataContext.DataFiles.AddAsync(model)).Entity;
            if (obj == null)
            {
                return Guid.Empty;
            }

            _dataContext.SaveChanges();

            return obj.Id;
        }

        public async Task<DbFileData> GetFileData(Guid id)
        {
 
            var fileData = await _dataContext.DataFiles.FirstOrDefaultAsync(x=> x.Id == id);
            if (fileData == null)
            {
                return null;
            }

            return fileData;
        }

    }
}
