using administracion.Handler;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace administracion.Models
{
    internal class Usuario : Handler<Usuario>
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

        public override string GetTabla()
        {
            return "USUARIO";
        }
        public override object CreateObject(SqlDataReader dataReader)
        {
            Usuario usuario = new Usuario(dataReader.GetInt64(0), dataReader.GetString(1),
                dataReader.GetString(2), dataReader.GetInt32(3), dataReader.GetString(4),
                dataReader.GetInt64(5));
            return usuario;
        }
        public override List<object> CreateObjects(SqlDataReader dataReader)
        {
            List<object> usuarios = new List<object>();
            while (dataReader.Read())
            {
                usuarios.Add(new Usuario(dataReader.GetInt64(0), dataReader.GetString(1),
                dataReader.GetString(2), dataReader.GetInt32(3), dataReader.GetString(4),
                dataReader.GetInt64(5)));
            }
            return usuarios;
        }
        public override Dictionary<string, object> GetKeyValuePairs()
        {
            return new Dictionary<string, object>()
            {
                { "ID_USUARIO", Id },
                {"NOMBRE", Nombre },
                {"APELLIDO", Apellido },
                {"EDAD", Edad },
                {"EMAIL", Mail },
                {"TELEFONO", Telefono }
            };
        }
    }
}
