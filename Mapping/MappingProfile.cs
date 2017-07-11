using AutoMapper;
using TeamUp.Core.Dtos;
using TeamUp.Core.Models;
using TeamUp.Core.ViewModels;

namespace TeamUp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // From Domain to Api/ViewModel
            CreateMap<City, CityDto>();
            CreateMap<District, DistrictDto>();
            CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(uv => uv.HasTeam, opts => opts.MapFrom(u => u.TeamId.HasValue));
            CreateMap<Team, TeamViewModel>()
                .ForMember(tv => tv.JoinRequests, opts => opts.Ignore())
                .ForMember(tv => tv.District, opts => opts.MapFrom(t => t.District.Name))
                .ForMember(tv => tv.City, opts => opts.MapFrom(t => t.District.City.Name));
            CreateMap<Team, TeamEditViewModel>()
                .ForMember(tv => tv.InitialName, opts => opts.MapFrom(t => t.Name))
                .ForMember(tv => tv.DistrictId, opts => opts.MapFrom(t => t.District.Id))
                .ForMember(tv => tv.CityId, opts => opts.MapFrom(t => t.District.CityId));
            CreateMap<JoinRequest, JoinRequestViewModel>()
                .ForMember(jv => jv.UserName, opts => opts.MapFrom(jr => jr.User.Name))
                .ForMember(jv => jv.TeamName, opts => opts.MapFrom(jr => jr.Team.Name));



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
