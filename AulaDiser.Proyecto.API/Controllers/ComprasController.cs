using AulaDiser.Poryecto.Clases.DTOs;
using AulaDiser.Proyecto.Logica.Compra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AulaDiser.Proyecto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly ICompraService _compraService;

        public ComprasController(ICompraService compraService)
        {
            _compraService = compraService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompraRequetsDto request)
        {
            try
            {
                var resultado = await _compraService.ProcesarCompraAsync(request);
                return Ok(resultado);
            }

            catch (Exception ex)
            {
                return BadRequest(new {error = ex.Message});
            }
        }
    }
}
