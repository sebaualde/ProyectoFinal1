using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using EntidadesCompartidas.ObjetosNegocio;
using EntidadesCompartidas.Excepciones;

namespace Persistencia
{
    public class PersistenciaCategoriaArticulo
    {
        public static void Agregar(CategoriaArticulo categoria)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdAgregarCategoria = new SqlCommand ("AgregarCategoria", conexion);
                cmdAgregarCategoria.CommandType = CommandType.StoredProcedure;

                cmdAgregarCategoria.Parameters.AddWithValue("@nombre", categoria.Nombre);
                cmdAgregarCategoria.Parameters.AddWithValue("@descripcion", categoria.Descripcion);
                cmdAgregarCategoria.Parameters.AddWithValue("@eliminado", categoria.Eliminado);

                SqlParameter retorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                retorno.Direction = ParameterDirection.ReturnValue;
                cmdAgregarCategoria.Parameters.Add(retorno);

                conexion.Open();

                int filasAfectadas = cmdAgregarCategoria.ExecuteNonQuery();

                if ((int)retorno.Value == 1)
                {
                    throw new ExcepcionPersistencia("Ya existe una categoria con el nombre: " + categoria.Nombre);
                }

                if (filasAfectadas < 1)
                {
                    throw new ExcepcionPersistencia("Se Produjo un error al Agregar la Categoria: " + categoria.Nombre);
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

        public static void Eliminar(string nombre)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdEliminarCategoria = new SqlCommand("EliminarCategoria", conexion);
                cmdEliminarCategoria.CommandType = CommandType.StoredProcedure;

                cmdEliminarCategoria.Parameters.AddWithValue("@nombre", nombre);

                conexion.Open();

                int filasAfectadas = cmdEliminarCategoria.ExecuteNonQuery();

                if (filasAfectadas < 1)
                {
                    throw new ExcepcionPersistencia("Error al Eliminar la categoria: " + nombre);
                }

                List<Articulo> articulos = PersistenciaArticulo.ListarXCategoriaDesordenado(nombre);

                foreach (Articulo a in articulos)
                {
                    PersistenciaArticulo.Eliminar(a.CodigoBarras);
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

        public static void Modificar(CategoriaArticulo categoria)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdModificarCategoria = new SqlCommand("ModificarCategoria", conexion);
                cmdModificarCategoria.CommandType = CommandType.StoredProcedure;

                cmdModificarCategoria.Parameters.AddWithValue("@nombre", categoria.Nombre);
                cmdModificarCategoria.Parameters.AddWithValue("@descripcion", categoria.Descripcion);
                cmdModificarCategoria.Parameters.AddWithValue("@eliminado", categoria.Eliminado);

                SqlParameter retorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                retorno.Direction = ParameterDirection.ReturnValue;
                cmdModificarCategoria.Parameters.Add(retorno);

                conexion.Open();

                int filasAfectadas = cmdModificarCategoria.ExecuteNonQuery();

                if ((int)retorno.Value == 1)
                {
                    throw new ExcepcionPersistencia("No existe una categoria con el nombre: " + categoria.Nombre);
                }

                if (filasAfectadas < 1)
                {
                    throw new ExcepcionPersistencia("Ocurrio un Error al modificar la categoria: " + categoria.Nombre);
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

        public static CategoriaArticulo Buscar(string nombre, bool buscar)
        {
            SqlConnection conexion = null;
            SqlDataReader drCategoria = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                if (buscar)
                {
                    SqlCommand cmdBuscarCategoria = new SqlCommand("BuscarCategoriaSinDiscriminarEliminado", conexion);
                    cmdBuscarCategoria.CommandType = CommandType.StoredProcedure;

                    cmdBuscarCategoria.Parameters.AddWithValue("@nombre", nombre);

                    conexion.Open();

                    drCategoria = cmdBuscarCategoria.ExecuteReader();
                }
                else
                {
                    SqlCommand cmdBuscarCategoria = new SqlCommand("BuscarCategoriasNoEliminada", conexion);
                    cmdBuscarCategoria.CommandType = CommandType.StoredProcedure;

                    cmdBuscarCategoria.Parameters.AddWithValue("@nombre", nombre);

                    conexion.Open();

                    drCategoria = cmdBuscarCategoria.ExecuteReader();
                }             

                CategoriaArticulo categoria = null;

                if (drCategoria.Read())
                {
                    categoria = new CategoriaArticulo((string)drCategoria["Nombre"], (string)drCategoria["Descripcion"], (bool)drCategoria["Eliminado"]);
                }

                return categoria;

            }
            finally
            {
                if (drCategoria != null)
                {
                    drCategoria.Close();
                }

                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public static List<CategoriaArticulo> Listar()
        {
            SqlConnection conexion = null;
            SqlDataReader drCategoria = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdListarCategorias = new SqlCommand("ListarCategorias", conexion);
                cmdListarCategorias.CommandType = CommandType.StoredProcedure;

                conexion.Open();

                drCategoria = cmdListarCategorias.ExecuteReader();

                List<CategoriaArticulo> Categorias = new List<CategoriaArticulo>();
                CategoriaArticulo categoria;

                while (drCategoria.Read())
                {
                    categoria = new CategoriaArticulo((string)drCategoria["Nombre"], (string)drCategoria["Descripcion"], (bool)drCategoria["Eliminado"]);

                    Categorias.Add(categoria);
                }

                return Categorias;
            }
            finally
            {
                if (drCategoria != null)
                {
                    drCategoria.Close();
                }

                if (conexion != null)
                {
                    conexion.Close();
                }
            }

        }

    }
}
