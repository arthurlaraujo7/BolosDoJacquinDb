using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;

namespace BolosDoJacquinDb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly Cloudinary _cloudinary;

    public UploadController(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }

    [HttpPost("imagem")]
    public async Task<IActionResult> UploadImagem(IFormFile arquivo)
    {
        try
        {
            if (arquivo == null || arquivo.Length == 0)
                return BadRequest("Selecione um arquivo de imagem válido.");

            using var stream = arquivo.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(arquivo.FileName, stream),
                Folder = "bolos_jacquin"
            };

            var result = await _cloudinary.UploadAsync(uploadParams);

            if (result.Error != null)
                return BadRequest(result.Error.Message);

            return Ok(new
            {
                Url = result.SecureUrl.ToString(),
                PublicId = result.PublicId
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro durante o upload: {ex.Message}");
        }
    }
}