using Microsoft.AspNetCore.Mvc;
using SnapDetective.Images.Contracts;
using SnapDetective.Images.Contracts.Events;
using SnapDetective.Images.Interfaces;

namespace SnapDetective.Images.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageSetsController(IImageSetService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await service.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateImageSetRequest request)
    {
        var result = await service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPost("{id}/images")]
    public async Task<IActionResult> AddImage(int id, IFormFile file, [FromForm] List<string> answers)
    {
        var request = new AddImageRequest(
            FileName: file.FileName,
            ContentType: file.ContentType,
            FileStream: file.OpenReadStream(),
            Answers: answers);

        await service.AddImageAsync(id, request);
        return NoContent();
    }
}