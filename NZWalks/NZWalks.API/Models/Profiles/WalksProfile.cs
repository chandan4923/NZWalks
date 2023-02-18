using AutoMapper;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.Profiles
{
    public class WalksProfile:Profile
    {
        public WalksProfile()
        {
            CreateMap<Models.Domain.Walks, Models.DTO.Walks>()
              .ReverseMap();

            CreateMap<Models.Domain.WalkDifficulty, Models.DTO.WalkDifficulty>()
              .ReverseMap();
        }
    }
}
