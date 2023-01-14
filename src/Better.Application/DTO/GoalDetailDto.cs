using AutoMapper;
using Better.Application.Common.Mappings;
using Better.Core.Entities;

namespace Better.Application.DTO;

public class GoalDetailDto : IMapFrom<Goal>
{
    public string Title { get; set; }
    public int Years { get; set; }
    public int InitialInvestment { get; set; }
    public int MonthlyContribution { get; set; }
    public int TargetAmount { get; set; }
    public string Category { get; set; }
    public string FinantialEntity { get; set; }
    public decimal TotalContributions { get; set; }
    public decimal TotalWithdrawal { get; set; }
    public decimal Percentaje { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Goal, GoalDetailDto>()
            .ForMember(x => x.Category, opt => opt.MapFrom(src => src.GoalCategory.Title))
            .ForMember(x => x.FinantialEntity, opt => opt.MapFrom(src => src.FinancialEntity.Title ?? string.Empty));
    }
}
