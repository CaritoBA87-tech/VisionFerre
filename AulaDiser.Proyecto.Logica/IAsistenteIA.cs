using System;
using System.Collections.Generic;
using System.Text;

namespace AulaDiser.Proyecto.Logica
{
    public interface IAsistenteIA
    {
        Task<string> ProcesarConsultaFerreteraAsync(string prompt);
    }
}
