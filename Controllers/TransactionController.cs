using Microsoft.AspNetCore.Mvc;
using CarRentalSystemAPI.Data;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class TransactionLogController : ControllerBase
{
    private readonly CarDbContext _context;

    public TransactionLogController(CarDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetLogs()
    {
        var logs = await _context.TransactionLogs
            .OrderByDescending(log => log.Timestamp)
            .ToListAsync();

        return Ok(logs);
    }

}
