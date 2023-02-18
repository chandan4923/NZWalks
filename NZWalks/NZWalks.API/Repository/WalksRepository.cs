using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
   
    public class WalksRepository : IWalksRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;
        public WalksRepository(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Walks> AddAsync(Walks walks)
        {
            walks.ID = Guid.NewGuid();
           await _nZWalksDbContext.Walks.AddAsync(walks);
           await _nZWalksDbContext.SaveChangesAsync();
       var walk= await _nZWalksDbContext.Walks.FindAsync(walks.ID);
            return walk;
        }

        public async Task<Walks> DeleteAsync(Guid id)
        {
           var walk= await _nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.ID == id);
            _nZWalksDbContext.Walks.Remove(walk);
            _nZWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walks>> GetAllAsync()
        {
           var walks =
           await _nZWalksDbContext.Walks
          .Include(x=>x.Region)
           .Include(x=>x.WalkDifficulties)
           .ToListAsync();

            return walks;
        }

        public async Task<Walks> GetAsync(Guid id)
        {
            var walk=
           await _nZWalksDbContext.Walks
           .Include(x=>x.Region)
           .Include(x=>x.WalkDifficulties)
           .FirstOrDefaultAsync(x=>x.ID==id);

            return walk;

        }

        public async Task<Walks> UpdateAsync(Guid ID,Walks walks)
        {
           var walk= await _nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.ID == ID);

            walk.Name = walks.Name;
            walk.WalkDifficultyID = walks.WalkDifficultyID;
            walk.Region = walks.Region;
            walk.Length = walks.Length;

            _nZWalksDbContext.Walks.Update(walk);
            await _nZWalksDbContext.SaveChangesAsync();
            return walk;
        }
    }
}
