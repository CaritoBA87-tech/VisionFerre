using AulaDiser.Poryecto.Clases.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AulaDiser.Proyecto.Datos.Auth
{
    public interface IUsuarioRepository
    {
        //Task<int> RegistrarUsuarioAsync(Usuario usuario);
        Task<Usuario> RegistrarUsuarioAsync(Usuario usuario);
        Task<Usuario> ObtenerUsuarioPorNombreAsync(string username);
    }
}
