using AutoMapper;
using Better.Application.Common.Mappings;
using Better.Core.Entities;

namespace Better.Application.Customers.DTO;

public class UserDto : IMapFrom<User>
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Advisor { get; set; }
    public DateTime CreatedAt { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserDto>()
            .ForMember(x => x.Username, opt => opt.MapFrom(src => $"{src.Firstname} {src.Surname}"))
            .ForMember(x => x.Advisor, opt => opt.MapFrom(src => src.Advisor == null ? string.Empty : $"{src.Advisor.Firstname} {src.Advisor.Surname}"))
            .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => src.Created));
    }
}
