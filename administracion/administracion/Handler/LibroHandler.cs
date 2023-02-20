
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using administracion.Models;

namespace administracion.Handler
{
    public class LibroHandler : Handler<Libro>
    {
        public override string GetTabla()
        {
            return "LIBRO";
        }
        public override Libro CreateObject(SqlDataReader dataReader)
        {
            Libro libro = new Libro(dataReader.GetInt64(0), dataReader.GetString(1),
                dataReader.GetString(2), dataReader.GetString(3), dataReader.GetString(4),
                dataReader.GetString(5), dataReader.GetInt32(6));
            return libro;
        }
        public override List<Libro> CreateObjects(SqlDataReader dataReader)
        {
            List<Libro> libros = new List<Libro>();
            while (dataReader.Read())
            {
                libros.Add(new Libro(dataReader.GetInt64(0), dataReader.GetString(1),
                dataReader.GetString(2), dataReader.GetString(3), dataReader.GetString(4),
                dataReader.GetString(5), dataReader.GetInt32(6)));
            }
            return libros;
        }
        public override Dictionary<string, object> GetKeyValuePairs(Libro libro)
        {
            return new Dictionary<string, object>()
            {
                { "ID_USUARIO", libro.Id },
                {"TITULO", libro.Titulo },
                {"NOMBR_EAUTOR", libro.NombreAutor },
                {"APELLIDO_AUTOR", libro.ApellidoAutor },
                {"EDITORIAL", libro.Editorial},
                {"SECCION", libro.Seccion},
                {"CANTIDAD", libro.Cantidad }
            };
        }
        public string ImprimirLibros(List<Libro> libros)
        {
            string texto = String.Format("=================== LIBROS ======================\n" +
                "{0,-25}{1,-50}{2,-30}{3,-30}{4,-30}{5,-30}{6,-20}\n", "ID", "TITULO",
                "NOMBRE_AUTOR", "APELLIDO_AUTOR", "EDITORIAL", "SECCION", "CANTIDAD");
            try
            {
                foreach (Libro libro in libros)
                {
                    texto += libro + "\n";
                }
                return texto;
            }
            catch (System.NullReferenceException e)
            {
                return "\n %%% NO HAY LIBROS %%% \n";
            }
        }

    }
}
