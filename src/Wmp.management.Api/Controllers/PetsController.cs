using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wmp.management.Api.DataAccess;

namespace Wmp.management.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController(ManagementDbContext dbcontext) : ControllerBase
{
    

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var res = await dbcontext.Pets.Include(p=>p.Breed).ToListAsync();
        return Ok(res);
    }
    [HttpGet("id")]
    public async Task<IActionResult>  Get(int id)
    {
        var pet= await dbcontext.Pets.Include(p => p.Breed).Where(p=>p.Id==id).ToListAsync();
        return Ok(pet);
    }
}
