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
            return CreatedAtAction(nameof(GetAll), new {}, result);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}