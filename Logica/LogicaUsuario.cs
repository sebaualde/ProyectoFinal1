using System;
using System.Collections.Generic;
using System.Text;

using EntidadesCompartidas.ObjetosNegocio;
using EntidadesCompartidas.Excepciones;
using Persistencia;

namespace Logica
{
    public class LogicaUsuario
    {
        public static void Validar(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ExcepcionLogica("El usuario es nulo");
            }

            if (usuario.Cedula < 1)
            {
                throw new ExcepcionLogica("La cédula debe ser mayor que 0");
            }

            if (String.IsNullOrEmpty(usuario.NombreCompleto))
            {
                throw new ExcepcionLogica("El campo \"Nombre Completo\" no puede quedar vacío");
            }

            if (usuario.NombreCompleto.Length > 50)
            {
                throw new ExcepcionLogica("El nombre completo no puede superar los 50 caracteres");
            }

            if (String.IsNullOrEmpty(usuario.NombreUsuario))
            {
                throw new ExcepcionLogica("El campo \"Nombre de Usuario\" no puede quedar vacío");
            }

            if (usuario.NombreUsuario.Length > 20)
            {
                throw new ExcepcionLogica("El nombre de usuario no puede superar los 20 caracteres");
            }

            if (String.IsNullOrEmpty(usuario.Contrasenia))
            {
                throw new ExcepcionLogica("Debe ingresar una contraseña");
            }

            if (usuario.NombreCompleto.Length > 30)
            {
                throw new ExcepcionLogica("La contraseña puede tener 30 caracteres");
            }

            if (usuario is Administrador)
            {
                if (String.IsNullOrEmpty(((Administrador)usuario).Cargo))
                {
                    throw new ExcepcionLogica("El campo \"Cargo\" no puede quedar vacío");
                }

                if (((Administrador)usuario).Cargo.Length > 20)
                {
                    throw new ExcepcionLogica("El cargo no puede superar los 20 caracteres");
                }
            }

            if (usuario is UsuarioRegistrado)
            {
                if (String.IsNullOrEmpty(((UsuarioRegistrado)usuario).DireccionEnvio))
                {
                    throw new ExcepcionLogica("El campo \"Dirección\" no puede quedar vacío");
                }

                if (((UsuarioRegistrado)usuario).DireccionEnvio.Length > 50)
                {
                    throw new ExcepcionLogica("La dirección no puede superar los 50 caracteres");
                }

                if (((UsuarioRegistrado)usuario).NumeroTarjeta <= 0)
                {
                    throw new ExcepcionLogica("El número de tarjeta debe ser mayor que 0");
                }

                if (((UsuarioRegistrado)usuario).Telefono <= 0)
                {
                    throw new ExcepcionLogica("El teléfono debe ser mayor que 0");
                }

            }


        }

        public static void Agregar(Usuario usuario)
        {
            Validar(usuario);

            if (usuario is Administrador)
            {
                PersistenciaAdministrador.Agregar((Administrador)usuario);
            }
            else if (usuario is UsuarioRegistrado)
            {
                PersistenciaUsuarioRegistrado.Agregar((UsuarioRegistrado)usuario);
            }
            else
            {
                throw new ExcepcionLogica("Tipo de usuario no válido");
            }
        }

        public static void Modificar(Usuario usuario)
        {
            Validar(usuario);

            if (usuario is Administrador)
            {
                PersistenciaAdministrador.Modificar((Administrador)usuario);
            }
            else if (usuario is UsuarioRegistrado)
            {
                PersistenciaUsuarioRegistrado.Modificar((UsuarioRegistrado)usuario);
            }
            else
            {
                throw new ExcepcionLogica("Tipo de usuario no válido");
            }
        }

        public static void Eliminar(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ExcepcionLogica("El usuario es nulo");
            }

            if (usuario is Administrador)
            {
                PersistenciaAdministrador.Eliminar((Administrador)usuario);
            }
            else if (usuario is UsuarioRegistrado)
            {
                PersistenciaUsuarioRegistrado.Eliminar((UsuarioRegistrado)usuario);
            }
            else
            {
                throw new ExcepcionLogica("Tipo de usuario no válido");
            }
        }

        public static Usuario Buscar(int cedula, bool busqueda)
        {
            Usuario usuario = null;

            usuario = PersistenciaAdministrador.Buscar(cedula);

            if (usuario == null)
            {
                usuario = PersistenciaUsuarioRegistrado.Buscar(cedula, busqueda);
            }

            return usuario;
        }

        public static List<Usuario> Listar()
        {
            List<Usuario> usuarios = new List<Usuario>();

            foreach (Administrador a in PersistenciaAdministrador.Listar())
            {
                usuarios.Add(a);
            }

            foreach (UsuarioRegistrado r in PersistenciaUsuarioRegistrado.Listar())
            {
                usuarios.Add(r);
            }

            return usuarios;
        }

        public static List<Administrador> ListarAdministradores()
        {
            return PersistenciaAdministrador.Listar();
        }

        public static List<UsuarioRegistrado> ListarUsuariosRegistrados()
        {
            return PersistenciaUsuarioRegistrado.Listar();
        }
    }
}
