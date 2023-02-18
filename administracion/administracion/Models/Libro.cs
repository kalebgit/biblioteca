using administracion.Handler;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace administracion.Models
{
    public class Libro
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

        public override string ToString()
        {
            return String.Format("{0,-25}{1,-50}{2,-30}{3,-30}{4,-30}{5,-30}{6,-20}",
                Id, Titulo, NombreAutor, ApellidoAutor, Editorial, Seccion, Cantidad);
        }
    }
}
