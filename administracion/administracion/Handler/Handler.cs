using administracion.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace administracion.Handler
{
    public abstract class Handler<T>
    {
        private string _connectionString = "Data Source=LAPTOP-VQVR3Q8R;Initial Catalog=Biblioteca;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public T Get(long id)
        {
            T obj;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM {GetTabla()}" +
                    $" WHERE ID_{GetTabla()} = {id}", connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            obj = (T)CreateObject(dataReader);
                            return obj;
                        }
                    }
                }
            }
            return default(T);
        }
        public List<T> GetAll()
        {
            List<T> list = new List<T>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM {GetTabla()}", connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            list = CreateObjects(dataReader);
                        }
                        return list;
                    }
                }
            }
            return default(T);
        }
        public void Update(T obj)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string camposValores = "";

                foreach(KeyValuePair<string, object> keyValue in GetKeyValuePairs())
                {
                    camposValores += $"{keyValue.Key} = " + keyValue.Value is string ?
                        $"'{keyValue.Value}'" : $"{keyValue.Value}";
                }

                SqlCommand command = new SqlCommand($"UPDATE {GetTabla} SET " +
                    camposValores + $" WHERE ID_{GetTabla()} = " +
                    $"{GetKeyValuePairs().ElementAt(0)}", connection);

            }
        }
        public void Create(T obj)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string valores = "";

                for(int i = 0; i < GetKeyValuePairs().Count; i++)
                {
                    valores += i == GetKeyValuePairs().Count - 1 ?
                        $"{GetKeyValuePairs().ElementAt(i).Value}" :
                        $"{GetKeyValuePairs().ElementAt(i).Value},";
                }

                SqlCommand command = new SqlCommand($"INSERTO INTO {GetTabla()}" +
                    $" VALUES ({valores})", connection);

                command.ExecuteNonQuery();
            }
        }
        public void Delete(long id)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($"DELETE FROM {GetTabla()} " +
                    $"WHERE ID_{GetTabla()} = {GetKeyValuePairs().ElementAt(0)}");
            }
        }

        public abstract string GetTabla();

        // en vez de object poner T
        public abstract object CreateObject(SqlDataReader dataReader);
        public abstract List<object> CreateObjects(SqlDataReader dataReader);
        public abstract Dictionary<string, object> GetKeyValuePairs();
        
    }
}
