using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace AulaDiser.Proyecto.Logica.Servicios
{
    using Amazon.BedrockRuntime;
    using Amazon.BedrockRuntime.Model;
    using AulaDiser.Poryecto.Clases;

    public class BedrockService : IAsistenteIA
    {
        private readonly IAmazonBedrockRuntime _client;

        public BedrockService(IAmazonBedrockRuntime client)
        {
            _client = client;
        }

        public async Task<string> ProcesarConsultaFerreteraAsync(string prompt)
        {
            // 1. Definimos el Prompt (Ajustado para Llama 3.2)
            /* itemDetectado = "Rondana Plana";
            string prompt = $@"System: Responde directamente como experto en ferretería. 
    User: El usuario ha subido una foto de una {itemDetectado}. Explícale brevemente para qué sirve y deséale un gran día de trabajo.";*/

            var requestBody = new
            {
                prompt = prompt,
                max_gen_len = 100,
                temperature = 0.1,
                top_p = 0.9
            };

            var request = new InvokeModelRequest
            {
                ModelId = "us.meta.llama3-2-1b-instruct-v1:0",
                ContentType = "application/json",
                Accept = "application/json",
                Body = new MemoryStream(JsonSerializer.SerializeToUtf8Bytes(requestBody))
            };

            try
            {
                var response = await _client.InvokeModelAsync(request);

                using var reader = new StreamReader(response.Body);
                string bodyRaw = await reader.ReadToEndAsync();

                Console.WriteLine($"Respuesta RAW: {bodyRaw}");

                var result = JsonSerializer.Deserialize<LlamaResponse>(bodyRaw);

                return result?.Generation.Trim() ?? "Lo siento, no pude procesar la respuesta del modelo.";
            }
            catch (Exception ex)
            {
                return $"Error al conectar con FerreBot: {ex.Message}";
            }
        }
    }
}
