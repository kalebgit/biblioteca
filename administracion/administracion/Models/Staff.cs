using administracion.Handler;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace administracion.Models
{
    public class Staff
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Turno { get; set; }

        public Staff(long id, string nombre, string apellido, string turno)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Turno = turno;
        }
        public Staff()
        {

        }
        public override string ToString()
        {
            return String.Format("{0,-25}{1,-30}{2,-30}{3,-25}", Id, Nombre, Apellido, Turno);
        }
    }
}
