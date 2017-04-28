using System;
using System.Collections.Generic;
using System.Text;

using EntidadesCompartidas.ObjetosNegocio;
using EntidadesCompartidas.Excepciones;
using Persistencia;

namespace Logica
{
    public class LogicaCategoriaArticulo
    {
        public static void Validar(CategoriaArticulo categoria)
        {
            if (categoria == null)
            {
                throw new ExcepcionLogica("La categoria es nula,");
            }

            if (categoria.Nombre.Length > 20)
            {
                throw new ExcepcionLogica("El nombre no puede tener más de 20 caracteres de longitud.");
            }

            if (categoria.Descripcion.Length > 1000)
            {
                throw new ExcepcionLogica("La descripción no puede tener más de 100 caracteres de longitud.");
            }
        }

        public static void Agregar(CategoriaArticulo categoria)
        {
            Validar(categoria);

            PersistenciaCategoriaArticulo.Agregar(categoria);
        }

        public static void Eliminar(string nombre)
        {
            PersistenciaCategoriaArticulo.Eliminar(nombre);        
        }

        public static void Modificar(CategoriaArticulo categoria)
        {
            Validar(categoria);

            PersistenciaCategoriaArticulo.Modificar(categoria);
        }

        public static CategoriaArticulo Buscar(string nombre, bool buscar)
        {
            return PersistenciaCategoriaArticulo.Buscar(nombre, buscar);
        }

        public static List<CategoriaArticulo> Listar()
        {
            List<CategoriaArticulo> Categorias = PersistenciaCategoriaArticulo.Listar();

            return Categorias;
        }

    }
}
