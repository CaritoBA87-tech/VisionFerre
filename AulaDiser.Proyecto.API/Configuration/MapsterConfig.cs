using AulaDiser.Poryecto.Clases;
using AulaDiser.Poryecto.Clases.DTOs;
using Mapster;

namespace AulaDiser.Proyecto.API.Configuration
{
    public class MapsterConfig
    {
        public static void RegisterMappings()
        {
            //Cuando mapees de CompraRequestDto a Compra, hazlo así:
            TypeAdapterConfig<CompraRequetsDto, Compra>
                .NewConfig()
                //Pasa lo que hay en 'Productos' a la lista 'Items'
                .Map(dest => dest.Items, src => src.Productos);

            //Mapster mapea automáticamente IdCliente porque se llaman igual. Solo hay que configurar lo que es diferente.
        }
    }
}
