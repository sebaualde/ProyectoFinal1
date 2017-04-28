using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using EntidadesCompartidas.ObjetosNegocio;
using EntidadesCompartidas.Excepciones;

namespace Persistencia
{
    public class PersistenciaPedido
    {
        public static void AgregarPedido(Pedido pedido)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdAgregarPedido = new SqlCommand("AgregarPedido", conexion);
                cmdAgregarPedido.CommandType = CommandType.StoredProcedure;

                cmdAgregarPedido.Parameters.AddWithValue("@fecha", pedido.Fecha);
                cmdAgregarPedido.Parameters.AddWithValue("@precioTotal", pedido.PrecioTotal);
                cmdAgregarPedido.Parameters.AddWithValue("@enviado", pedido.Enviado);
                cmdAgregarPedido.Parameters.AddWithValue("@cedulaUsuario", pedido.Registrado.Cedula);

                SqlParameter numeroPedido = new SqlParameter("@numeroPedido", SqlDbType.Int);
                numeroPedido.Direction = ParameterDirection.Output;
                cmdAgregarPedido.Parameters.Add(numeroPedido);

                conexion.Open();

                int filasAfectadas = cmdAgregarPedido.ExecuteNonQuery();

                int numeroPedidoAgregado = Convert.ToInt32(numeroPedido.Value);

                List<LineaPedido> lineasPedido = pedido.LineasPedidos;

                foreach (LineaPedido lp in lineasPedido)
                {
                    PersistenciaPedido.AgregarLineasDePedido(lp, numeroPedidoAgregado);

                    Articulo artciulo = PersistenciaArticulo.Buscar(lp.PArticulo.CodigoBarras, false);

                    artciulo.Stock = artciulo.Stock - lp.Cantidad;

                    PersistenciaArticulo.Modificar(artciulo);
                }

