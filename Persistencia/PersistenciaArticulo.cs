using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using EntidadesCompartidas.ObjetosNegocio;
using EntidadesCompartidas.Excepciones;

namespace Persistencia
{
    public class PersistenciaArticulo
    {
        public static void Agregar(Articulo articulo)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdAgregarArticulo = new SqlCommand("AgregarArticulo", conexion);
                cmdAgregarArticulo.CommandType = CommandType.StoredProcedure;

                cmdAgregarArticulo.Parameters.AddWithValue("@codigoBarras", articulo.CodigoBarras);
                cmdAgregarArticulo.Parameters.AddWithValue("@nombre", articulo.Nombre);
                cmdAgregarArticulo.Parameters.AddWithValue("@precio", articulo.Precio);
                cmdAgregarArticulo.Parameters.AddWithValue("@stock", articulo.Stock);
                cmdAgregarArticulo.Parameters.AddWithValue("@descripcion", articulo.Descripcion);
                cmdAgregarArticulo.Parameters.AddWithValue("@imagen", articulo.Imagen);
                cmdAgregarArticulo.Parameters.AddWithValue("@categoria", articulo.Categoria.Nombre);
                cmdAgregarArticulo.Parameters.AddWithValue("@eliminado", articulo.Eliminado);

                SqlParameter retorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                retorno.Direction = ParameterDirection.ReturnValue;
                cmdAgregarArticulo.Parameters.Add(retorno);

                conexion.Open();

                int filasAfectadas = cmdAgregarArticulo.ExecuteNonQuery();

                if ((int)retorno.Value == 1)
                {
                    throw new ExcepcionPersistencia("Ya existe el articulo con el codigo de barras: " + articulo.CodigoBarras);
                }

