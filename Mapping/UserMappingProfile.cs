using System.Linq;
using AutoMapper;
using TeamUp.Core.Models;
using TeamUp.Core.ViewModels;

namespace TeamUp.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // From Domain to Api/ViewModel
            CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(uv => uv.HasTeam, opts => opts.MapFrom(u => u.TeamId.HasValue));

            CreateMap<ApplicationUser, MemberEditViewModel>()
                .ForMember(v => v.CityId, opts => opts.MapFrom(u => u.District.CityId))
                .ForMember(v => v.BirthDate, opts => opts.MapFrom(u => u.BirthDate.HasValue ? u.BirthDate.Value.ToString("MM/dd/yyyy") : ""))
                .ForMember(v => v.Positions, opts =>
                    opts.MapFrom(u => u.Positions.Select(up => new PositionViewModel { Id = up.PositionId, Name = up.Position.Name, Selected = true })));

            CreateMap<ApplicationUser, MemberInfoViewModel>()
                .ForMember(v => v.Team,
                    opts => opts.MapFrom(u => u.TeamId.HasValue
                        ? new KeyValuePairViewModel { Id = u.TeamId.Value, Name = u.Team.Name }
                        : null))
                .ForMember(v => v.Age, opts => opts.MapFrom(u => u.Age.HasValue ? u.Age.Value.ToString() : ""))
                .ForMember(v => v.City, opts => opts.MapFrom(u => u.District.City.Name))
                .ForMember(v => v.District, opts => opts.MapFrom(u => u.District.Name))
                .ForMember(v => v.Positions, opts => opts.MapFrom(u => u.Positions.Select(up => up.Position.Name)))
                .ForMember(v => v.StrongFoot, opts => opts.Ignore())
                .AfterMap((u, v) =>
                {
                    if (u.StrongFoot.HasValue)
                        v.StrongFoot = u.StrongFoot.Value ? "Chân phải" : "Chân trái";
                    else
                        v.StrongFoot = "";
                });

            // From Api/ViewModel to Domain
            CreateMap<MemberEditViewModel, ApplicationUser>()
                .ForMember(u => u.Positions, opts => opts.Ignore())
                .AfterMap((v, u) =>
                {
                    var selectedPositions = v.Positions.Where(p => p.Selected).ToList();

                    var addedPositions = selectedPositions.Where(p => u.Positions.All(up => up.PositionId != p.Id))
                        .Select(p => new UserPosition { PositionId = p.Id }).ToList();
                    foreach (var p in addedPositions)
                        u.Positions.Add(p);

                    var removedPositions = u.Positions.Where(up => selectedPositions.All(p => p.Id != up.PositionId))
                        .ToList();
                    foreach (var p in removedPositions)
                        u.Positions.Remove(p);
                });
        }
    }
}
