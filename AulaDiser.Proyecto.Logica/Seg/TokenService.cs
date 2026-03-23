using AulaDiser.Poryecto.Clases.Modelos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AulaDiser.Proyecto.Logica.Seg
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config; 
        }

        //Genera un JWT único para un usuario que ya ha sido autenticado
        //Entrega al usuario un token que le permitirá navegar por la API sin volver a pedirle su contraseña durante una hora

        public string CrearToken(Usuario usuario)
        {
            //Obtener la llave secreta desde el archivo de configuración appsettings.json y convertir en una llave matemática
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            //El token se sella con el algoritmo HmacSha256
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Los Claims son declaraciones sobre el usuario que se incrustan dentro del token
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim(ClaimTypes.Actor, usuario.NombreCompleto),
                new Claim(ClaimTypes.Role, usuario.DescripcionRol)
            };

            //Crear el token
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"], //Quien emite el token
                audience: _config["Jwt:Audience"], //Quien recibe el token
                claims: claims, //Información del usuario
                expires: DateTime.Now.AddHours(1), //El token tiene una vida útil de 1 hora. Después de ese tiempo, el usuario tendrá que loguearse de nuevo por seguridad
                signingCredentials: creds //Credenciales de la firma
                );

            //Finalmente, convierte todo el objeto complejo en una cadena de texto larga dividida por tres puntos
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal CrearPrincipal(Usuario usuario)
        {
            // 1. Creamos la lista de Claims (exactamente como los tenías)
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuario.Username),
            new Claim(ClaimTypes.Actor, usuario.NombreCompleto),
            new Claim(ClaimTypes.Role, usuario.DescripcionRol)
        };

            // 2. Creamos la Identidad vinculándola al esquema de Cookies
            var claimsIdentity = new ClaimsIdentity(
                claims,
                "Cookies");

            // 3. Devolvemos el "Principal" (el dueño de la identidad)
            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}