                if (filasAfectadas < 1)
                {
                    throw new ExcepcionPersistencia("Ocurrio un error al agregar el articulo con el codigo de barras: " + articulo.CodigoBarras);
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

        public static void Eliminar(long codigoBarras)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdEliminarArticulo = new SqlCommand("EliminarArticulo", conexion);
                cmdEliminarArticulo.CommandType = CommandType.StoredProcedure;

                SqlParameter retorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                retorno.Direction = ParameterDirection.ReturnValue;
                cmdEliminarArticulo.Parameters.Add(retorno);

                cmdEliminarArticulo.Parameters.AddWithValue("@codigoBarras", codigoBarras);

                conexion.Open();

                int filasAfectadas = cmdEliminarArticulo.ExecuteNonQuery();

                if ((int)retorno.Value == 1)
                {
                    throw new ExcepcionPersistencia("No existe el articulo con el codigo de barras: " + codigoBarras);
                }

                if (filasAfectadas < 1)
                {
                    throw new ExcepcionPersistencia("Error al eliminar el Articulo con el codigo: " + codigoBarras);
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

        public static void Modificar(Articulo articulo)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdModificarArticulo = new SqlCommand("ModificarArticulo", conexion);
                cmdModificarArticulo.CommandType = CommandType.StoredProcedure;

                cmdModificarArticulo.Parameters.AddWithValue("@codigoBarras", articulo.CodigoBarras);
                cmdModificarArticulo.Parameters.AddWithValue("@nombre", articulo.Nombre);
                cmdModificarArticulo.Parameters.AddWithValue("@precio", articulo.Precio);
                cmdModificarArticulo.Parameters.AddWithValue("@stock", articulo.Stock);
                cmdModificarArticulo.Parameters.AddWithValue("@descripcion", articulo.Descripcion);
                cmdModificarArticulo.Parameters.AddWithValue("@imagen", articulo.Imagen);
                cmdModificarArticulo.Parameters.AddWithValue("@categoria", articulo.Categoria.Nombre);
                cmdModificarArticulo.Parameters.AddWithValue("@eliminado", articulo.Eliminado);

                SqlParameter retorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                retorno.Direction = ParameterDirection.ReturnValue;
                cmdModificarArticulo.Parameters.Add(retorno);

                conexion.Open();

                int filasAfectadas = cmdModificarArticulo.ExecuteNonQuery();

                if ((int)retorno.Value == 1)
                {
                    throw new ExcepcionPersistencia("No existe el articulo con el codigo de barras: " + articulo.CodigoBarras);
                }

                if (filasAfectadas < 0)
                {
                    throw new ExcepcionPersistencia("Ocurrio un error al modificar el Articulo de codigo: " + articulo.CodigoBarras);
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

        public static Articulo Buscar(long codigoBarras, bool buscar)
        {
            SqlConnection conexion = null;
            SqlDataReader drArticulo = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                if (buscar)
                {
                    SqlCommand cmdBuscarArticulo = new SqlCommand("BuscarArticuloSinDiscriminarEliminado", conexion);
                    cmdBuscarArticulo.CommandType = CommandType.StoredProcedure;

                    cmdBuscarArticulo.Parameters.AddWithValue("@codigoBarras", codigoBarras);

                    conexion.Open();

                    drArticulo = cmdBuscarArticulo.ExecuteReader();
                }
                else
                {
                    SqlCommand cmdBuscarArticulo = new SqlCommand("BuscarArticuloNoEliminado", conexion);
                    cmdBuscarArticulo.CommandType = CommandType.StoredProcedure;

                    cmdBuscarArticulo.Parameters.AddWithValue("@codigoBarras", codigoBarras);

                    conexion.Open();

                    drArticulo = cmdBuscarArticulo.ExecuteReader();
                }

                CategoriaArticulo categoria;
                Articulo articulo = null;
                bool buscarCategoria = false;

                if (drArticulo.Read())
                {
                    categoria = PersistenciaCategoriaArticulo.Buscar((string)drArticulo["Categoria"], buscarCategoria);

                    articulo = new Articulo((long)drArticulo["CodigoBarras"], (string)drArticulo["Nombre"], (double)drArticulo["Precio"], (int)drArticulo["Stock"], (string)drArticulo["Descripcion"], (string)drArticulo["Imagen"], categoria, (bool)drArticulo["Eliminado"]);
                }

                return articulo;

            }
            finally
            {
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public static List<Articulo> BuscarXNombre(string nombre, bool buscar)
        {
            SqlConnection conexion = null;
            SqlDataReader drArticulo = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                if (buscar)
                {
                    SqlCommand cmdBuscarXNombre = new SqlCommand("BuscarTodosArticulosXNombreMayorMenor", conexion);
                    cmdBuscarXNombre.CommandType = CommandType.StoredProcedure;

                    cmdBuscarXNombre.Parameters.AddWithValue("@nombreBuscado", nombre);

                    conexion.Open();

                    drArticulo = cmdBuscarXNombre.ExecuteReader();
                }
                else
                {
                    SqlCommand cmdBuscarXNombre = new SqlCommand("BuscarTodosArticulosXNombreMenorMayor", conexion);
                    cmdBuscarXNombre.CommandType = CommandType.StoredProcedure;

                    cmdBuscarXNombre.Parameters.AddWithValue("@nombreBuscado", nombre);

                    conexion.Open();

                    drArticulo = cmdBuscarXNombre.ExecuteReader();

                }

                List<Articulo> Articulos = new List<Articulo>();

                CategoriaArticulo categoria;
                Articulo articulo = null;
                bool buscarCategoria = false;

                while (drArticulo.Read())
                {
                    categoria = PersistenciaCategoriaArticulo.Buscar((string)drArticulo["Categoria"], buscarCategoria);

                    articulo = new Articulo((long)drArticulo["CodigoBarras"], (string)drArticulo["Nombre"], (double)drArticulo["Precio"], (int)drArticulo["Stock"], (string)drArticulo["Descripcion"], (string)drArticulo["Imagen"], categoria, (bool)drArticulo["Eliminado"]);

                    Articulos.Add(articulo);
                }

                return Articulos;

            }
            finally
            {
                if (drArticulo != null)
                {
                    drArticulo.Close();
                }

                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public static List<Articulo> Listar(bool ordenamiento)
        {
            SqlConnection conexion = null;
            SqlDataReader drArticulo = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                if (ordenamiento)
                {
                    SqlCommand cmdListarArticulos = new SqlCommand("ListarArticulosPrecioMayorAMenor", conexion);
                    cmdListarArticulos.CommandType = CommandType.StoredProcedure;

                    conexion.Open();

                    drArticulo = cmdListarArticulos.ExecuteReader();
                }
                else
                {
                    SqlCommand cmdListarArticulos = new SqlCommand("ListarArticulosPrecioMenorAMayor", conexion);
                    cmdListarArticulos.CommandType = CommandType.StoredProcedure;

                    conexion.Open();

                    drArticulo = cmdListarArticulos.ExecuteReader();
                }

                List<Articulo> Articulos = new List<Articulo>();

                CategoriaArticulo categoria; 
                Articulo articulo;
                bool buscarCategoria = false;

                while (drArticulo.Read())
                {
                    categoria = PersistenciaCategoriaArticulo.Buscar((string)drArticulo["Categoria"], buscarCategoria);

                    articulo = new Articulo((long)drArticulo["CodigoBarras"], (string)drArticulo["Nombre"], (double)drArticulo["Precio"], (int)drArticulo["Stock"], (string)drArticulo["Descripcion"], (string)drArticulo["Imagen"], categoria, (bool)drArticulo["Eliminado"]);

                    Articulos.Add(articulo);
                }

                return Articulos;
            }
            finally
            {
                if (drArticulo != null)
                {
                    drArticulo.Close();
                }

                if (conexion != null)
                {
                    conexion.Close();
                }
            }

        }

        public static List<Articulo> ListarXCategoria(string categoria, bool ordenamiento)
        {
            SqlConnection conexion = null;
            SqlDataReader drArticulo = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                if (ordenamiento)
                {
                    SqlCommand cmdListarArticulosXCategoria = new SqlCommand("ListarArticulosXCategoriaPrecioMayorAMenor", conexion);
                    cmdListarArticulosXCategoria.CommandType = CommandType.StoredProcedure;

                    cmdListarArticulosXCategoria.Parameters.AddWithValue("@categoria", categoria);

                    conexion.Open();

                    drArticulo = cmdListarArticulosXCategoria.ExecuteReader();
                }
                else
                {
                    SqlCommand cmdListarArticulosXCategoria = new SqlCommand("ListarArticulosXCategoriaPrecioMenorAMayor", conexion);
                    cmdListarArticulosXCategoria.CommandType = CommandType.StoredProcedure;

                    cmdListarArticulosXCategoria.Parameters.AddWithValue("@categoria", categoria);

                    conexion.Open();

                    drArticulo = cmdListarArticulosXCategoria.ExecuteReader();
                }

                List<Articulo> Articulos = new List<Articulo>();

                CategoriaArticulo categoriaAriculo;
                Articulo articulo;
                bool buscarCategoria = false;

                while (drArticulo.Read())
                {
                    categoriaAriculo = PersistenciaCategoriaArticulo.Buscar((string)drArticulo["Categoria"], buscarCategoria);

                    articulo = new Articulo((long)drArticulo["CodigoBarras"], (string)drArticulo["Nombre"], (double)drArticulo["Precio"], (int)drArticulo["Stock"], (string)drArticulo["Descripcion"], (string)drArticulo["Imagen"], categoriaAriculo, (bool)drArticulo["Eliminado"]);

                    Articulos.Add(articulo);
                }

                return Articulos;
            }
            finally
            {
                if (drArticulo != null)
                {
                    drArticulo.Close();
                }

                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public static List<Articulo> ListarArticulosGruposCategoria(bool ordenamiento)
        {
            SqlConnection conexion = null;
            SqlDataReader drArticulo = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                if (ordenamiento)
                {
                    SqlCommand cmdListarArticulosCategoriaAZ = new SqlCommand("ListarArticulosCategoriaAZ", conexion);
                    cmdListarArticulosCategoriaAZ.CommandType = CommandType.StoredProcedure;

                    conexion.Open();

                    drArticulo = cmdListarArticulosCategoriaAZ.ExecuteReader();
                }
                else
                {
                    SqlCommand cmdListarArticulosCategoriaZA = new SqlCommand("ListarArticulosCategoriaZA", conexion);
                    cmdListarArticulosCategoriaZA.CommandType = CommandType.StoredProcedure;

                    conexion.Open();

                    drArticulo = cmdListarArticulosCategoriaZA.ExecuteReader();
                }

                List<Articulo> Articulos = new List<Articulo>();

                CategoriaArticulo categoriaAriculo;
                Articulo articulo;
                bool buscarCategoria = false;

                while (drArticulo.Read())
                {
                    categoriaAriculo = PersistenciaCategoriaArticulo.Buscar((string)drArticulo["Categoria"], buscarCategoria);

                    articulo = new Articulo((long)drArticulo["CodigoBarras"], (string)drArticulo["Nombre"], (double)drArticulo["Precio"], (int)drArticulo["Stock"], (string)drArticulo["Descripcion"], (string)drArticulo["Imagen"], categoriaAriculo, (bool)drArticulo["Eliminado"]);

                    Articulos.Add(articulo);
                }

                return Articulos;
            }
            finally
            {
                if (drArticulo != null)
                {
                    drArticulo.Close();
                }

                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public static List<Articulo> ListarTodosLosArticulos()
        {
            SqlConnection conexion = null;
            SqlDataReader drArticulo = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdListarTodosLosArticulos = new SqlCommand("ListarTodosLosArticulos", conexion);
                cmdListarTodosLosArticulos.CommandType = CommandType.StoredProcedure;

                conexion.Open();

                drArticulo = cmdListarTodosLosArticulos.ExecuteReader();

                List<Articulo> Articulos = new List<Articulo>();

                CategoriaArticulo categoriaAriculo;
                Articulo articulo;
                bool buscarCategoria = false;

                while (drArticulo.Read())
                {
                    categoriaAriculo = PersistenciaCategoriaArticulo.Buscar((string)drArticulo["Categoria"], buscarCategoria);

                    articulo = new Articulo((long)drArticulo["CodigoBarras"], (string)drArticulo["Nombre"], (double)drArticulo["Precio"], (int)drArticulo["Stock"], (string)drArticulo["Descripcion"], (string)drArticulo["Imagen"], categoriaAriculo, (bool)drArticulo["Eliminado"]);

                    Articulos.Add(articulo);
                }

                return Articulos;
            }
            finally
            {
                if (drArticulo != null)
                {
                    drArticulo.Close();
                }

                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public static List<Articulo> ListarXCategoriaDesordenado(string categoria)
        {
            SqlConnection conexion = null;
            SqlDataReader drArticulo = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdListarArticulosXCategoria = new SqlCommand("ListarArticulosXCategoria", conexion);
                cmdListarArticulosXCategoria.CommandType = CommandType.StoredProcedure;

                cmdListarArticulosXCategoria.Parameters.AddWithValue("@categoria", categoria);

                conexion.Open();

                drArticulo = cmdListarArticulosXCategoria.ExecuteReader();
                
                List<Articulo> Articulos = new List<Articulo>();

                CategoriaArticulo categoriaAriculo;
                Articulo articulo;
                bool buscarCategoria = false;

                while (drArticulo.Read())
                {
                    categoriaAriculo = PersistenciaCategoriaArticulo.Buscar((string)drArticulo["Categoria"], buscarCategoria);

                    articulo = new Articulo((long)drArticulo["CodigoBarras"], (string)drArticulo["Nombre"], (double)drArticulo["Precio"], (int)drArticulo["Stock"], (string)drArticulo["Descripcion"], (string)drArticulo["Imagen"], categoriaAriculo, (bool)drArticulo["Eliminado"]);

                    Articulos.Add(articulo);
                }

                return Articulos;
            }
            finally
            {
                if (drArticulo != null)
                {
                    drArticulo.Close();
                }

                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }
    }
}
