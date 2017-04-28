using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using EntidadesCompartidas.ObjetosNegocio;
using EntidadesCompartidas.Excepciones;

namespace Persistencia
{
    public class PersistenciaAdministrador
    {
        public static void Agregar(Administrador administrador)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdAgregarAdministrador = new SqlCommand("AgregarUsuarioAdmin", conexion);
                cmdAgregarAdministrador.CommandType = CommandType.StoredProcedure;

                cmdAgregarAdministrador.Parameters.AddWithValue("@cedula", administrador.Cedula);
                cmdAgregarAdministrador.Parameters.AddWithValue("@nombreCompleto", administrador.NombreCompleto);
                cmdAgregarAdministrador.Parameters.AddWithValue("@nombreUsuario", administrador.NombreUsuario);
                cmdAgregarAdministrador.Parameters.AddWithValue("@contrasenia", administrador.Contrasenia);
                cmdAgregarAdministrador.Parameters.AddWithValue("@foto", administrador.Imagen);
                cmdAgregarAdministrador.Parameters.AddWithValue("@cargo", administrador.Cargo);

                SqlParameter retorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                retorno.Direction = ParameterDirection.ReturnValue;

                cmdAgregarAdministrador.Parameters.Add(retorno);

                conexion.Open();

                int filasAfectadas = cmdAgregarAdministrador.ExecuteNonQuery();

                switch ((int)retorno.Value)
                {
                    case 1:
                        throw new ExcepcionPersistencia("Ya existe un usuario con la cédula " + administrador.Cedula);
                        break;
                    case 2:
                        throw new ExcepcionPersistencia("El nombre de usuario " + administrador.NombreUsuario + " ya está en uso");
                        break;
                    case 3:
                        throw new ExcepcionPersistencia("No se pudo agregar el usuario");
                        break;
                    case 4:
                        throw new ExcepcionPersistencia("No se pudo agregar el administrador");
                        break;

                        if (filasAfectadas < 1)
                        {
                            throw new ExcepcionPersistencia("Se produjo un error al agregar el usuario administrador.");
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

        public static Administrador Buscar(int cedula)
        {
            SqlConnection conexion = null;
            SqlDataReader drAdministrador = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdBuscarAdministrador = new SqlCommand("BuscarUsuarioAdmin", conexion);
                cmdBuscarAdministrador.CommandType = CommandType.StoredProcedure;

                cmdBuscarAdministrador.Parameters.AddWithValue("@cedula", cedula);

                conexion.Open();

                drAdministrador = cmdBuscarAdministrador.ExecuteReader();

                Administrador administrador = null;

                if (drAdministrador.Read())
                {
                    administrador = new Administrador((int)drAdministrador["Cedula"], (string)drAdministrador["NombreCompleto"], (string)drAdministrador["NombreUsuario"], (string)drAdministrador["Contrasenia"], (string)drAdministrador["Imagen"], (string)drAdministrador["Cargo"]);
                }

                return administrador;
            }

            finally
            {
                if (drAdministrador != null)
                {
                    drAdministrador.Close();
                }

                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public static void Modificar(Administrador administrador)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdModificarAdministrador = new SqlCommand("ModificarUsuarioAdmin", conexion);
                cmdModificarAdministrador.CommandType = CommandType.StoredProcedure;

                cmdModificarAdministrador.Parameters.AddWithValue("@cedula", administrador.Cedula);
                cmdModificarAdministrador.Parameters.AddWithValue("@nombreCompleto", administrador.NombreCompleto);
                cmdModificarAdministrador.Parameters.AddWithValue("@nombreUsuario", administrador.NombreUsuario);
                cmdModificarAdministrador.Parameters.AddWithValue("@contrasenia", administrador.Contrasenia);
                cmdModificarAdministrador.Parameters.AddWithValue("@foto", administrador.Imagen);
                cmdModificarAdministrador.Parameters.AddWithValue("@cargo", administrador.Cargo);

                SqlParameter retorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                retorno.Direction = ParameterDirection.ReturnValue;

                cmdModificarAdministrador.Parameters.Add(retorno);

                conexion.Open();

                int filasAfectadas = cmdModificarAdministrador.ExecuteNonQuery();

                switch ((int)retorno.Value)
                {
                    case 1:
                        throw new ExcepcionPersistencia("No existe un administrador con la cédula " + administrador.Cedula);
                        break;
                    case 2:
                        throw new ExcepcionPersistencia("El nombre de usuario " + administrador.NombreUsuario + " ya está en uso");
                        break;
                    case 3:
                        throw new ExcepcionPersistencia("No se pudo modificar el administrador");
                        break;
                    case 4:
                        throw new ExcepcionPersistencia("No se pudo modificar el usuario");
                        break;

                  }

                  if (filasAfectadas < 1)
                  {
                      throw new ExcepcionPersistencia("Se produjo un error al modificar el usuario administrador");
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

        public static void Eliminar(Administrador administrador)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdEliminarAdministrador = new SqlCommand("EliminarUsuarioAdmin", conexion);
                cmdEliminarAdministrador.CommandType = CommandType.StoredProcedure;

                cmdEliminarAdministrador.Parameters.AddWithValue("@cedula", administrador.Cedula);

                 SqlParameter retorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                retorno.Direction = ParameterDirection.ReturnValue;

                cmdEliminarAdministrador.Parameters.Add(retorno);

                conexion.Open();

                int filasAfectadas = cmdEliminarAdministrador.ExecuteNonQuery();

                switch ((int)retorno.Value)
                {
                    case 1:
                        throw new ExcepcionPersistencia("No existe un administrador con la cédula " + administrador.Cedula);
                        break;
                    case 2:
                        throw new ExcepcionPersistencia("No se pudo eliminar el administrador");
                        break;
                    case 3:
                        throw new ExcepcionPersistencia("No se pudo eliminar el usuario");
                        break;
                }

                if (filasAfectadas < 1)
                {
                    throw new ExcepcionPersistencia("Se produjo un error al eliminar el usuario administrador");
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

        public static List<Administrador> Listar()
        {
            SqlConnection conexion = null;
            SqlDataReader drAdministrador = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdListarAdministradores = new SqlCommand("ListarAdministradores", conexion);
                cmdListarAdministradores.CommandType = CommandType.StoredProcedure;

                conexion.Open();

                drAdministrador = cmdListarAdministradores.ExecuteReader();

                List<Administrador> administradores = new List<Administrador>();
                Administrador administrador;

                while (drAdministrador.Read())
                {
                    administrador = new Administrador((int)drAdministrador["Cedula"], (string)drAdministrador["NombreCompleto"], (string)drAdministrador["NombreUsuario"], (string)drAdministrador["Contrasenia"], (string)drAdministrador["Imagen"], (string)drAdministrador["Cargo"]);

                    administradores.Add(administrador);
                }

                return administradores;
            }
            finally
            {
                if (drAdministrador != null)
                {
                    drAdministrador.Close();
                }

                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

    }
}
