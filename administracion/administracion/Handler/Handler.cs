using administracion.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace administracion.Handler
{
    public abstract class Handler<T>
    {
        public delegate void Actualizar(T obj);

        protected string _connectionString = "Data Source=LAPTOP-VQVR3Q8R;Initial Catalog=Biblioteca;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // =========== METODOS CRUD ===============

        // metodos CREATE
        public virtual void Create(T obj)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string valores = "";

                for (int i = 0; i < GetKeyValuePairs(obj).Count; i++)
                {
                    valores += i == GetKeyValuePairs(obj).Count - 1 ?
                        $"{GetKeyValuePairs(obj).ElementAt(i).Value}" :
                        $"{GetKeyValuePairs(obj).ElementAt(i).Value},";
                }

                SqlCommand command = new SqlCommand($"INSERTO INTO {GetTabla()}" +
                    $" VALUES ({valores})", connection);

                command.ExecuteNonQuery();
            }
        }
        public virtual void Create(List<T> lista)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string valores = "";
                for (int y = 0; y < lista.Count; y++)
                {
                    T obj = lista[y];
                    valores += "(";
                    for (int i = 0; i < GetKeyValuePairs(obj).Count; i++)
                    {
                        valores += i == GetKeyValuePairs(obj).Count - 1 ?
                            $"{GetKeyValuePairs(obj).ElementAt(i).Value}" :
                            $"{GetKeyValuePairs(obj).ElementAt(i).Value},";
                    }
                    valores += y == lista.Count - 1 ? ") " : "), ";
                }


                SqlCommand command = new SqlCommand($"INSERTO INTO {GetTabla()}" +
                    $" VALUES {valores}", connection);

                command.ExecuteNonQuery();
            }
        }

        // metodos READ
        public T Get(long id)
        {
            T obj;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM {GetTabla()}" +
                    $" WHERE ID_{GetTabla()} = {id}", connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            obj = CreateObject(dataReader);
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
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM {GetTabla()}", connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        list = CreateObjects(dataReader);
                        return list;
                    }
                }
            }
            return default(List<T>);
        }

        // metodo UPDATE
        public void Update(T obj)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string camposValores = "";

                foreach(KeyValuePair<string, object> keyValue in GetKeyValuePairs(obj))
                {
                    camposValores += $"{keyValue.Key} = " + keyValue.Value is string ?
                        $"'{keyValue.Value}'" : $"{keyValue.Value}";
                }

                SqlCommand command = new SqlCommand($"UPDATE {GetTabla} SET " +
                    camposValores + $" WHERE ID_{GetTabla()} = " +
                    $"{GetKeyValuePairs(obj).ElementAt(0)}", connection);

            }
        }
        
        // metodo DELETE
        public void Delete(long id)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"DELETE FROM {GetTabla()} " +
                    $"WHERE ID_{GetTabla()} = {id}", connection);
                command.ExecuteNonQuery();
            }
        }


        // ============= metodos ABSTRACTOS ================
        public abstract string GetTabla();
        public abstract T CreateObject(SqlDataReader dataReader);
        public abstract List<T> CreateObjects(SqlDataReader dataReader);
        public abstract Dictionary<string, object> GetKeyValuePairs(T obj);
        
    }
}
