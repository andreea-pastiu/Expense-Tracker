using AutoMapper;
using ExpenseTracker.Server.Data;
using ExpenseTracker.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ExpenseTracker.Server.Services
{
    public class IncomeService
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;

        public IncomeService(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IncomeDto> CreateIncomeAsync(IncomeDto incomeDto, int userId)
        {
            var income = _mapper.Map<Income>(incomeDto);
            income.UserId = userId;

            _context.Incomes.Add(income);
            await _context.SaveChangesAsync();

            return _mapper.Map<IncomeDto>(income);
        }

        public async Task<List<IncomeDto>> GetIncomesByUserAsync(int userId)
        {
            var incomes = await _context.Incomes
                .Where(i => i.UserId == userId)
                .OrderByDescending(i => i.Date)
                .ToListAsync();

            return _mapper.Map<List<IncomeDto>>(incomes);
        }

        public async Task<IncomeDto?> GetIncomeByIdAsync(int id, int userId)
        {
            var income = await _context.Incomes
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);

            return income == null ? null : _mapper.Map<IncomeDto>(income);
        }

        public async Task<bool> UpdateIncomeAsync(int id, int userId, IncomeDto updatedIncomeDto)
        {
            var income = await _context.Incomes.FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);
            if (income == null)
                return false;

            income.Source = updatedIncomeDto.Source;
            income.Amount = updatedIncomeDto.Amount;
            income.Date = updatedIncomeDto.Date;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteIncomeAsync(int id, int userId)
        {
            var income = await _context.Incomes.FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);
            if (income == null)
                return false;

            _context.Incomes.Remove(income);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
