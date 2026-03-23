using System;
using System.Collections.Generic;
using System.Text;

using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;

namespace AulaDiser.Proyecto.Datos.Compra
{
    public class CompraRepository : ICompraRepository
    {
        private readonly string _connectionString;
        public CompraRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<int> GuardarCompraAsync(Poryecto.Clases.Compra compra)
        {
            using var db = new SqlConnection(_connectionString);

            var tablaDetalle = new DataTable();
            //Estas columnas deben coincidir con el user-defined table type detalleCompraType que definimos en SQL Server
            tablaDetalle.Columns.Add("idProducto", typeof(int));
            tablaDetalle.Columns.Add("cantidad", typeof(decimal));
            tablaDetalle.Columns.Add("precio", typeof(decimal));

            foreach(var item in compra.Items)
            {
                tablaDetalle.Rows.Add(
                    item.IdProducto,
                    item.Cantidad,
                    item.Precio
                );
            }

            var parametros = new DynamicParameters();
            //El primer argumento es el nombre del parámetro en el procedimiento almacenado
            parametros.Add("@idCliente", compra.IdCliente);
            parametros.Add("@productosComprados", tablaDetalle.AsTableValuedParameter("pv.detalleCompraType")); //pv.detalleCompraType es el nombre del user-defined table type que creamos en SQL

            //ExecuteScalarAsync es un método de Dapper (y también de ADO.NET) que se utiliza para ejecutar una consulta SQL y devolver un único valor de la primera columna de la primera fila del resultado
            //El procedimiento almacenado pv.detalleCompraType devuelve el id de la compra registrada, por eso es <int>
            return await db.ExecuteScalarAsync<int>(
                "pv.pa_AlmacenaCompra",
                parametros,
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
