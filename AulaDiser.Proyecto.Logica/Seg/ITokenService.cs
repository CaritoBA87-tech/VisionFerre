using AulaDiser.Poryecto.Clases.Modelos;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace AulaDiser.Proyecto.Logica.Seg
{
    public interface ITokenService
    {
        string CrearToken(Usuario usuario);
        ClaimsPrincipal CrearPrincipal(Usuario usuario); // Nuevo método para Cookies
    }
}
