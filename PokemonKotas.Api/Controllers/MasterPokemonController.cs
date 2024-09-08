using Microsoft.AspNetCore.Mvc;
using PokemonKotas.Domain.Dto;
using PokemonKotas.Domain.Interfaces;
using PokemonKotas.Infra.Helper;

namespace PokemonKotas.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MasterPokemonController(IMasterPokemonService service) : Controller
{
    /// <summary>
    ///     Retrieves all master Pokémon entities from the repository.
    /// </summary>
    /// <returns>
    ///     An <see cref="IActionResult" /> containing a list of master Pokémon entities if found,
    ///     otherwise a <see cref="NotFoundResult" />.
    /// </returns>
    /// <response code="200">If the master Pokémon entities were successfully retrieved.</response>
    /// <response code="404">If no master Pokémon entities were found.</response>
    /// <response code="400">If there was an error in the request.</response>
    [HttpGet]
    [Route("GetAll")]
    [EndpointSummary("Retrieves all master Pokémon entities from the repository.")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var masters = await service.GetAllMasters().ConfigureAwait(false);
            if (masters == null) return NotFound();
            return Ok(masters);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    ///     Retrieves a master Pokémon entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the master Pokémon.</param>
    /// <returns>
    ///     An <see cref="IActionResult" /> containing the master Pokémon entity if found, otherwise a
    ///     <see cref="NotFoundResult" />.
    /// </returns>
    /// <response code="200">If the master Pokémon entity was successfully retrieved.</response>
    /// <response code="404">If the master Pokémon entity was not found.</response>
    /// <response code="400">If there was an error in the request.</response>
    [HttpGet]
    [Route("Get/{id:int}")]
    [EndpointSummary("Retrieves a master Pokémon entity by its unique identifier.")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var masters = await service.GetMasterById(id); //.ConfigureAwait(false);
            if (masters == null) return NotFound();
            return Ok(Task.FromResult(masters));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    ///     Adds a new master Pokémon to the repository.
    /// </summary>
    /// <param name="masterPokemon">The master Pokémon data transfer object containing the details of the Pokémon to be added.</param>
    /// <returns>
    ///     An <see cref="IActionResult" /> indicating the result of the operation.
    ///     If the master Pokémon was successfully added, returns an <see cref="OkObjectResult" /> with the result.
    ///     If the master Pokémon was not modified, returns a <see cref="StatusCodeResult" /> with status code 304.
    ///     If there was an error, returns a <see cref="BadRequestObjectResult" /> with the error message.
    /// </returns>
    /// <response code="200">If the master Pokémon was successfully added.</response>
    /// <response code="304">If the master Pokémon was not modified.</response>
    /// <response code="400">If there was an error in the request.</response>
    [HttpPost]
    [Route("AddMasterPokemon")]
    [EndpointSummary("Adds a new master Pokémon to the repository.")]
    public async Task<IActionResult> AddMasterPokemon(MasterPokemonDto masterPokemon)
    {
        try
        {
            masterPokemon.ResetIds();
            await service.Clear();
            var result = await service.AddMasterPokemonAsync(masterPokemon);
            if (result > 0) return Ok(result);
            return StatusCode(304);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("GetRank")]
    public async Task<IActionResult> GetRank()
    {
        try
        {
            var result = await service.GetRanking();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    ///     Adds a captured Pokémon to the specified master Pokémon.
    /// </summary>
    /// <param name="masterPokemonId">The unique identifier of the master Pokémon to which the captured Pokémon will be added.</param>
    /// <param name="pokemon">The <see cref="PokemonDto" /> representing the captured Pokémon to be added.</param>
    /// <returns>
    ///     An <see cref="IActionResult" /> indicating the result of the operation.
    /// </returns>
    /// <response code="200">If the captured Pokémon was successfully added.</response>
    /// <response code="400">If there was an error in the request.</response>
    [HttpPut]
    [Route("AddCapturedPokemon/{masterPokemonId:int}")]
    [EndpointSummary("Adds a captured Pokémon to the specified master Pokémon.")]
    public async Task<IActionResult> AddCapturedPokemon(int masterPokemonId, [FromBody] PokemonDto pokemon)
    {
        try
        {
            pokemon.ResetIds();
            var result = await service.AddCapturedPokemonAsync(masterPokemonId, pokemon).ConfigureAwait(false);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}