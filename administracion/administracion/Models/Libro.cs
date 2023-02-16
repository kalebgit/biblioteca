using administracion.Handler;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace administracion.Models
{
    internal class Libro : Handler<Libro>
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string NombreAutor { get; set; }
        public string ApellidoAutor { get; set; }
        public string Editorial { get; set; }
        public string Seccion { get; set; }
        public int Cantidad { get; set; }

        public Libro(long id, string titulo, string nombreAutor,
            string apellidoAutor, string editorial, string seccion, int cantidad)
        {
            Id = id;
            Titulo = titulo;
            NombreAutor = nombreAutor;
            ApellidoAutor = apellidoAutor;
            Editorial = editorial;
            Seccion = seccion;
            Cantidad = cantidad;
        }

        public override string GetTabla()
        {
            return "LIBRO";
        }
        public override object CreateObject(SqlDataReader dataReader)
        {
            Libro libro = new Libro(dataReader.GetInt64(0), dataReader.GetString(1), 
                dataReader.GetString(2), dataReader.GetString(3), dataReader.GetString(4),
                dataReader.GetString(5), dataReader.GetInt32(6));
            return libro;
        }
        public override List<object> CreateObjects(SqlDataReader dataReader)
        {
            List<object> libros = new List<object>();
            while (dataReader.Read())
            {
                libros.Add(new Libro(dataReader.GetInt64(0), dataReader.GetString(1),
                dataReader.GetString(2), dataReader.GetString(3), dataReader.GetString(4),
                dataReader.GetString(5), dataReader.GetInt32(6)));
            }
            return libros;
        }
        public override Dictionary<string, object> GetKeyValuePairs()
        {
            return new Dictionary<string, object>()
            {
                { "ID_USUARIO", Id },
                {"TITULO", Titulo },
                {"NOMBR_EAUTOR", NombreAutor },
                {"APELLIDO_AUTOR", ApellidoAutor },
                {"EDITORIAL", Editorial},
                {"SECCION", Seccion},
                {"CANTIDAD", Cantidad }
            };
        }
    }
}
