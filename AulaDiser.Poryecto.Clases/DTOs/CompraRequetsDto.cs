using System;
using System.Collections.Generic;
using System.Text;

namespace AulaDiser.Poryecto.Clases.DTOs
{
    //Lo que el usuario envía desde el front
    public class CompraRequetsDto
    {
        public int IdCliente { get; set; }
        public List<ItemCompraDto> Productos { get; set; } = new List<ItemCompraDto>();
    }
}
