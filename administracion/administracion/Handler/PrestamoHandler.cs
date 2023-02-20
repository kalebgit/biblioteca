using administracion.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace administracion.Handler
{
    
    public class PrestamoHandler : Handler<Prestamo>
    {
        public event Actualizar ActualizacionAtributo;

        public override string GetTabla()
        {
            return "PRESTAMO";
        }
        public override Prestamo CreateObject(SqlDataReader dataReader)
        {
            Prestamo prestamo = new Prestamo(dataReader.GetInt64(0), dataReader.GetInt64(1),
                dataReader.GetInt64(2), dataReader.GetString(3), dataReader.GetString(4),
                dataReader.GetString(5));
            return prestamo;
        }
        public override List<Prestamo> CreateObjects(SqlDataReader dataReader)
        {
            List<Prestamo> prestamos = new List<Prestamo>();
            while (dataReader.Read())
            {
                prestamos.Add(new Prestamo(dataReader.GetInt64(0), dataReader.GetInt64(1),
                dataReader.GetInt64(2), Convert.ToString(dataReader.GetDateTime(3)), 
                Convert.ToString(dataReader.GetDateTime(4)), dataReader.GetString(5)));
            }
            return prestamos;
        }
        public override Dictionary<string, object> GetKeyValuePairs(Prestamo prestamo)
        {
            return new Dictionary<string, object>()
            {
                { "ID_USUARIO", prestamo.IdUsuario},
                {"ID_LIBRO", prestamo.IdLibro},
                {"ID_STAFF", prestamo.IdStaff },
                {"FECHA_PRESTAMO", prestamo.FechaPrestamo},
                {"FEHCA_DEVOLUCION", prestamo.FechaDevolucion},
                {"ESTATUS", prestamo.Estatus}
            };
        }

        public override void Create(Prestamo obj)
        {
            Actualizar delegado = new Actualizar(UpdateCantidad);
            ActualizacionAtributo += delegado;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string valores = "";

                for (int i = 0; i < GetKeyValuePairs(obj).Count; i++)
                {
                    valores += i == GetKeyValuePairs(obj).Count - 1 ?
                            (GetKeyValuePairs(obj).ElementAt(i).Value is string ?
                            $"'{GetKeyValuePairs(obj).ElementAt(i).Value}'" :
                        $"{GetKeyValuePairs(obj).ElementAt(i).Value}") :
                        (GetKeyValuePairs(obj).ElementAt(i).Value is string ?
                            $"'{GetKeyValuePairs(obj).ElementAt(i).Value}', " :
                            $"{GetKeyValuePairs(obj).ElementAt(i).Value}, ");
                }

                SqlCommand command = new SqlCommand($"INSERT INTO {GetTabla()}" +
                    $" VALUES ({valores})", connection);

                command.ExecuteNonQuery();
                ActualizacionAtributo(obj);
            }
        }

        public List<Prestamo> GetPrestamos(long id)
        {
            List<Prestamo> prestamos = new List<Prestamo>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM {GetTabla()} WHERE" +
                    $" ID_USUARIO = {id}", connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        prestamos = CreateObjects(dataReader);
                        return prestamos;
                    }
                }
            }
            return null;
        }

        public void UpdateCantidad(Prestamo prestamo)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand($"SELECT ID_LIBRO FROM LIBRO " +
                    $"WHERE ID_LIBRO = {prestamo.IdLibro}", connection);
                int cantidadActual = Convert.ToInt32(command1.ExecuteScalar());
                SqlCommand command = new SqlCommand($"UPDATE LIBRO SET CANTIDAD = " +
                    $"{cantidadActual - 1} WHERE ID_LIBRO = {prestamo.IdLibro}",
                connection);
                command.ExecuteNonQuery();
            }
        }
        public void ConsultarFecha(Prestamo prestamo)
        {
            int diaPrestamo, mesPrestamo, anioPrestamo, diaDevolucion, mesDevolucion,
                anioDevolucion;
            GetDatosFecha(prestamo.FechaPrestamo, out diaPrestamo, out mesPrestamo, 
                out anioPrestamo);
            GetDatosFecha(prestamo.FechaDevolucion, out diaDevolucion, out mesDevolucion,
                out anioDevolucion);
            if((anioDevolucion < anioPrestamo) ? true : (mesDevolucion < mesPrestamo) ? 
                true : (diaDevolucion < diaPrestamo) ? true : false)
            {
                prestamo.Estatus = "RETRASADO";
            }
            Update(prestamo);
            
        }

        public string ImprimirPrestamos(List<Prestamo> prestamos)
        {
            string texto = String.Format("=================== PRESTAMOS ======================\n" +
                "{0,-30}{1,-30}{2,-30}{3,-30}{4,-30}{5,-30}\n", "ID_USUARIO", "ID_LIBRO",
                "ID_STAFF", "FECHA_PRESTAMO", "FECHA_DEVOLUCION", "ESTATUS");
            try
            {
                foreach (Prestamo prestamo in prestamos)
                {
                    texto += prestamo + "\n";
                }
                return texto;
            }
            
            catch(System.NullReferenceException e)
            {
                return "\n %%% NO HAY PRESTAMOS %%%\n";
            }
        }
        public static string StringDerivado(string texto, int startIndex, int endIndex)
        {
            string derivado = "";
            for (int i = startIndex; i <= endIndex; i++)
            {
                derivado += texto[i];
            }
            return derivado;
        }
        public static void GetDatosFecha(string obj, out int dia, out int mes, 
            out int anio)
        {
            dia = 0;
            mes = 0;
            anio = 0;
            int progreso = 0;
            int flag = 0;
            for (int i = 0; i < obj.Length; i++)
            {
                if (i < obj.Length - 1)
                {
                    if (obj[i + 1] == '-' || obj[i + 1] == '/')
                    {
                        if (progreso == 0)
                        {
                            int.TryParse(StringDerivado(obj, flag, i), out mes);
                            flag = i + 2;
                            progreso++;
                        }
                        else if (progreso == 1)
                        {
                            int.TryParse(StringDerivado(obj, flag, i), out dia);
                            flag = i + 2;
                            progreso++;

                        }

                    }
                }
            }

            if (progreso == 2)
            {
                int.TryParse(StringDerivado(obj, flag, obj.Length - 1), out anio);
            }
        }
    }
}
