using administracion.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace administracion.Handler
{
    public class StaffHandler : Handler<Staff>
    {
        public override string GetTabla()
        {
            return "STAFF";
        }
        public override Staff CreateObject(SqlDataReader dataReader)
        {
            Staff staff = new Staff(dataReader.GetInt64(0), dataReader.GetString(1),
                dataReader.GetString(2), dataReader.GetString(3));
            return staff;
        }
        public override List<Staff> CreateObjects(SqlDataReader dataReader)
        {
            List<Staff> staffs = new List<Staff>();
            while (dataReader.Read())
            {
                staffs.Add(new Staff(dataReader.GetInt64(0), dataReader.GetString(1),
                dataReader.GetString(2), dataReader.GetString(3)));
            }
            return staffs;
        }
        public override Dictionary<string, object> GetKeyValuePairs(Staff staff)
        {
            return new Dictionary<string, object>()
            {
                { "ID_STAFF", staff.Id },
                {"NOMBRE", staff.Nombre },
                {"APELLIDO", staff.Apellido },
                {"TURNO", staff.Turno }
            };
        }

        public string ImprimirStaffs(List<Staff> staffs)
        {
            string texto = String.Format("=================== STAFFS ======================\n" +
                "{0,-25}{1,-30}{2,-30}{3,-25}\n", "ID", "NOMBRE", 
                "APELLIDO", "TURNO");
            foreach(Staff staff in staffs)
            {
                texto += staff + "\n";
            }

            return texto;
        }
    }
}
