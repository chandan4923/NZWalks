using Microsoft.Identity.Client;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();

      Task<Region>  GetAsync(Guid id);

        Task<Region> AddAsync(Region region);

         void DeleteAsync(Guid id);

        Task<Region> UpdateAsync(Guid id, Region region);
    }
}
