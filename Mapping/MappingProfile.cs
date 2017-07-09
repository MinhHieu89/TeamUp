using AutoMapper;
using TeamUp.Controllers.Resources;
using TeamUp.Models;
using TeamUp.Models.ViewModels;

namespace TeamUp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // From Domain to Api/ViewModel
            CreateMap<City, CityResource>();
            CreateMap<District, DistrictResource>();
            CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(uv => uv.HasTeam, opts => opts.MapFrom(u => u.TeamId.HasValue));
            CreateMap<Team, TeamDetailViewModel>()
                .ForMember(tv => tv.District, opts => opts.MapFrom(t => t.District.Name))
                .ForMember(tv => tv.City, opts => opts.MapFrom(t => t.District.City.Name));
            CreateMap<Team, TeamEditViewModel>()
                .ForMember(tv => tv.DistrictId, opts => opts.MapFrom(t => t.District.Id))
                .ForMember(tv => tv.CityId, opts => opts.MapFrom(t => t.District.CityId));



            // From Api/ViewModel to Domain
            CreateMap<SaveTeamViewModel, Team>()
                .ForMember(t => t.Id, opts => opts.Ignore());
            CreateMap<TeamEditViewModel, Team>()
                .ForMember(t => t.Id, opts => opts.Ignore())
                .ForMember(t => t.Members, opts => opts.Ignore())
                .ForMember(t => t.CreatedDate, opts => opts.Ignore())
                .ForMember(t => t.Logo, opts => opts.Ignore())
                .ForMember(t => t.CaptainId, opts => opts.Ignore())
                .ForMember(t => t.Captain, opts => opts.Ignore());
        }
    }
}
