using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AulaDiser.Poryecto.Clases
{
    public class AtributoProductoDTO
    {
        [JsonPropertyName("atributo")]
        public string Atributo { get; set; }

        [JsonPropertyName("valor")]
        public string? Valor { get; set; }
        [JsonPropertyName("pendiente")]
        public bool Pendiente { get; set; }
    }
}
