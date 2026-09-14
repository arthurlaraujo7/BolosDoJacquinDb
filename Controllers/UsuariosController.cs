using BolosDoJacquinDb.DTO;
using BolosDoJacquinDb.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BolosDoJacquinDb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuario _service;

    public UsuariosController(IUsuario service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.ObterTodosAsync();
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.ObterPorIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UsuariosDTO dto)
    {
        var created = await _service.CriarAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UsuariosDTO dto)
    {
        var success = await _service.AtualizarAsync(id, dto);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _service.DeletarAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}