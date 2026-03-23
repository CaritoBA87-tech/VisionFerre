using AulaDiser.Poryecto.Clases;
using AulaDiser.Poryecto.Clases.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AulaDiser.Proyecto.Datos.Auth
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Usuario> ObtenerUsuarioPorNombreAsync(string username)
        {
            using var db = new SqlConnection(_connectionString);

            //QueryFirstOrDefaultAsync es un método de Dapper
            //Notar que los nombres de las columnas que devuelve el procedimiento almacenado son las mismas que las propiedades de la clase Usuario
             

            var result = await db.QueryFirstOrDefaultAsync<Usuario>(
                "seg.pa_Usuario_ObtenerPorNombre",
                new { username = username },
                commandType: CommandType.StoredProcedure
                );

            return result;

        }

        /*public async Task<int> RegistrarUsuarioAsync(Usuario usuario)
        {
            using var conn = new SqlConnection(_connectionString);

            var parametros = new
            {
                username = usuario.Username,
                pass = usuario.Passwd,
                salt = usuario.Salt,
                nombreCompleto = usuario.NombreCompleto,
                idRol = usuario.IdRol
            };

            return await conn.ExecuteScalarAsync<int>(
                "seg.pa_Usuario_Alta",
                parametros,
                commandType: CommandType.StoredProcedure
                );
        }*/

        public async Task<Usuario> RegistrarUsuarioAsync(Usuario usuario)
        {
            using var conn = new SqlConnection(_connectionString);

            var parametros = new
            {
                username = usuario.Username,
                pass = usuario.Passwd,
                salt = usuario.Salt,
                nombreCompleto = usuario.NombreCompleto,
                idRol = usuario.IdRol
            };

            return await conn.QueryFirstOrDefaultAsync<Usuario>(
                "seg.pa_Usuario_Alta",
                parametros,
                commandType: CommandType.StoredProcedure
                );
        }
    }
}
