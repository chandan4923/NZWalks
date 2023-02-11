using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repository;

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
        public IActionResult GetAllRegions()
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

            var regions = _iregionRepository.GetAll().ToList();

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
    }
}
