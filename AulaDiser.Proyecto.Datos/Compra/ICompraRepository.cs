using System;
using System.Collections.Generic;
using System.Text;

namespace AulaDiser.Proyecto.Datos.Compra
{
    public interface ICompraRepository
    {
        Task<int> GuardarCompraAsync(Poryecto.Clases.Compra compra);
    }
}
