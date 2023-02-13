using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using System.Runtime.InteropServices;

namespace NZWalks.API.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;
        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Models.Domain.Region> AddAsync(Models.Domain.Region region)
        {
            //Creating a new primary key for the new entry
            region.ID=Guid.NewGuid();
            await _nZWalksDbContext.AddAsync(region);
            await _nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async void DeleteAsync(Guid id)
        {

           var regionToDelete= await _nZWalksDbContext.Regions.FindAsync(id);
           _nZWalksDbContext.Regions.Remove(regionToDelete);
            await _nZWalksDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Models.Domain.Region>> GetAllAsync()
        {
           return await _nZWalksDbContext.Regions.ToListAsync();

        }

        public async Task<Models.Domain.Region> GetAsync(Guid id)
        {
            return await _nZWalksDbContext.Regions.FirstAsync(x => x.ID == id);
        }

        public async Task<Models.Domain.Region> UpdateAsync(Guid id, Models.Domain.Region region)
        {
            var findRegion =await _nZWalksDbContext.Regions.FindAsync(id);

            findRegion.Name = region.Name;
            findRegion.Code = region.Code;
                findRegion.Area = region.Area;
                findRegion.Lat = region.Lat;
                findRegion.Long = region.Long;
                findRegion.Population = region.Population;

           await _nZWalksDbContext.SaveChangesAsync();


            return findRegion;

        }
    }
}
