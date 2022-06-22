using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Domain.Interfaces
{
    public interface ILinkRepository
    {
        public Task<Guid> Create(DbLink entity);
        public Task<bool> Delete (Guid id);
        public Task<DbLink?> Get(Guid id);
        public Task<DbLink?> GetByFileId(Guid fileId);

    }
}
