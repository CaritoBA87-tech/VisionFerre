using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AulaDiser.Proyecto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PruebaSeguridadController : ControllerBase
    {
        [HttpGet("publico")]
        public IActionResult GetPublico()
        {
            return Ok(new { mensaje = "Este endpoint es público y accesible para todos. No necesitas token." });
        }

        [Authorize] //Este endpoint es privado. Requiere token.
        [HttpGet("privado")]
        public IActionResult GetPrivado()
        {
            //Extraer el usuario del token
            var nombre = User.Identity.Name ?? "desconocido";
            return Ok($"Hola {nombre}, este mensaje es seguro. Ya leímos el token.");
        }

        //Reconoce el rol por los claims que se definieron en TokenService.cs (ClaimTypes.Role)
        [Authorize(Roles = "Administrador")]
        [HttpGet("solo_admin")]
        public IActionResult GetSoloAdmin()
        {
            return Ok("Bienvenido administrador");
        }
    }
}
