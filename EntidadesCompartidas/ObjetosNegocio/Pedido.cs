using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesCompartidas.ObjetosNegocio
{
    public class Pedido
    {
        private int _numero;
        private DateTime _fecha;
        private double _precioTotal;
        private bool _enviado;

        private UsuarioRegistrado _registrado;
        private List<LineaPedido> _LineasPedidos;

        public int Numero
        {
            get
            {
                return _numero;
            }
            set
            {
                _numero = value;
            }
        }

        public DateTime Fecha
        {
            get
            {
                return _fecha;
            }
            set
            {
                _fecha = value;
            }
        }

        public double PrecioTotal 
        {
            get
            {
                return _precioTotal;
            }
            set
            {
                _precioTotal = value;
            }
        }

        public bool Enviado
        {
            get
            {
                 return _enviado;
            }
            set
            {
                _enviado = value;
            }
        }

        public UsuarioRegistrado Registrado
        {
            get
            {
                return _registrado;
            }
            set
            {
                _registrado = value;
            }
        }

        public List<LineaPedido> LineasPedidos
        {
            get
            {
                return _LineasPedidos;
            }
            set
            {
                _LineasPedidos = value;
            }
        }

        public Pedido()
            :this (0,DateTime.Today, 0, false, new UsuarioRegistrado(), new List<LineaPedido> () )
        { 
        
        }

        public Pedido(int numero, DateTime fecha, double precioTotal, bool enviado, UsuarioRegistrado registrado, List<LineaPedido> lineasPedidos)
        {
            Numero = numero;
            Fecha = fecha;
            PrecioTotal = precioTotal;
            Enviado = enviado;
            Registrado = registrado;
            LineasPedidos = lineasPedidos;
        }

        public override string ToString()
        {
            return "Numero: " + Numero + "Fecha: " + Fecha + "Precio Total: " + PrecioTotal + "Enviado: " + (Enviado ? "Si" : "No") + "Usuario: " + Registrado.NombreCompleto + "Lineas de Pedidos: " + LineasPedidos.ToString();
        }
    }
}
