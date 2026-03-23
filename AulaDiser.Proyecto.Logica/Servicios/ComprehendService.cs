using Amazon.Comprehend;
using Amazon.Comprehend.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AulaDiser.Proyecto.Logica.Servicios
{
    public class ComprehendService
    {
        private readonly IConfiguration _configuration;

        public ComprehendService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /*public async Task<List<object>> DetectarEntidades(string text)
        {
            // 1. Extraer las llaves del JSON
            var accessKey = _configuration["AWS:AccessKey"];
            var secretKey = _configuration["AWS:SecretKey"];
            var region = Amazon.RegionEndpoint.GetBySystemName(_configuration["AWS:Region"]);

            // 2. Configurar las credenciales
            var credentials = new Amazon.Runtime.BasicAWSCredentials(accessKey, secretKey);

            // 3. Pasar las credenciales al cliente
            var comprehendClient = new AmazonComprehendClient(credentials, region);

            var detectEntitiesRequest = new DetectEntitiesRequest()
            {
                Text = text,
                LanguageCode = "en", // Cambia a "es" si pruebas en español
            };

            var response = await comprehendClient.DetectEntitiesAsync(detectEntitiesRequest);

            // Convertimos la respuesta de AWS a una lista simple para Angular
            return response.Entities.Select(e => new {
                Texto = e.Text,
                Tipo = e.Type,
                Confianza = e.Score
            }).Cast<object>().ToList();
        }*/
    }
}
