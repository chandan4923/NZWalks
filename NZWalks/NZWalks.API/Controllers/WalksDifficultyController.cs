using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("WalksDifficulty")]
    public class WalksDifficultyController:Controller
    {
        private IWalksDifficultyRepository _iwalksDifficultyRepository;
        private IMapper _iMapper;
        public WalksDifficultyController(IWalksDifficultyRepository iwalksDifficultyRepository, IMapper mapper)
        {
            _iwalksDifficultyRepository = iwalksDifficultyRepository;
            _iMapper= mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficultiesAsync()
        {
          var walksDifficulty=await _iwalksDifficultyRepository.GetAllAsync();
            return Ok(walksDifficulty);
        }
        [HttpGet]
        [Route("{ID:guid}")]
        [ActionName("GetWalkDifficultiesAsync")]
        public async Task<IActionResult> GetWalkDifficultiesAsync(Guid ID)
        {
         var walkDifficulty=  await _iwalksDifficultyRepository.GetAsync(ID);
            if (walkDifficulty == null)
            {
                return BadRequest();
            }

            return Ok(walkDifficulty);
        }

        [HttpPut]
        [Route("{ID:guid}")]
        public async Task<IActionResult> UpdateWalkDifficulty([FromRoute]Guid ID, [FromBody] UpdateWalkDifficultyRequest walkDifficultyRequest)
        {
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty()
            {
                Code = walkDifficultyRequest.Code
            };

            walkDifficultyDomain =  await _iwalksDifficultyRepository.UpdateAsync(ID, walkDifficultyDomain);

           walkDifficultyRequest.Code=walkDifficultyDomain.Code;


            return Ok(walkDifficultyRequest);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync(Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty()
            {
                Code = addWalkDifficultyRequest.Code
            };
            walkDifficultyDomain = await _iwalksDifficultyRepository.AddAsync(walkDifficultyDomain);

            var walkDifficultyDTO = _iMapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);
            return CreatedAtAction((nameof(GetWalkDifficultiesAsync),new { ID=walkDifficultyDTO.ID}, walkDifficultyDTO));
        }
        [HttpDelete]
        [Route("{ID:guid}")]
        public void DeleteWalkDifficulty(Guid ID)
        {
            Task<Models.Domain.WalkDifficulty> task = _iwalksDifficultyRepository.DeleteAsync(ID);
            //var result = task.Result;
            task.Wait();


        //return Ok(task);
        }

    }
}
