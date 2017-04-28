using System;
using System.Collections.Generic;
using System.Text;

using EntidadesCompartidas.ObjetosNegocio;
using EntidadesCompartidas.Excepciones;
using Persistencia;

namespace Logica
{
    public class LogicaPedido
    {
        public static void Validar(Pedido pedido)
        {
            if (pedido == null)
            {
                throw new ExcepcionLogica("El pedido es nulo.");
            }

            if (pedido.Numero < 1)
            {
                throw new ExcepcionLogica("El número del pedido debe ser mayor a cero.");
            }

            if (pedido.PrecioTotal < 0)
            {
                throw new ExcepcionLogica("El precio del articulo no puede ser inferior a 0.");
            }

            if (pedido.LineasPedidos == null)
            {
                throw new ExcepcionLogica("El pedido esta vacio.");
            }

            if (pedido.Registrado == null)
            {
                throw new ExcepcionLogica("El pedido no tiene un usuario asignado.");
            }

            LogicaUsuario.Validar(pedido.Registrado);
        }

        public static void AgregarPedido(Pedido pedido)
        {
            Validar(pedido);

            PersistenciaPedido.AgregarPedido(pedido);
        }

        public static void ModificarEnvioPedido(int numero, bool enviado)
        {
            PersistenciaPedido.ModificarEnvioPedido(numero, enviado);
        }

        public static void EliminarPedido(Pedido pedido)
        {
            PersistenciaPedido.EliminarPedido(pedido);
        }

        public static Pedido BuscarPedido(int numero)
        {
            Pedido pedido = PersistenciaPedido.BuscarPedido(numero);

            return pedido;
        }

        public static List<Pedido> ListarTodosLosPedidos()
        {
            List<Pedido> Pedidos = PersistenciaPedido.ListarTodosLosPedidos();

            return Pedidos;
        }

        public static List<Pedido> ListarPedidosPendientes()
        {
            List<Pedido> PedidosPendientes = PersistenciaPedido.ListarPedidosPendientes();

            return PedidosPendientes;
        }

        public static List<Pedido> ListarPedidosEnviados()
        {
            List<Pedido> PedidosEnviados = PersistenciaPedido.ListarPedidosEnviados();

            return PedidosEnviados;
        }

        public static List<Pedido> ListarPedidosPorUsuario(int cedula)
        {
            List<Pedido> PedidosPorUsuario = PersistenciaPedido.ListarPedidosPorUsuario(cedula);

            return PedidosPorUsuario;
        }

        public static List<Pedido> ListarPedidosPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            List<Pedido> PedidosPorFecha = PersistenciaPedido.ListarPedidosPorFecha(fechaInicio, fechaFin);

            return PedidosPorFecha;
        }
    }
}
