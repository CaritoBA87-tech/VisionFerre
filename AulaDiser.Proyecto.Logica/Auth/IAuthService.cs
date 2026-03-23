using AulaDiser.Poryecto.Clases.DTOs;
using AulaDiser.Poryecto.Clases.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AulaDiser.Proyecto.Logica.Auth
{
    public interface IAuthService
    {
        //Task<int> RegistrarUsuario(UsuarioRegistroDTO dto);
        Task<Usuario> RegistrarUsuario(UsuarioRegistroDTO dto);
        Task<Usuario> ValidaUsuario(LoginRequestDto dto);
    }
}
