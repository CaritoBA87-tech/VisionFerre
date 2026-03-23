using System;
using System.Collections.Generic;
using System.Text;

namespace AulaDiser.Poryecto.Clases
{
    //Estos son todos los datos necesarios para registrar una compra en la base de datos
    public class Compra
    {
        public int IdCompra { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public List<DetalleCompra> Items { get; set; } = new List<DetalleCompra>();
    }
}
