using AulaDiser.Poryecto.Clases.DTOs;
using AulaDiser.Poryecto.Clases.Modelos;
using AulaDiser.Proyecto.Datos.Auth;
using AulaDiser.Proyecto.Logica.Auth;
using AulaDiser.Proyecto.Logica.Seg;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AulaDiser.Proyecto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _authService = authService;
        }

        //Registrar un nuevo usuario
        /*[HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] UsuarioRegistroDTO dto)
        {
            var id = await _authService.RegistrarUsuario(dto);

            //Retorna el id del usuario creado
            return Ok(new { mensaje = "Usuario creado", id }); 
        }*/

        //Registrar un nuevo usuario
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] UsuarioRegistroDTO dto)
        {
            Usuario usuario = await _authService.RegistrarUsuario(dto);

            //Firmar la cookie aquí mismo
            var principal = _tokenService.CrearPrincipal(usuario);
            await HttpContext.SignInAsync("Cookies", principal);

            // El usuario ya no tiene que loguearse, ya entra con su sesión activa
            return Ok(new { message = "Usuario creado y sesión iniciada" });
        }

        //Login del usuario - Retorna un Json Web Token
        /*[HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            //Validar las credenciales ingresadas
            var usuario = await _authService.ValidaUsuario(dto);

            //Si no se pudo obtener el usuario por nombre y/o contraseñas incorrectas
            if (usuario == null)
            {
                return Unauthorized("Usuario y/o contraseñas incorrectas");
            }

            //Generar el token
            var token = _tokenService.CrearToken(usuario);
            return Ok(new { token });
        }*/

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            //Validar las credenciales ingresadas
            var usuario = await _authService.ValidaUsuario(dto);

            //Si no se pudo obtener el usuario por nombre y/o contraseñas incorrectas
            if (usuario == null)
            {
                return Unauthorized("Usuario y/o contraseñas incorrectas");
            }

            //Firmar la cookie de autenticación
            var principal = _tokenService.CrearPrincipal(usuario);

            // 2. Firmamos la cookie. .NET se encarga de cifrarla y enviarla al navegador.
            await HttpContext.SignInAsync(
                "Cookies",
                principal);

            return Ok(new { message = "Sesión iniciada correctamente", nombre = usuario.NombreCompleto });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // Esto le ordena al navegador borrar la cookie "AuthToken"
            await HttpContext.SignOutAsync("Cookies");

            return Ok(new { message = "Sesión cerrada correctamente" });
        }

        //Si el usuario refresca el navegador se llama a este método para verificar si ya estaba autenticado, ya que al pulsar F5 la memoria de Angular se borra
        [HttpGet("check-session")]
        [Authorize] // Si no hay cookie válida, .NET responde 401 Unauthorized automáticamente
        public IActionResult CheckSession()
        {
            // Si llegó aquí, es porque el usuario está autenticado
            // Puedes devolver info básica del usuario si quieres

            var nombreCompleto = User.Claims
            .FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Actor)?.Value;

            return Ok(new
            {
                authenticated = true,
                usuario = User.Identity.Name,
                nombreCompleto = nombreCompleto
            });
        }
    }
}
