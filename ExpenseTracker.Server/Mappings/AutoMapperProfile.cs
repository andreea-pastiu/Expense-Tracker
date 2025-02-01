using AutoMapper;
using ExpenseTracker.Server.Data;
using ExpenseTracker.Server.Models;

namespace ExpenseTracker.Server.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Income, IncomeDto>().ReverseMap();
            CreateMap<Expense, ExpenseDto>().ReverseMap();
        }
    }
}
