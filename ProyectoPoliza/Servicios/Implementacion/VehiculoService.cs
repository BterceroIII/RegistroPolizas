using Microsoft.Data.SqlClient;
using ProyectoPoliza.Models;
using ProyectoPoliza.Servicios.Contrato;
using System.Data;

namespace ProyectoPoliza.Servicios.Implementacion
{
    public class VehiculoService : IGenericService<Vehiculo>
    {
        private readonly string _cadenaSQL = "";

        public VehiculoService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("CadenaSQL");
        }

        public async Task<List<Vehiculo>> List()
        {
            List<Vehiculo> _lista = new List<Vehiculo>();
            try
            {
                using (var conexion = new SqlConnection(_cadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("Mostrar_Vehiculo", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            _lista.Add(new Vehiculo()
                            {
                                IdVehiculo = Convert.ToInt32(dr["idVehiculo"]),
                                refCliente = new Cliente()
                                {
                                    IdCliente = Convert.ToInt32(dr["idCliente"]),
                                    NombreCliente = dr["NombreCliente"].ToString()
                                },
                                Tipo = dr["Tipo"].ToString(),
                                NoPlaca = dr["NoPlaca"].ToString(),
                                Marca = dr["Marca"].ToString(),
                                Modelo = dr["Modelo"].ToString(),
                                NoMotor = dr["NoMotor"].ToString(),
                                NoChasis = dr["NoChasis"].ToString(),
                                Uso = dr["Uso"].ToString(),
                                Año = dr["Año"].ToString()
                                //Eliminado = Convert.ToInt32(dr["Eliminado"]),
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
                
            return _lista;
        }

        public async Task<bool> Save(Vehiculo model)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Insertar_Vehiculo", conexion);
                cmd.Parameters.AddWithValue("idCliente", model.refCliente.IdCliente);
                cmd.Parameters.AddWithValue("Tipo", model.Tipo);
                cmd.Parameters.AddWithValue("NoPlaca", model.NoPlaca);
                cmd.Parameters.AddWithValue("Marca", model.Marca);
                cmd.Parameters.AddWithValue("Modelo", model.Modelo);
                cmd.Parameters.AddWithValue("NoMotor", model.NoMotor);
                cmd.Parameters.AddWithValue("NoChasis", model.NoChasis);
                cmd.Parameters.AddWithValue("Uso", model.Uso);
                cmd.Parameters.AddWithValue("Año", model.Año);

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

        public async Task<bool> Edit(Vehiculo model)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                try
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("Modificar_Vehiculo", conexion);
                    cmd.Parameters.AddWithValue("idVehiculo", model.IdVehiculo);
                    cmd.Parameters.AddWithValue("idCliente", model.refCliente.IdCliente);
                    cmd.Parameters.AddWithValue("Tipo", model.Tipo);
                    cmd.Parameters.AddWithValue("NoPlaca", model.NoPlaca);
                    cmd.Parameters.AddWithValue("Marca", model.Marca);
                    cmd.Parameters.AddWithValue("Modelo", model.Modelo);
                    cmd.Parameters.AddWithValue("NoMotor", model.NoMotor);
                    cmd.Parameters.AddWithValue("NoChasis", model.NoChasis);
                    cmd.Parameters.AddWithValue("Uso", model.Uso);
                    cmd.Parameters.AddWithValue("Año", model.Año);

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
                catch (Exception ex)
                {

                    throw ex;
                }
                

            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Eliminar_Vehiculo", conexion);
                cmd.Parameters.AddWithValue("idVehiculo", id);
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



    }
}


