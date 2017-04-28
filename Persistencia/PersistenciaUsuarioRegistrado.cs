using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using EntidadesCompartidas.ObjetosNegocio;
using EntidadesCompartidas.Excepciones;

namespace Persistencia
{
    public class PersistenciaUsuarioRegistrado
    {
           public static void Agregar(UsuarioRegistrado registrado)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdAgregarRegistrado = new SqlCommand("AgregarUsuarioRegistrado", conexion);
                cmdAgregarRegistrado.CommandType = CommandType.StoredProcedure;

                cmdAgregarRegistrado.Parameters.AddWithValue("@cedula", registrado.Cedula);
                cmdAgregarRegistrado.Parameters.AddWithValue("@nombreCompleto", registrado.NombreCompleto);
                cmdAgregarRegistrado.Parameters.AddWithValue("@nombreUsuario", registrado.NombreUsuario);
                cmdAgregarRegistrado.Parameters.AddWithValue("@contrasenia", registrado.Contrasenia);
                cmdAgregarRegistrado.Parameters.AddWithValue("@foto", registrado.Imagen);
                cmdAgregarRegistrado.Parameters.AddWithValue("@direccion", registrado.DireccionEnvio);
                cmdAgregarRegistrado.Parameters.AddWithValue("@numeroTarjeta", registrado.NumeroTarjeta);
                cmdAgregarRegistrado.Parameters.AddWithValue("@telefono", registrado.Telefono);
                cmdAgregarRegistrado.Parameters.AddWithValue("@eliminado", registrado.Eliminado);

                SqlParameter retorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                retorno.Direction = ParameterDirection.ReturnValue;

                cmdAgregarRegistrado.Parameters.Add(retorno);

                conexion.Open();

                int filasAfectadas = cmdAgregarRegistrado.ExecuteNonQuery();

                switch ((int)retorno.Value)
                {
                    case 1:
                        throw new ExcepcionPersistencia("Ya existe un usuario con la cédula " + registrado.Cedula);
                        break;
                    case 2:
                        throw new ExcepcionPersistencia("El nombre de usuario " + registrado.NombreUsuario + " ya está en uso");
                        break;
                    case 3:
                        throw new ExcepcionPersistencia("No se pudo agregar el usuario");
                        break;
                    case 4:
                        throw new ExcepcionPersistencia("No se pudo agregar el usuario cliente");
                        break;

                        if (filasAfectadas < 1)
                        {
                            throw new ExcepcionPersistencia("Se produjo un error al agregar el usuario cliente.");
                        }
                }
            }

