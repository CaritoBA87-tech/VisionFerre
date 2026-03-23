using AulaDiser.Poryecto.Clases.DTOs;
using AulaDiser.Proyecto.Datos.Compra;
using AulaDiser.Proyecto.Logica.Compra;
using System;
using System.Collections.Generic;
using System.Text;

using Mapster;

namespace AulaDiser.Proyecto.Logica
{
    public class CompraService : ICompraService
    {
        private readonly ICompraRepository _compraRepository;
        public CompraService(ICompraRepository compraRepository)
        {
            _compraRepository = compraRepository;
        }

        public async Task<CompraResponseDto> ProcesarCompraAsync(CompraRequetsDto dto)
        {
            //Mapear CompraRequetsDto a entidad Compra de acuerdo a como se estableció en MapsterConfig.RegisterMappings
            var entidadCompra = dto.Adapt<Poryecto.Clases.Compra>();

            entidadCompra.Fecha = DateTime.Now;

            if(entidadCompra.Items == null || entidadCompra.Items.Count == 0)
            {
                throw new ArgumentException("La compra debe contener al menos un item.");
            }

            int idGenerado = await _compraRepository.GuardarCompraAsync(entidadCompra);

            return new CompraResponseDto
            {
                IdCompra = idGenerado
            };
        }
    }
}
