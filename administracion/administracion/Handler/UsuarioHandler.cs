using administracion.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace administracion.Handler
{
    public class UsuarioHandler : Handler<Usuario>
    {
        public override string GetTabla()
        {
            return "USUARIO";
        }
        public override Usuario CreateObject(SqlDataReader dataReader)
        {
            Usuario usuario = new Usuario(dataReader.GetInt64(0), dataReader.GetString(1),
                dataReader.GetString(2), dataReader.GetInt32(3), dataReader.GetString(4),
                dataReader.GetInt64(5));
            return usuario;
        }
        public override List<Usuario> CreateObjects(SqlDataReader dataReader)
        {
            List<Usuario> usuarios = new List<Usuario>();
            while (dataReader.Read())
            {
                usuarios.Add(new Usuario(dataReader.GetInt64(0), dataReader.GetString(1),
                dataReader.GetString(2), dataReader.GetInt32(3), dataReader.GetString(4),
                dataReader.GetInt64(5)));
            }
            return usuarios;
        }
        public override Dictionary<string, object> GetKeyValuePairs(Usuario usuario)
        {
            return new Dictionary<string, object>()
            {
                { "ID_USUARIO", usuario.Id },
                {"NOMBRE", usuario.Nombre },
                {"APELLIDO", usuario.Apellido },
                {"EDAD", usuario.Edad },
                {"EMAIL", usuario.Mail },
                {"TELEFONO", usuario.Telefono }
            };
        }

        public string ImprimirUsuarios(List<Usuario> usuarios)
        {
            string texto = String.Format("=================== USUARIOS ======================\n" +
                "{0,-25}{1,-30}{2,-30}{3,-10}{4,-30}{5,-30}\n", "ID", "NOMBRE",
                "APELLIDO", "EDAD", "EMAIL", "TELEFONO");
            try
            {
                foreach (Usuario usuario in usuarios)
                {
                    texto += usuario + "\n";
                }
                return texto;
            }
            catch (System.NullReferenceException e)
            {
                return "\n %%% NO HAY USUARIOS %%% \n";
            }
        }
    }
}
