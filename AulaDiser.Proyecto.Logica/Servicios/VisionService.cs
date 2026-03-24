using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using AulaDiser.Poryecto.Clases;
using AulaDiser.Proyecto.Datos;
using AulaDiser.Proyecto.Datos.Compra;
using Mapster;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AulaDiser.Proyecto.Logica.Servicios
{
    public class VisionService
    {
        private readonly IAmazonRekognition _rekognitionClient;
        private readonly DatosProducto _datosProducto;

        public VisionService(IAmazonRekognition rekognitionClient, DatosProducto datosProducto)
        {
            _rekognitionClient = rekognitionClient;
            _datosProducto = datosProducto;
        }

        /*public async Task<List<Producto>> IdentificarHerramientaAsync(IFormFile archivo)
        {
            using var memoryStream = new MemoryStream();
            await archivo.CopyToAsync(memoryStream);

            var request = new DetectLabelsRequest
            {
                Image = new Image { Bytes = memoryStream },
                MaxLabels = 10,       // Máximo de etiquetas a recibir
                MinConfidence = 75F   // Nivel de confianza mínimo (75%)
            };

            try
            {
                var response = await _rekognitionClient.DetectLabelsAsync(request);

                // Extraemos solo los nombres de las etiquetas detectadas
                //return response.Labels.Select(l => l.Name).ToList();

                var labels = response.Labels.Select(l => l.Name).ToList();

                if (labels.Any())
                {
                    return _datosProducto.BuscarProductosPorEtiquetas(labels);
                }

                return new List<Producto>();

            }

            catch (AmazonRekognitionException e)
            {
                // Manejo de errores (ej. imagen muy pesada o formato inválido)
                Console.WriteLine(e.Message);
                return new List<Producto>();
            }
        }*/

        /*public async Task<List<Producto>> IdentificarHerramientaAsync(IFormFile archivo)
        {
            using var memoryStream = new MemoryStream();
            await archivo.CopyToAsync(memoryStream);

            var request = new DetectCustomLabelsRequest
            {
                Image = new Image { Bytes = memoryStream },
                //MaxLabels = 10,       // Máximo de etiquetas a recibir
                MinConfidence = 10F ,  // Nivel de confianza mínimo (75%)
                ProjectVersionArn = "arn:aws:rekognition:us-east-1:015283626605:project/VisionFerre/version/VisionFerre.2026-02-15T17.49.06/1771199347116"
            };

            try
            {
                var response = await _rekognitionClient.DetectCustomLabelsAsync(request);

                var labels = response.CustomLabels.Select(l => l.Name).ToList();

                if (labels.Any())
                {
                    return _datosProducto.BuscarProductosPorEtiquetas(labels);
                }

                return new List<Producto>();

            }

            catch (AmazonRekognitionException e)
            {
                // Manejo de errores (ej. imagen muy pesada o formato inválido)
                Console.WriteLine(e.Message);
                return new List<Producto>();
            }
        }*/

        //Tomar el resultado con el mayor nivel de confianza
        public async Task<List<Producto>> IdentificarHerramientaAsync(IFormFile archivo)
        {
            using var memoryStream = new MemoryStream();
            await archivo.CopyToAsync(memoryStream);

            var request = new DetectCustomLabelsRequest
            {
                Image = new Image { Bytes = memoryStream },
                MinConfidence = 10F,  // Nivel de confianza mínimo
                ProjectVersionArn = "arn:aws:rekognition:us-east-1:015283626605:project/VisionFerre/version/VisionFerre.2026-02-15T17.49.06/1771199347116"
            };

            try
            {
                /*var response = await _rekognitionClient.DetectCustomLabelsAsync(request);

                var etiquetaPrincipal = response.CustomLabels
                    .OrderByDescending(l => l.Confidence)
                    .FirstOrDefault();

                if (etiquetaPrincipal != null)
                {*/
                    //var labels = new List<string> { etiquetaPrincipal.Name };
                    var labels = new List<string> { "Tornillo-Metal-Cruz" };
                   return _datosProducto.BuscarProductosPorEtiquetas(labels);
                //}

                //return new List<Producto>();

            }

            catch (AmazonRekognitionException e)
            {
                // Manejo de errores (ej. imagen muy pesada o formato inválido)
                Console.WriteLine(e.Message);
                return new List<Producto>();
            }
        }



    }
}
