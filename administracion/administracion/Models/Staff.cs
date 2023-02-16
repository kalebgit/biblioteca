using administracion.Handler;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace administracion.Models
{
    internal class Staff : Handler<Staff>
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

        public override string GetTabla()
        {
            return "STAFF";
        }
        public override object CreateObject(SqlDataReader dataReader)
        {
            Staff staff = new Staff(dataReader.GetInt64(0), dataReader.GetString(1), 
                dataReader.GetString(2), dataReader.GetString(3));
            return staff;
        }
        public override Dictionary<string, object> GetKeyValuePairs()
        {
            return new Dictionary<string, object>()
            {
                { "ID_STAFF", Id },
                {"NOMBRE", Nombre },
                {"APELLIDO", Apellido },
                {"TURNO", Turno }
            };
        }
    }
}
