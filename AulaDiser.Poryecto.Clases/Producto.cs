using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace AulaDiser.Poryecto.Clases
{
    public class Producto
    {
        private int idProducto;

        public int IdProducto
        {
            get { return idProducto;  }
            set { idProducto = value; }
        }

        //Estoy es equivalente a la forma de declarar anterior
        public string SKU { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Estatus { get; set; }
        public string Categoria { get; set; }
        public string Marca { get; set; }
        public string jsonImagenes { get; set; }
        public string Clave { get; set; }

        public List<string> Imagenes { get; set; } = new List<string>();
        public string AtributosJSON { get; set; }

        public List<AtributoProducto> Atributos
        {
            get
            {
                if (string.IsNullOrEmpty(AtributosJSON)) return new List<AtributoProducto>();
                return JsonSerializer.Deserialize<List<AtributoProducto>>(AtributosJSON);
            }
        }
    }
}
