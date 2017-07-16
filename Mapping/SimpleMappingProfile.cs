using AutoMapper;
using TeamUp.Core.Dtos;
using TeamUp.Core.Models;
using TeamUp.Core.ViewModels;

namespace TeamUp.Mapping
{
    public class SimpleMappingProfile : Profile
    {
        public SimpleMappingProfile()
        {
            // From Domain to Api/ViewModel
            CreateMap<City, CityDto>();
            CreateMap<District, DistrictDto>();
            CreateMap<Position, PositionViewModel>();
            CreateMap<JoinRequest, JoinRequestViewModel>()
                .ForMember(jv => jv.UserName, opts => opts.MapFrom(jr => jr.User.Name))
                .ForMember(jv => jv.TeamName, opts => opts.MapFrom(jr => jr.Team.Name));
        }
    }
}
