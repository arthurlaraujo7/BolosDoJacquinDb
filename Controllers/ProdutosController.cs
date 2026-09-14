using BolosDoJacquinDb.DTO;
using BolosDoJacquinDb.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BolosDoJacquinDb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutos _service;

    public ProdutosController(IProdutos service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.ObterTodosAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.ObterPorIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProdutosDTO dto)
    {
        var created = await _service.CriarAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProdutosDTO dto)
    {
        var success = await _service.AtualizarAsync(id, dto);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeletarAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}