using ExpenseTracker.Server.Helpers;
using ExpenseTracker.Server.Models;
using ExpenseTracker.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/expenses")]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseService _expenseService;

        public ExpenseController(ExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] ExpenseDto expenseDto)
        {
            if (expenseDto == null || expenseDto.Amount <= 0)
                return BadRequest("Invalid expense data.");

            int userId = HttpContext.GetUserId();
            var newExpense = await _expenseService.CreateExpenseAsync(expenseDto, userId);
            return CreatedAtAction(nameof(GetExpenseById), new { id = newExpense.Id }, newExpense);
        }

        [HttpGet]
        public async Task<IActionResult> GetExpenses()
        {
            int userId = HttpContext.GetUserId();
            var expenses = await _expenseService.GetExpensesByUserAsync(userId);
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseById(int id)
        {
            int userId = HttpContext.GetUserId();
            var expense = await _expenseService.GetExpenseByIdAsync(id, userId);

            if (expense == null)
                return NotFound("Expense not found.");

            return Ok(expense);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(int id, [FromBody] ExpenseDto updatedExpenseDto)
        {
            if (updatedExpenseDto == null || updatedExpenseDto.Amount <= 0)
                return BadRequest("Invalid expense data.");

            int userId = HttpContext.GetUserId();
            bool updated = await _expenseService.UpdateExpenseAsync(id, userId, updatedExpenseDto);

            if (!updated)
                return NotFound("Expense not found or not owned by the user.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            int userId = HttpContext.GetUserId();
            bool deleted = await _expenseService.DeleteExpenseAsync(id, userId);

            if (!deleted)
                return NotFound("Expense not found or not owned by the user.");

            return NoContent();
        }
    }
}
