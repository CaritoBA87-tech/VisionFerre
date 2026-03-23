using System;
using System.Collections.Generic;
using System.Text;

namespace AulaDiser.Poryecto.Clases
{
    public class RespuestaPaso
    {
        public string Pregunta { get; set; }
        public string Atributo { get; set; }
        public List<string> Opciones { get; set; } = new List<string>();
    }
}
