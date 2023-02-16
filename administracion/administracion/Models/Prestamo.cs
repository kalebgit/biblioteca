using administracion.Handler;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace administracion.Models
{
    internal class Prestamo
    {
        public long IdUsuario { get; set; }
        public long IdLibro { get; set; }
        public long IdStaff { get; set; }
        public string FechaPrestamo { get; set; }
        public string FechaDevolucion { get; set; }
        public string Estatus { get; set; }

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

        
    }
}
