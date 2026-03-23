using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AulaDiser.Poryecto.Clases
{
    public class PasoRequestDto
    {
        [JsonPropertyName("claveProducto")]
        public string ClaveProducto { get; set; }

        [JsonPropertyName("seguimiento")]
        public List<AtributoProductoDTO> Seguimiento { get; set; }
    }
}