                if (filasAfectadas < 1)
                {
                    throw new ExcepcionPersistencia("Ocurrio un error al agregar el Pedido.");
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

        public static void AgregarLineasDePedido(LineaPedido lineaPedido, int numeroPedido)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdAgregarLineasDePedido = new SqlCommand("AgregarLineaPedido", conexion);
                cmdAgregarLineasDePedido.CommandType = CommandType.StoredProcedure;

                cmdAgregarLineasDePedido.Parameters.AddWithValue("@cantidad", lineaPedido.Cantidad);
                cmdAgregarLineasDePedido.Parameters.AddWithValue("@codigoArticulo", lineaPedido.PArticulo.CodigoBarras);
                cmdAgregarLineasDePedido.Parameters.AddWithValue("@numeroPedido", numeroPedido);

                conexion.Open();

                int filasAfectadas = cmdAgregarLineasDePedido.ExecuteNonQuery();

                if (filasAfectadas < 1)
                {
                    throw new ExcepcionPersistencia("Ocurrio un error al agregar las lineas de pedido.");
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

        public static void ModificarEnvioPedido(int numero, bool enviado)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdModificarEnvioPedido = new SqlCommand("ModificarEnvioPedido", conexion);
                cmdModificarEnvioPedido.CommandType = CommandType.StoredProcedure;

                cmdModificarEnvioPedido.Parameters.AddWithValue("@numero", numero);
                cmdModificarEnvioPedido.Parameters.AddWithValue("@enviado", enviado);                

                conexion.Open();

                int filasAfectadas = cmdModificarEnvioPedido.ExecuteNonQuery();

                if (filasAfectadas < 1)
                {
                    throw new ExcepcionPersistencia("Ocurrio un error al modificar el envio del Pedido.");
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

        public static void EliminarPedido(Pedido pedido)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdEliminarPedido = new SqlCommand("EliminarPedido", conexion);
                cmdEliminarPedido.CommandType = CommandType.StoredProcedure;

                cmdEliminarPedido.Parameters.AddWithValue("@numero", pedido.Numero);

                SqlParameter retorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                retorno.Direction = ParameterDirection.ReturnValue;
                cmdEliminarPedido.Parameters.Add(retorno);

                conexion.Open();

                int filasAfectadas = cmdEliminarPedido.ExecuteNonQuery();

                switch ((int)retorno.Value)
                {
                    case 1:
                        throw new ExcepcionPersistencia("No existe un pedido con el número: " + pedido.Numero);
                        break;
                    case 2:
                        throw new ExcepcionPersistencia("El pedido no puede eliminarse porque ya fue enviado al usuario.");
                        break;
                    case 3:
                        throw new ExcepcionPersistencia("Ocurrio un error al eliminar las lineas del pedido.");
                        break;
                    case 4:
                        throw new ExcepcionPersistencia("No se pudo Eliminar el pedido.");
                        break;
                }

                if (filasAfectadas < 1)
                {
                    throw new ExcepcionPersistencia("Error al eliminar el Pedido con el número: " + pedido.Numero);
                }
                else
                {
                    foreach (LineaPedido lp in pedido.LineasPedidos)
                    {
                        if (lp.PArticulo.Eliminado == false)
                        {
                            Articulo artciulo = PersistenciaArticulo.Buscar(lp.PArticulo.CodigoBarras, false);

                            artciulo.Stock = artciulo.Stock + lp.Cantidad;

                            PersistenciaArticulo.Modificar(artciulo);
                        }
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

        public static Pedido BuscarPedido(int numero)
        {
            SqlConnection conexion = null;
            SqlDataReader drPedido = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdBuscarPedido = new SqlCommand("BuscarPedido", conexion);
                cmdBuscarPedido.CommandType = CommandType.StoredProcedure;

                cmdBuscarPedido.Parameters.AddWithValue("@numero", numero);

                conexion.Open();

                drPedido = cmdBuscarPedido.ExecuteReader();

                Pedido pedido = new Pedido ();

                List<LineaPedido> lineasPedido;
                UsuarioRegistrado registrado;

                if (drPedido.Read())
                {
                    registrado = PersistenciaUsuarioRegistrado.Buscar((int)drPedido["CedulaUsuario"], false);

                    lineasPedido = PersistenciaPedido.BuscarLineasPedido((int)drPedido["Numero"]);

                    pedido = new Pedido((int)drPedido["Numero"], (DateTime)drPedido["Fecha"], (double)drPedido["PrecioTotal"], (bool)drPedido["Enviado"], registrado, lineasPedido);
                }

                return pedido;
            }
            finally
            {
                if (drPedido != null)
                {
                    drPedido.Close();
                }

                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public static List<LineaPedido> BuscarLineasPedido(int numero)
        {
            SqlConnection conexion = null;
            SqlDataReader drLineaPedido = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdBuscarLineasPedido = new SqlCommand("BuscarLineasPedido", conexion);
                cmdBuscarLineasPedido.CommandType = CommandType.StoredProcedure;

                cmdBuscarLineasPedido.Parameters.AddWithValue("@numero", numero);

                conexion.Open();

                drLineaPedido = cmdBuscarLineasPedido.ExecuteReader();

                List<LineaPedido> LineasPedidos = new List<LineaPedido> ();

                LineaPedido lineaPedido;
                Articulo articulo;

                while (drLineaPedido.Read())
                {
                    articulo = PersistenciaArticulo.Buscar((long)drLineaPedido["CodigoArticulo"], true);

                    lineaPedido = new LineaPedido((int)drLineaPedido["Numero"], (int)drLineaPedido["Cantidad"], articulo);
                    LineasPedidos.Add(lineaPedido);
                }

                return LineasPedidos;
            }
            finally
            {
                if (drLineaPedido != null)
                {
                    drLineaPedido.Close();
                }

                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public static List<Pedido> ListarTodosLosPedidos()
        {
            SqlConnection conexion = null;
            SqlDataReader drPedidos = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdListarTodosLosPedidos = new SqlCommand("ListarTodosLosPedidos", conexion);
                cmdListarTodosLosPedidos.CommandType = CommandType.StoredProcedure;

                conexion.Open();

                drPedidos = cmdListarTodosLosPedidos.ExecuteReader();

                List<Pedido> pedidos = new List<Pedido>();
                List<LineaPedido> LineasPedido;

                Pedido pedido;
                UsuarioRegistrado registrado;

                while (drPedidos.Read())
                {
                    registrado = PersistenciaUsuarioRegistrado.Buscar((int)drPedidos["CedulaUsuario"], false);

                    LineasPedido = PersistenciaPedido.BuscarLineasPedido((int)drPedidos["Numero"]);

                    pedido = new Pedido((int)drPedidos["Numero"], (DateTime)drPedidos["Fecha"], (double)drPedidos["PrecioTotal"], (bool)drPedidos["Enviado"], registrado, LineasPedido);

                    pedidos.Add(pedido);
                }

                return pedidos;
            }
            finally
            {
                if (drPedidos != null)
                {
                    drPedidos.Close();
                }

                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public static List<Pedido> ListarPedidosPendientes()
        {
            SqlConnection conexion = null;
            SqlDataReader drPedidoPendiente = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdListarPedidosPendientes = new SqlCommand("ListarPedidosPendientes", conexion);
                cmdListarPedidosPendientes.CommandType = CommandType.StoredProcedure;

                conexion.Open();

                drPedidoPendiente = cmdListarPedidosPendientes.ExecuteReader();
              
                List<Pedido> pedidosPendientes = new List<Pedido> ();
                List<LineaPedido> LineasPedido;

                Pedido pedido;
                UsuarioRegistrado registrado;

                while (drPedidoPendiente.Read())
                {
                    registrado = PersistenciaUsuarioRegistrado.Buscar((int)drPedidoPendiente["CedulaUsuario"], false);

                    LineasPedido = PersistenciaPedido.BuscarLineasPedido((int)drPedidoPendiente["Numero"]);

                    pedido = new Pedido((int)drPedidoPendiente["Numero"], (DateTime)drPedidoPendiente["Fecha"], (double)drPedidoPendiente["PrecioTotal"], (bool)drPedidoPendiente["Enviado"], registrado, LineasPedido);

                    pedidosPendientes.Add(pedido);
                }

                return pedidosPendientes;
            }
            finally
            {
                if (drPedidoPendiente != null)
                {
                    drPedidoPendiente.Close();
                }

                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public static List<Pedido> ListarPedidosEnviados()
        {
            SqlConnection conexion = null;
            SqlDataReader drPedidoEnviado = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdListarPedidosEnviados = new SqlCommand("ListarPedidosEnviados", conexion);
                cmdListarPedidosEnviados.CommandType = CommandType.StoredProcedure;

                conexion.Open();

                drPedidoEnviado = cmdListarPedidosEnviados.ExecuteReader();

                List<Pedido> pedidosEnviados = new List<Pedido>();
                List<LineaPedido> LineasPedido;

                Pedido pedido;
                UsuarioRegistrado registrado;

                while (drPedidoEnviado.Read())
                {
                    registrado = PersistenciaUsuarioRegistrado.Buscar((int)drPedidoEnviado["CedulaUsuario"], false);

                    LineasPedido = PersistenciaPedido.BuscarLineasPedido((int)drPedidoEnviado["Numero"]);

                    pedido = new Pedido((int)drPedidoEnviado["Numero"], (DateTime)drPedidoEnviado["Fecha"], (double)drPedidoEnviado["PrecioTotal"], (bool)drPedidoEnviado["Enviado"], registrado, LineasPedido);

                    pedidosEnviados.Add(pedido);
                }

                return pedidosEnviados;
            }
            finally
            {
                if (drPedidoEnviado != null)
                {
                    drPedidoEnviado.Close();
                }

                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public static List<Pedido> ListarPedidosPorUsuario(int cedula)
        {
            SqlConnection conexion = null;
            SqlDataReader drPedidoPorUsuario = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdListarPedidosPorUsuario = new SqlCommand("ListarPedidosPorUsuario", conexion);
                cmdListarPedidosPorUsuario.CommandType = CommandType.StoredProcedure;

                cmdListarPedidosPorUsuario.Parameters.AddWithValue("@cedula", cedula);

                conexion.Open();

                drPedidoPorUsuario = cmdListarPedidosPorUsuario.ExecuteReader();

                List<Pedido> pedidosPorUsuario = new List<Pedido>();
                List<LineaPedido> LineasPedido;

                Pedido pedido;
                UsuarioRegistrado registrado;

                while (drPedidoPorUsuario.Read())
                {
                    registrado = PersistenciaUsuarioRegistrado.Buscar((int)drPedidoPorUsuario["CedulaUsuario"], false);

                    LineasPedido = PersistenciaPedido.BuscarLineasPedido((int)drPedidoPorUsuario["Numero"]);

                    pedido = new Pedido((int)drPedidoPorUsuario["Numero"], (DateTime)drPedidoPorUsuario["Fecha"], (double)drPedidoPorUsuario["PrecioTotal"], (bool)drPedidoPorUsuario["Enviado"], registrado, LineasPedido);

                    pedidosPorUsuario.Add(pedido);
                }

                return pedidosPorUsuario;
            }
            finally
            {
                if (drPedidoPorUsuario != null)
                {
                    drPedidoPorUsuario.Close();
                }

                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public static List<Pedido> ListarPedidosPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            SqlConnection conexion = null;
            SqlDataReader drPedidoPorFecha = null;

            try
            {
                conexion = new SqlConnection(Conexion.CadenaConexion);

                SqlCommand cmdListarPedidosPorFecha = new SqlCommand("ListarPedidosPorFecha", conexion);
                cmdListarPedidosPorFecha.CommandType = CommandType.StoredProcedure;

                cmdListarPedidosPorFecha.Parameters.AddWithValue("@inicio", fechaInicio);
                cmdListarPedidosPorFecha.Parameters.AddWithValue("@fin", fechaFin);

                conexion.Open();

                drPedidoPorFecha = cmdListarPedidosPorFecha.ExecuteReader();

                List<Pedido> pedidosPorFecha = new List<Pedido>();
                List<LineaPedido> LineasPedido;

                Pedido pedido;
                UsuarioRegistrado registrado;

                while (drPedidoPorFecha.Read())
                {
                    registrado = PersistenciaUsuarioRegistrado.Buscar((int)drPedidoPorFecha["CedulaUsuario"], false);

                    LineasPedido = PersistenciaPedido.BuscarLineasPedido((int)drPedidoPorFecha["Numero"]);

                    pedido = new Pedido((int)drPedidoPorFecha["Numero"], (DateTime)drPedidoPorFecha["Fecha"], (double)drPedidoPorFecha["PrecioTotal"], (bool)drPedidoPorFecha["Enviado"], registrado, LineasPedido);

                    pedidosPorFecha.Add(pedido);
                }

                return pedidosPorFecha;
            }
            finally
            {
                if (drPedidoPorFecha != null)
                {
                    drPedidoPorFecha.Close();
                }

                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

    }
        
}
