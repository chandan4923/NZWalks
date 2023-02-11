using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;
        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext = nZWalksDbContext;
        }
        public IEnumerable<Region> GetAll()
        {
           return _nZWalksDbContext.Regions.ToList();

        }
    }
}
