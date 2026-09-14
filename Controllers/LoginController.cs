using BolosDoJacquinDb.DTO;
using BolosDoJacquinDb.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace BolosDoJacquinDb.Controllers
{
    /// <summary>
    /// Controller responsável pela autenticação de usuários via JWT (JSON Web Token).
    /// 
    /// COMO FUNCIONA O JWT?
    /// 1. O usuário envia e-mail e senha via POST /api/Login.
    /// 2. A API valida as credenciais no banco (e-mail e hash BCrypt).
    /// 3. Se válido, a API gera um Token JWT assinado com uma chave secreta.
    /// 4. O cliente usa esse token no cabeçalho "Authorization: Bearer {token}" 
    ///    em todas as requisições seguintes que exigem autenticação ([Authorize]).
    /// </summary>
    /// 
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuario _usuario;
        private readonly IConfiguration _configuration;

        public LoginController(IUsuario usuario, IConfiguration configuration)
        {
            _usuario = usuario;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var usuarioEncontrado = await _usuario.BuscarPorEmailESenhaAsync(dto.Email, dto.Senha);

            if (usuarioEncontrado == null)
            {
                return Unauthorized("E-mail ou senha inválidos!");
            }

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, usuarioEncontrado.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, usuarioEncontrado.Email),
            new Claim("nome", usuarioEncontrado.Nome),
            new Claim("tipoUsuarioId", usuarioEncontrado.TipoUsuarioId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var chaveSecreta = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
            );

            var credenciais = new SigningCredentials(chaveSecreta, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"] ?? "BolosDoJacquinDb",
                audience: _configuration["Jwt:Audience"] ?? "BolosDoJacquinDb",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: credenciais
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                Token = tokenString,
                Expiracao = token.ValidTo,
                Usuario = new
                {
                    usuarioEncontrado.Id,
                    usuarioEncontrado.Nome,
                    usuarioEncontrado.Email,
                    usuarioEncontrado.TipoUsuarioId
                }
            });
        }
    }
}