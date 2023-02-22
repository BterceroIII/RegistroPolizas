using Microsoft.EntityFrameworkCore;
using ProyectoPoliza.Models;
using ProyectoPoliza.Servicios.Contrato;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;

namespace ProyectoPoliza.Servicios.Implementacion
{
    public class EmpleadoService : IGenericService<Empleado>
    {
        private readonly GestionSegurosSaContext _dbcontext;
        private readonly string _cadenaSQL = "";

        public EmpleadoService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("cadenaSQL");
        }
        public async Task<List<Empleado>> List()
        {
            List<Empleado> _lista = new List<Empleado>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Mostrar_Empleados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        _lista.Add(new Empleado
                        {
                            IdEmpleado = Convert.ToInt32(dr["idEmpleado"]),
                            Nombre = dr["Nombre"].ToString(),
                            Cedula = dr["Cedula"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            Cargo = dr["Cargo"].ToString(),
                            Eliminado = Convert.ToInt32(dr["Eliminado"]),
                        });
                    }

                }

            }

            return _lista;
        }
        public async Task<bool> Save(Empleado model)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Insertar_Empleado", conexion);
                cmd.Parameters.AddWithValue("Nombre", model.Nombre);
                cmd.Parameters.AddWithValue("Cedula", model.Cedula);
                cmd.Parameters.AddWithValue("Telefono", model.Telefono);
                cmd.Parameters.AddWithValue("Cargo", model.Cargo);

                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public async Task<bool> Edit(Empleado model)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Modificar_Empleados", conexion);
                cmd.Parameters.AddWithValue("idEmpleado", model.IdEmpleado);
                cmd.Parameters.AddWithValue("Nombre", model.Nombre);
                cmd.Parameters.AddWithValue("Cedula", model.Cedula);
                cmd.Parameters.AddWithValue("Telefono", model.Telefono);
                cmd.Parameters.AddWithValue("Cargo", model.Cargo);

                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_EliminarEmpleado", conexion);
                cmd.Parameters.AddWithValue("idEmpleado", id);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }



        }

        //public async Task<IQueryable<Empleado>> ObtenerTodos()
        //{
        //    IQueryable<Empleado> queryContactoSQL = _dbcontext.Empleados;
        //    return queryContactoSQL;
        //}
    }
}
