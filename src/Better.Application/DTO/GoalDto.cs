using AutoMapper;
using Better.Application.Common.Mappings;
using Better.Core.Entities;

namespace Better.Application.DTO;

public class GoalDto : IMapFrom<Goal>
{
    public string Title { get; set; }
    public int Years { get; set; }
    public int InitialInvestment { get; set; }
    public int MonthlyContribution { get; set; }
    public int TargetAmount { get; set; }
    public string FinancialEntity { get; set; }
    public string Portfolio { get; set; }
    public DateTime CreatedAt { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Goal, GoalDto>()
            .ForMember(x => x.FinancialEntity, opt => opt.MapFrom(x => x.FinancialEntity.Title ?? string.Empty))
            .ForMember(x => x.Portfolio, opt => opt.MapFrom(x => x.Portfolio.Title ?? string.Empty))
            .ForMember(x => x.CreatedAt, opt => opt.MapFrom(x => x.Created));
    }
}
