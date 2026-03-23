using AulaDiser.Poryecto.Clases.DTOs;
using AulaDiser.Poryecto.Clases.Modelos;
using AulaDiser.Proyecto.Datos.Auth;
using AulaDiser.Proyecto.Logica.Servicios;
using System;
using System.Collections.Generic;
using System.Text;

namespace AulaDiser.Proyecto.Logica.Auth
{

    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _repo;

        public AuthService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        /*public async Task<int> RegistrarUsuario(UsuarioRegistroDTO dto)
        {
            //Cifra el password
            //Por referencia obtenemos el hash del password y el hash del salt (firma)
            SeguridadService.CrearPasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var nuevoUsuario = new Usuario
            {
                Username = dto.Username,
                Passwd = passwordHash,
                Salt = passwordSalt,
                NombreCompleto = dto.NombreCompleto,
                IdRol = dto.IdRol
            };

            return await _repo.RegistrarUsuarioAsync(nuevoUsuario);
        }*/

        public async Task<Usuario> RegistrarUsuario(UsuarioRegistroDTO dto)
        {
            //Cifra el password
            //Por referencia obtenemos el hash del password y el hash del salt (firma)
            SeguridadService.CrearPasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var nuevoUsuario = new Usuario
            {
                Username = dto.Username,
                Passwd = passwordHash,
                Salt = passwordSalt,
                NombreCompleto = dto.NombreCompleto,
                IdRol = dto.IdRol
            };

            return await _repo.RegistrarUsuarioAsync(nuevoUsuario);
        }

        public async Task<Usuario> ValidaUsuario(LoginRequestDto dto)
        {
            var user = await _repo.ObtenerUsuarioPorNombreAsync(dto.Usuario);

            //Si el usuario no existe
            if(user == null)
            {
                return null;
            }

            //Si el usuario si existe verifica si la contraseña ingresada coincide con la contraseña del usuario encontrado en la base de datos
            if(!SeguridadService.VerificarPasswordHash(dto.Contrasena, user.Passwd, user.Salt)) 
            {
                return null;
            }

            return user;
        }
    }
}
