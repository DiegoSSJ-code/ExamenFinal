using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ExamenFinal.Clases
{
    class ClsConexionSQL
    {
        public SqlConnection conexion;
        private String _conexion { get; }

        public ClsConexionSQL()
        {

            _conexion = "Data Source=DESKTOP-0FKOV9I\\SQLEXPRESS;Initial Catalog=Animes;Integrated Security=True";

        }


        public void abrirconexion()
        {
            conexion = new SqlConnection(_conexion);
            conexion.Open();
        }
        public void cerrarconexion()
        {
            conexion.Close();
        }
        public DataTable consultaTablaDirecta(String sqll)
        {
            abrirconexion();
            SqlDataReader dr;
            SqlCommand comm = new SqlCommand(sqll, conexion);
            dr = comm.ExecuteReader();

            var dataTable = new DataTable();
            dataTable.Load(dr);
            cerrarconexion();
            return dataTable;
        }
        public void EjecutaSQLDirecto(String sqll)
        {
            abrirconexion();
            try
            {

                SqlCommand comm = new SqlCommand(sqll, conexion);
                comm.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                cerrarconexion();
            }
        }
    }
}
