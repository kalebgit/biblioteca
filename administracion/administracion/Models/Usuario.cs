using administracion.Handler;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace administracion.Models
{
    public class Usuario
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Mail { get; set; }
        public long Telefono { get; set; }

        public Usuario(long id, string nombre, string apellido, 
            int edad, string mail, long telefono)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Edad = edad;
            Mail = mail;
            Telefono = telefono;
        }

        public override string ToString()
        {
            return String.Format("{0,-25}{1,-30}{2,-30}{3,-10}{4,-30}{5,-30}", Id,
                Nombre, Apellido, Edad, Mail, Telefono);
        }

    }
}
