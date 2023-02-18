using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("Walks")]
    public class WalksController :Controller
    {
        private readonly IWalksRepository _iwalksRepository;
        private readonly IMapper mapper;

        public WalksController(IWalksRepository walksRepository, IMapper mapper)
        {
            _iwalksRepository = walksRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {


            var walks= await _iwalksRepository.GetAllAsync();

            var walkDTO = mapper.Map<List<Models.DTO.Walks>>(walks);


            return Ok(walkDTO);
        }

        [HttpGet]
        [Route("{ID:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid ID)
        {
           var walk=await _iwalksRepository.GetAsync(ID);

            var walkDTO = mapper.Map<Models.DTO.Walks>(walk);

            return Ok(walkDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddWalkAsync(AddWalksRequest addWalksRequest)
        {
            Models.Domain.Walks walkDomain = new Models.Domain.Walks();
            walkDomain.Name = addWalksRequest.Name;
            walkDomain.Length = addWalksRequest.Length;
            walkDomain.RegionID = addWalksRequest.RegionID;
            walkDomain.WalkDifficultyID = addWalksRequest.WalkDifficultyID;
            var walk = await _iwalksRepository.AddAsync(walkDomain);

            Models.DTO.Walks walksDTO = new Models.DTO.Walks() {
               Name = walkDomain.Name,
            Length = walkDomain.Length,
            RegionID = walkDomain.RegionID,
           WalkDifficultyID = walkDomain.WalkDifficultyID};
            return CreatedAtAction(nameof(GetWalkAsync), new {ID=}
        }
        [HttpDelete]
        [Route("{ID:guid}")]
        public async Task<IActionResult> DeleteWalksAsync(Guid ID)
        {
             return Ok(await _iwalksRepository.DeleteAsync(ID));
        }
        [HttpPut]
        [Route("{ID:guid}")]
        public async Task<IActionResult> UpdateWalksAsync([FromRoute]Guid ID, [FromBody] UpdateWalksRequest walksDTO)
        {
            Models.Domain.Walks walksDomain = new Models.Domain.Walks()
            {
                Name = walksDTO.Name,
                Length=walksDTO.Length,
                RegionID = walksDTO.RegionID,
                WalkDifficultyID=walksDTO.WalkDifficultyID,
            };

           var walkFromUpdate= await _iwalksRepository.UpdateAsync(ID, walksDomain);

            walksDTO.Name=walkFromUpdate.Name;
            walksDTO.Length = walkFromUpdate.Length;
            walksDTO.WalkDifficultyID = walkFromUpdate.WalkDifficultyID;
            walksDTO.RegionID = walkFromUpdate.RegionID;

            return Ok(walksDTO);
        }
    }
}
