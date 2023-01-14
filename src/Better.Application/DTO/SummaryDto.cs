using AutoMapper;
using Better.Application.Common.Mappings;
using Better.Core.Entities;

namespace Better.Application.DTO;

public class SummaryDto : IMapFrom<Balance>
{
    public double Balance { get; set; }
    public double CurrentContributions { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Balance, SummaryDto>()
            .ForMember(x => x.Balance, opt => opt.MapFrom(src => src.CurrentAmount))
            .ForMember(x => x.CurrentContributions, opt => opt.MapFrom(src => src.Total));
    }
}