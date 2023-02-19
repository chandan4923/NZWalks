using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public interface IWalksDifficultyRepository
    {
        Task<List<WalkDifficulty>> GetAllAsync();

        Task<WalkDifficulty> GetAsync(Guid ID);

        Task<WalkDifficulty> UpdateAsync(Guid ID,WalkDifficulty walkDifficulty);

        Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficultyDomain);

        Task<WalkDifficulty> DeleteAsync(Guid ID);

    }
}
