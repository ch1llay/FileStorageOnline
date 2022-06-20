using DataAccess;
using DataAccess.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain;

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
        if (obj == null)
        {
            return null;
        }

        return obj;
    }

    public async Task<DbFileInfo?> Update(DbFileInfo entity)
    {
        if (entity == null)
        {
            return null;
        }

        var obj = await _dataContext.InfoFiles.FirstOrDefaultAsync(x => x.Id == entity.Id);
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

    public async Task<IEnumerable<DbFileInfo>> GetAll()
    {
        return await _dataContext.InfoFiles.ToListAsync();
    }
}