using administracion.Handler;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace administracion.Models
{
    public delegate void Creacion(Object obj);

    public class Prestamo
    {
        public long IdUsuario { get; set; }
        public long IdLibro { get; set; }
        public long IdStaff { get; set; }
        public string FechaPrestamo { get; set; }
        public string FechaDevolucion { get; set; }
        public string Estatus { get; set; }

        public Prestamo(Usuario usuario, Libro libro, Staff staff,
            string fechaPrestamo, string fechaDevolucion, string estatus)
        {
            IdUsuario = usuario.Id;
            IdLibro = libro.Id;
            IdStaff = staff.Id;
            FechaPrestamo = fechaPrestamo;
            FechaDevolucion = fechaDevolucion;
            Estatus = estatus;
        }
        public Prestamo(long idUsuario, long idLibro, long idStaff,
            string fechaPrestamo, string fechaDevolucion, string estatus)
        {
            IdUsuario = idUsuario;
            IdLibro = idLibro;
            IdStaff = idStaff;
            FechaPrestamo = fechaPrestamo;
            FechaDevolucion = fechaDevolucion;
            Estatus = estatus;
        }

        public Prestamo()
        {

        }
        public override string ToString()
        {
            return String.Format("{0,-30}{1,-30}{2,-30}{3,-30}{4,-30}{5,-30}",
                IdUsuario, IdLibro, IdStaff, FechaPrestamo, FechaDevolucion, Estatus);
        }
    }
}
