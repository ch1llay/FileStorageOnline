using DataAccess.Models;

namespace Domain.Interfaces
{
    public interface ILinkRepository : IRepository<DbLink>, IRepositoryGetAllable<DbLink>
    {
    }
}
