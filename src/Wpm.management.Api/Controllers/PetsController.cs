using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wpm.management.Api.DataAccess;

namespace Wpm.management.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController(ManagementDbContext dbcontext) : ControllerBase
{


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var res = await dbcontext.Pets.Include(p => p.Breed).ToListAsync();
        return Ok(res);
    }
    [HttpGet("id", Name = nameof(GetById))]
    public async Task<IActionResult> GetById(int id)
    {
        var pet = await dbcontext.Pets.Include(p => p.Breed).Where(p => p.Id == id).ToListAsync();
        return Ok(pet);
    }

    [HttpPost]
    public async Task<IActionResult> Create(NewPet newPet)
    {
        var pet = newPet.ToPet();
        await dbcontext.Pets.AddAsync(pet);
        await dbcontext.SaveChangesAsync();
        return CreatedAtRoute(nameof(GetById), new { id = pet.Id },pet);
        //return Ok(pet);
    }

    public record NewPet(string name, int age, int breedId)
    {
        public Pet ToPet()
        {
            return new Pet() { Name = name, Age = age, BreedId = breedId };
        }
    }
}
