using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public interface IWalksRepository
    {
        Task<IEnumerable<Walks>> GetAllAsync();

        Task<Walks> GetAsync(Guid id);
        Task<Walks> AddAsync(Walks walks);
        Task<Walks> DeleteAsync(Guid id);

        Task<Walks> UpdateAsync(Guid ID,Walks walks);
    }
}
