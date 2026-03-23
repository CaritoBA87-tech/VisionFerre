using System;
using System.Collections.Generic;
using System.Text;

namespace AulaDiser.Poryecto.Clases.DTOs
{
    public class LoginRequestDto
    {
        public string Usuario { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
    }
}