            finally
            {
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public static UsuarioRegistrado Buscar(int cedula, bool busqueda)
        {
            SqlConnection conexion = null;
            SqlDataReader drRegistrado = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                if (busqueda)
                {

                    SqlCommand cmdBuscarRegistrado = new SqlCommand("BuscarUsuarioRegistrado", conexion);
                    cmdBuscarRegistrado.CommandType = CommandType.StoredProcedure;

                    cmdBuscarRegistrado.Parameters.AddWithValue("@cedula", cedula);

                    conexion.Open();

                    drRegistrado = cmdBuscarRegistrado.ExecuteReader();
                }
                else
                {
                    SqlCommand cmdBuscarRegistrado = new SqlCommand("BuscarUsuarioRegistradoEliminadoLogica", conexion);
                    cmdBuscarRegistrado.CommandType = CommandType.StoredProcedure;

                    cmdBuscarRegistrado.Parameters.AddWithValue("@cedula", cedula);

                    conexion.Open();

                    drRegistrado = cmdBuscarRegistrado.ExecuteReader();
                }

                UsuarioRegistrado registrado = null;

                if (drRegistrado.Read())
                {
                    registrado = new UsuarioRegistrado((int)drRegistrado["Cedula"], (string)drRegistrado["NombreCompleto"], (string)drRegistrado["NombreUsuario"], (string)drRegistrado["Contrasenia"], (string)drRegistrado["Imagen"], (string)drRegistrado["DireccionEnvio"], (long)drRegistrado["NumeroTarjeta"], (int)drRegistrado["Telefono"], (bool)drRegistrado["Eliminado"]);
                }

                return registrado;
            }

            finally
            {
                if (drRegistrado != null)
                {
                    drRegistrado.Close();
                }

                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public static void Modificar(UsuarioRegistrado registrado)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdModificarRegistrado = new SqlCommand("ModificarUsuarioRegistrado", conexion);
                cmdModificarRegistrado.CommandType = CommandType.StoredProcedure;

                cmdModificarRegistrado.Parameters.AddWithValue("@cedula", registrado.Cedula);
                cmdModificarRegistrado.Parameters.AddWithValue("@nombreCompleto", registrado.NombreCompleto);
                cmdModificarRegistrado.Parameters.AddWithValue("@nombreUsuario", registrado.NombreUsuario);
                cmdModificarRegistrado.Parameters.AddWithValue("@contrasenia", registrado.Contrasenia);
                cmdModificarRegistrado.Parameters.AddWithValue("@foto", registrado.Imagen);
                cmdModificarRegistrado.Parameters.AddWithValue("@direccion", registrado.DireccionEnvio);
                cmdModificarRegistrado.Parameters.AddWithValue("@numeroTarjeta", registrado.NumeroTarjeta);
                cmdModificarRegistrado.Parameters.AddWithValue("@telefono", registrado.Telefono);
                cmdModificarRegistrado.Parameters.AddWithValue("@eliminado", registrado.Eliminado);

                SqlParameter retorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                retorno.Direction = ParameterDirection.ReturnValue;

                cmdModificarRegistrado.Parameters.Add(retorno);

                conexion.Open();

                int filasAfectadas = cmdModificarRegistrado.ExecuteNonQuery();

                switch ((int)retorno.Value)
                {
                    case 1:
                        throw new ExcepcionPersistencia("No existe un usuario con la cédula " + registrado.Cedula);
                        break;
                    case 2:
                        throw new ExcepcionPersistencia("El nick " + registrado.NombreUsuario + " ya está en uso por otro usuario");
                        break;
                    case 3:
                        throw new ExcepcionPersistencia("No se pudo modificar el usuario cliente");
                        break;
                    case 4:
                        throw new ExcepcionPersistencia("No se pudo modificar el usuario");
                        break;

                        if (filasAfectadas < 1)
                        {
                            throw new ExcepcionPersistencia("Se produjo un error al modificar el usuario cliente.");
                        }
                }
            }

            finally
            {
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public static void Eliminar(UsuarioRegistrado registrado)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdEliminarRegistrado = new SqlCommand("EliminarUsuarioRegistrado", conexion);
                cmdEliminarRegistrado.CommandType = CommandType.StoredProcedure;

                cmdEliminarRegistrado.Parameters.AddWithValue("@cedula", registrado.Cedula);

                SqlParameter retorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                retorno.Direction = ParameterDirection.ReturnValue;

                cmdEliminarRegistrado.Parameters.Add(retorno);

                conexion.Open();

                int filasAfectadas = cmdEliminarRegistrado.ExecuteNonQuery();

                switch ((int)retorno.Value)
                {
                    case 1:
                        throw new ExcepcionPersistencia("No existe un usuario con la cédula " + registrado.Cedula);
                        break;
                    case 2:
                        throw new ExcepcionPersistencia("No se pudo eliminar el cliente");
                        break;
                    case 3:
                        throw new ExcepcionPersistencia("No se pudo eliminar el usuario");
                        break;
                    case 4:
                        throw new ExcepcionPersistencia("No se pudo eliminar las lineas de pedido del usuario.");
                        break;
                    case 5:
                        throw new ExcepcionPersistencia("No se pudo eliminar los pedidos pendientes del usuario");
                        break;
                    case 6:
                        throw new ExcepcionPersistencia("No se pudo eliminar el usuario");
                        break;
                }

                if (filasAfectadas < 1)
                {
                    throw new ExcepcionPersistencia("Se produjo un error al eliminar el usuario cliente");
                }

            }
            finally
            {
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }


        public static List<UsuarioRegistrado> Listar()
        {
            SqlConnection conexion = null;
            SqlDataReader drRegistrado = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdListarRegistrados = new SqlCommand("ListarRegistrados", conexion);
                cmdListarRegistrados.CommandType = CommandType.StoredProcedure;

                conexion.Open();

                drRegistrado = cmdListarRegistrados.ExecuteReader();

                List<UsuarioRegistrado> registrados = new List<UsuarioRegistrado>();
                UsuarioRegistrado registrado;

                while (drRegistrado.Read())
                {
                    registrado = new UsuarioRegistrado((int)drRegistrado["Cedula"], (string)drRegistrado["NombreCompleto"], (string)drRegistrado["NombreUsuario"], (string)drRegistrado["Contrasenia"], (string)drRegistrado["Imagen"], (string)drRegistrado["DireccionEnvio"], (long)drRegistrado["NumeroTarjeta"], (int)drRegistrado["Telefono"], (bool)drRegistrado["Eliminado"]);

                    registrados.Add(registrado);
                }

                return registrados;
            }
            finally
            {
                if (drRegistrado != null)
                {
                    drRegistrado.Close();
                }

                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

       
        
    }
    
}
