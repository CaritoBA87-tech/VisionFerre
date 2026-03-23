using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace AulaDiser.Proyecto.Logica.Servicios
{
    public class SeguridadService
    {
        //Cuando el usuario se registra, genera el hash y el salt para su password plano
        public static void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //Cada que se instancia la clase HMACSHA512 se genera una firma digital (salt) aleatoria
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key; //Se genera la firma
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); //Convertimos el pass plano en bytes y lo ciframos con la llave
            }
        }

        //Verifica que el password plano coincida con el hash y el salt almacenados
        public static bool VerificarPasswordHash(string password, byte[] hashAlmacenado, byte[] saltAlmacenado)
        {
            //Usamos el Salt almacenado para iniciar el algoritmo
            using (var hmac = new HMACSHA512(saltAlmacenado))
            {
                //Calculamos el hash del password que escribió el usuario
                var hashCalculado = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                //Comparar byte por byte el hash calculado con el de la base de datos
                for (int i=0; i< hashCalculado.Length; i++)
                {
                    if (hashCalculado[i] != hashAlmacenado[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
