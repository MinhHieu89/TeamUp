using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeamUp.Core;
using TeamUp.Core.Dtos;
using TeamUp.Core.Models;

namespace TeamUp.Controllers.Api
{
    [Route("/api/cities")]
    public class CitiesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CitiesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: /api/cities
        [HttpGet]
        public IEnumerable<CityDto> GetCities()
        {
            var cities = _unitOfWork.Locations.GetCities();
            return _mapper.Map<IEnumerable<City>, IEnumerable<CityDto>>(cities);
        }

        // GET: /api/cities/1
        [HttpGet("{cityId}")]
        public IEnumerable<DistrictDto> GetDistricts(int cityId)
        {
            var districts = _unitOfWork.Locations.GetDistricts(cityId);
            return _mapper.Map<IEnumerable<District>, IEnumerable<DistrictDto>>(districts);
        }
    }
}
