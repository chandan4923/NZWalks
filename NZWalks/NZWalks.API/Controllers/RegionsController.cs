using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;
using System.Runtime.InteropServices;
using System.Threading;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("Regions")]
    public class RegionsController : Controller
    {
        private IRegionRepository _iregionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            _iregionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            //List<Region> regions = new List<Region>()
            //{
            //    new Region()
            //    {
            //        ID=Guid.NewGuid(),
            //        Name="Wellington",
            //        Code="WLG",
            //        Area="227755",
            //        Lat=Convert.ToDecimal(-1.8822),
            //        Long=Convert.ToDecimal(299.88),
            //        Population=500000,
            //    },
            //    new Region()
            //    {
            //        ID=Guid.NewGuid(),
            //        Name="Auckland",
            //        Code="Auck",
            //        Area="227755",
            //        Lat=Convert.ToDecimal(-1.8822),
            //        Long=Convert.ToDecimal(299.88),
            //        Population=500000,
            //    }
            //};

            var regions = await _iregionRepository.GetAllAsync();

           var regionsDTO= mapper.Map<List<Models.DTO.Region>>(regions);

            //var regionsDTOList = new List<Models.DTO.Region>();

            //regions.ForEach(region =>
            //{
            //   var regionDTO= new Models.DTO.Region()
            //    {
            //        ID = region.ID,
            //        Name = region.Name,
            //        Code= region.Code,
            //        Area= region.Area,
            //        Lat= region.Lat,
            //        Long=region.Long,
            //        PopulationChandan=region.Population,
            //    };
            //    regionsDTOList.Add(regionDTO);
            //});


            return Ok(regionsDTO);
        }
        [HttpGet()]
        [Route("{ID:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid ID)
        {
           var region= await _iregionRepository.GetAsync(ID);

            if(region==null)
            {
                return NotFound();
            }
            Models.DTO.Region regionDTO =mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);
        }
        [HttpPost()]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            if (addRegionRequest == null)
            {
                return NoContent();
            }

            var region = new Models.Domain.Region()
            {
                Name = addRegionRequest.Name,
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population,
            };


            var _region = await _iregionRepository.AddAsync(region);

            Models.DTO.Region regionDTO=mapper.Map<Models.DTO.Region>(region);

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.ID }, regionDTO);
        }
        [HttpDelete()]
        [Route("{ID:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid ID)
        {

            var region = await _iregionRepository.GetAsync(ID);
            if (region == null)
            {
                return NoContent();
            }


            //AsyncHelper.RunSync<Timer>(() => manager.FindByIdAsync(userId))

            //var result = task.WaitAndUnwrapException();

            //var task = Task.Run(async () => await _iregionRepository.DeleteAsync(ID));




            //Task t1 = new Task(() => _iregionRepository.DeleteAsync(ID));


            //await t1;

            //t1.Start();
            //t1.Wait(10000);

            _iregionRepository.DeleteAsync(ID);

            return Ok("Delete Successful");
        }
        [HttpPut()]
        [Route("{ID:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute]Guid ID,[FromBody]Models.DTO.UpdateRegionRequest regionDTO)
        {


            var regionModel = new Models.Domain.Region()
            {
                Name = regionDTO.Name,
                Code = regionDTO.Code,
                Area = regionDTO.Area,
                Lat = regionDTO.Lat,
                Long = regionDTO.Long,
                Population = regionDTO.Population,
            };

          var updatedRegion= await _iregionRepository.UpdateAsync(ID,regionModel);

            if (regionModel == null)
            {
                return NotFound();
            }

            regionDTO.Name = updatedRegion.Name;
            regionDTO.Code = updatedRegion.Code;
            regionDTO.Area = updatedRegion.Area;
            regionDTO.Lat = updatedRegion.Lat;
            regionDTO.Long = updatedRegion.Long;
            regionDTO.Population = updatedRegion.Population;

            return Ok("Updates successfully!");
        }

    }
}
