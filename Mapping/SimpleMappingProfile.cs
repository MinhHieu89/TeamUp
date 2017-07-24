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
            CreateMap<Request, JoinRequestViewModel>()
                .ForMember(v => v.UserName, opts => opts.MapFrom(r => r.User.Name))
                .ForMember(v => v.TeamName, opts => opts.MapFrom(r => r.Team.Name))
                .ForMember(v => v.CreatedTime, opts => opts.MapFrom(r => r.CreatedTime.ToString("HH:mm tt - dd/MM/yyyy")))
                .ForMember(v => v.UpdatedTime, opts => opts.MapFrom(r => r.UpdatedTime.ToString("HH:mm tt - dd/MM/yyyy")));

        }
    }
}
