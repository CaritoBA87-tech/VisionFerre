using AulaDiser.Poryecto.Clases.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AulaDiser.Proyecto.Logica.Compra
{
    public interface ICompraService
    {
        Task<CompraResponseDto> ProcesarCompraAsync(CompraRequetsDto dto);
    }
}
