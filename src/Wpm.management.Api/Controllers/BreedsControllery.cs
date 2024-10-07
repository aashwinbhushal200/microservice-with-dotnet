using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Wpm.management.Api.DataAccess;

namespace Wpm.management.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BreedsController(ManagementDbContext dbcontext,ILogger<BreedsController> logger) : ControllerBase
{


    [HttpGet]
    public async Task<IActionResult> GetBreeds()
    {
        var res = await dbcontext.Breeds.ToListAsync();
        return Ok(res);
    }
    [HttpGet("id", Name = nameof(GetBreedById))]
    public async Task<IActionResult> GetBreedById(int id)
    {
        var breed = await dbcontext.Breeds.Where(p => p.id == id).ToListAsync();
        return Ok(breed);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBreed(NewBreed newBreed)
    {
        try {
            var breed = newBreed.ToBreed();
            await dbcontext.Breeds.AddAsync(breed);
            await dbcontext.SaveChangesAsync();
            return CreatedAtRoute(nameof(GetBreedById), new { id = breed.id }, breed);
        }
        catch(Exception ex) {
            logger?.LogError(ex.ToString());
            return StatusCode((int)HttpStatusCode.InternalServerError);

        }
       
    }

    public record NewBreed(string name)
    {
        public Breed ToBreed()
        {
            return new Breed(0,name);
        }
    }
}
