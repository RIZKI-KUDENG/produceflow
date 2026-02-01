using Microsoft.AspNetCore.Mvc;
using ProduceFlow.Application.DTOs.Assets;
using ProduceFlow.Application.Services;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetsController : ControllerBase
{
    private readonly AssetService _service;

    public AssetsController(AssetService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var assets = await _service.GetAllAssetsAsync();
        return Ok(assets);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAssetRequest request)
    {
        try
        {
            var result = await _service.CreateAssetAsync(request);
            return CreatedAtAction(nameof(GetAll), new { }, result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetAssetByIdAsync(id);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAssetAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAssetRequest request)
    {
        try
        {
            await _service.UpdateAssetAsync(id, request);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {

            return NotFound();
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}