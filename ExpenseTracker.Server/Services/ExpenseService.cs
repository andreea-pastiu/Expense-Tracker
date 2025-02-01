using AutoMapper;
using ExpenseTracker.Server.Data;
using ExpenseTracker.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Server.Services
{
    public class ExpenseService
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;

        public ExpenseService(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExpenseDto> CreateExpenseAsync(ExpenseDto expenseDto, int userId)
        {
            var expense = _mapper.Map<Expense>(expenseDto);
            expense.UserId = userId;

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return _mapper.Map<ExpenseDto>(expense);
        }

        public async Task<List<ExpenseDto>> GetExpensesByUserAsync(int userId)
        {
            var expenses = await _context.Expenses
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.Date)
                .ToListAsync();

            return _mapper.Map<List<ExpenseDto>>(expenses);
        }

        public async Task<ExpenseDto?> GetExpenseByIdAsync(int id, int userId)
        {
            var expense = await _context.Expenses
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            return expense == null ? null : _mapper.Map<ExpenseDto>(expense);
        }

        public async Task<bool> UpdateExpenseAsync(int id, int userId, ExpenseDto updatedExpenseDto)
        {
            var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
            if (expense == null)
                return false;

            expense.Description = updatedExpenseDto.Description;
            expense.Amount = updatedExpenseDto.Amount;
            expense.Date = updatedExpenseDto.Date;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteExpenseAsync(int id, int userId)
        {
            var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
            if (expense == null)
                return false;

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
