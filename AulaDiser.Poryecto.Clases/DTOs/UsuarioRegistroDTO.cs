using System;
using System.Collections.Generic;
using System.Text;

namespace AulaDiser.Poryecto.Clases.DTOs
{
    public class UsuarioRegistroDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string NombreCompleto { get; set; }
        public int IdRol { get; set; }
    }
}
