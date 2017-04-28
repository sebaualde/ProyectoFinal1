using System;
using System.Collections.Generic;
using System.Text;

using EntidadesCompartidas.ObjetosNegocio;
using EntidadesCompartidas.Excepciones;
using Persistencia;

namespace Logica
{
    public class LogicaArticulo
    {
        public static void Validar(Articulo articulo)
        {
            if (articulo == null)
            {
                throw new ExcepcionLogica("El articulo es nulo.");
            }

            if (articulo.CodigoBarras < 1)
            {
                throw new ExcepcionLogica("El código de barras debe ser mayor a cero.");
            }

            if (articulo.Nombre.Length > 50)
            {
                throw new ExcepcionLogica("El nombre del articulo no puede tener más de 50 caracteres de longitud.");
            }

            if (articulo.Precio < 1)
            {
                throw new ExcepcionLogica("El precio del articulo no puede ser inferior a 1.");
            }

            if (articulo.Stock < 1)
            {
                throw new ExcepcionLogica("El stock de ingreso del articulo no puede ser inferior a 1.");
            }

            if (articulo.Descripcion.Length > 1000)
            {
                throw new ExcepcionLogica("La descripcion del articulo no puede tener más de 100 caracteres de longitud.");
            }

            if (string.IsNullOrEmpty(articulo.Imagen))
            {
                throw new ExcepcionLogica("La imagen del articulo no puede quedar en blanco.");
            }

            if (articulo.Imagen.Length > 500)
            {
                throw new ExcepcionLogica("La ruta donde guardara la imagen no puede tener mas de 500 caracteres.");
            }

            LogicaCategoriaArticulo.Validar(articulo.Categoria);
        }

        public static void Agregar(Articulo articulo)
        {
            Validar(articulo);

            PersistenciaArticulo.Agregar(articulo);
        }

        public static void Eliminar(long codigoBarras)
        {
            PersistenciaArticulo.Eliminar(codigoBarras);
        }

        public static void Modificar(Articulo articulo)
        {
            Validar(articulo);

            PersistenciaArticulo.Modificar(articulo);
        }

        public static Articulo Buscar(long codigoBarras, bool buscar)
        {
            return PersistenciaArticulo.Buscar(codigoBarras, buscar);
        }

        public static List<Articulo> BuscarXNombre(string nombre, bool buscar)
        {
            List<Articulo> Articulos = PersistenciaArticulo.BuscarXNombre(nombre, buscar);

            return Articulos;
        }

        public static List<Articulo> ListarTodosLosArticulos()
        {
            List<Articulo> Articulos = PersistenciaArticulo.ListarTodosLosArticulos();

            return Articulos;
        }

        public static List<Articulo> Listar(bool ordenamiento)
        {
            List<Articulo> Articulos = PersistenciaArticulo.Listar(ordenamiento);

            return Articulos;
        }

        public static List<Articulo> ListarXCategoriaDesordenado(string categoria)
        {
            List<Articulo> Articulos = PersistenciaArticulo.ListarXCategoriaDesordenado(categoria);

            return Articulos;
        }

        public static List<Articulo> ListarXCategoria(string categoria, bool ordenamiento)
        {
            List<Articulo> Articulos = PersistenciaArticulo.ListarXCategoria(categoria, ordenamiento);

            return Articulos;
        }

        public static List<Articulo> ListarArticulosGruposCategoria(bool ordenamiento)
        {
            List<Articulo> Articulos = PersistenciaArticulo.ListarArticulosGruposCategoria(ordenamiento);

            return Articulos;
        }
        
    }
}
