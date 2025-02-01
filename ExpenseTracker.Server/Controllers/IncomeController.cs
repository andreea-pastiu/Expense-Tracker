using ExpenseTracker.Server.Helpers;
using ExpenseTracker.Server.Models;
using ExpenseTracker.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Server.Controllers
{
    [Authorize]  // Requires authentication for all endpoints
    [ApiController]
    [Route("api/incomes")]
    public class IncomeController : ControllerBase
    {
        private readonly IncomeService _incomeService;

        public IncomeController(IncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncome([FromBody] IncomeDto incomeDto)
        {
            if (incomeDto == null || incomeDto.Amount <= 0)
                return BadRequest("Invalid income data.");

            int userId = HttpContext.GetUserId();
            var newIncome = await _incomeService.CreateIncomeAsync(incomeDto, userId);
            return CreatedAtAction(nameof(GetIncomeById), new { id = newIncome.Id }, newIncome);
        }

        [HttpGet]
        public async Task<IActionResult> GetIncomes()
        {
            int userId = HttpContext.GetUserId();
            var incomes = await _incomeService.GetIncomesByUserAsync(userId);
            return Ok(incomes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIncomeById(int id)
        {
            int userId = HttpContext.GetUserId();
            var income = await _incomeService.GetIncomeByIdAsync(id, userId);

            if (income == null)
                return NotFound("Income not found.");

            return Ok(income);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncome(int id, [FromBody] IncomeDto updatedIncomeDto)
        {
            if (updatedIncomeDto == null || updatedIncomeDto.Amount <= 0)
                return BadRequest("Invalid income data.");

            int userId = HttpContext.GetUserId();
            bool updated = await _incomeService.UpdateIncomeAsync(id, userId, updatedIncomeDto);

            if (!updated)
                return NotFound("Income not found or not owned by the user.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            int userId = HttpContext.GetUserId();
            bool deleted = await _incomeService.DeleteIncomeAsync(id, userId);

            if (!deleted)
                return NotFound("Income not found or not owned by the user.");

            return NoContent();
        }
    }
}
