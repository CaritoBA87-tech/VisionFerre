using System;
using System.Collections.Generic;
using System.Text;

namespace AulaDiser.Poryecto.Clases.Modelos
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public byte[] Passwd { get; set; }
        public byte[] Salt { get; set; }
        public string NombreCompleto { get; set; }
        public int IdRol { get; set; }
        public int Activo { get; set; }
        public string DescripcionRol { get; set; }
    }
}
