using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repository
{
    public class WalksDifficultyRepository:IWalksDifficultyRepository
    {private readonly NZWalksDbContext _nzWalksDbContext;
        public WalksDifficultyRepository(NZWalksDbContext nZWalksDbContext) 
        {
            _nzWalksDbContext = nZWalksDbContext;
        }

        public async Task<Models.Domain.WalkDifficulty> AddAsync(Models.Domain.WalkDifficulty walkDifficultyDomain)
        {
            Guid guid= Guid.NewGuid();
            walkDifficultyDomain.ID = guid;
         await _nzWalksDbContext.WalkDifficulties.AddAsync(walkDifficultyDomain);
          await  _nzWalksDbContext.SaveChangesAsync();
            var wD = _nzWalksDbContext.WalkDifficulties.Find(walkDifficultyDomain.ID);
            return wD;
        }

        public async Task<Models.Domain.WalkDifficulty> DeleteAsync(Guid ID)
        {
            var walkDifficulty = await _nzWalksDbContext.WalkDifficulties.FirstOrDefaultAsync(x => x.ID == ID);
            _nzWalksDbContext.WalkDifficulties.Remove(walkDifficulty);
           await _nzWalksDbContext.SaveChangesAsync();

            return walkDifficulty;
        }

        public async Task<List<Models.Domain.WalkDifficulty>> GetAllAsync()
        {
           var walksDifficulties=await _nzWalksDbContext.WalkDifficulties.ToListAsync();
            return walksDifficulties;
        }

        public async Task<Models.Domain.WalkDifficulty> GetAsync(Guid id)
        {
          var walkDifficulty= await _nzWalksDbContext.WalkDifficulties.FirstOrDefaultAsync(x=>x.ID==id);
            return walkDifficulty;
        }

        public async Task<Models.Domain.WalkDifficulty> UpdateAsync(Guid ID, Models.Domain.WalkDifficulty walkDifficultyDomain)
        {
           var walkDifficulty= await _nzWalksDbContext.WalkDifficulties.FirstOrDefaultAsync(x => x.ID == ID);
            walkDifficulty.Code = walkDifficultyDomain.Code;
          var walkDificultyAfterUpdate=  _nzWalksDbContext.WalkDifficulties.Update(walkDifficulty);
           await _nzWalksDbContext.SaveChangesAsync();

            return walkDifficulty;
        }
    }
}
