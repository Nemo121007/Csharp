using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

//ssh -L 1433:192.168.112.103:1433 -N -T gashkov@kappa.cs.petrsu.ru

namespace Database
{
    internal class Connection
    {
        static string connectionString = "Server=127.0.0.1;Database=db22205;TrustServerCertificate=true;User Id=User050;Password=User050][7";

        public static void Start()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Console.WriteLine("Connect");

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction = connection.BeginTransaction();

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "Select tblFlat.txtFlatAddress, tblOwner.txtOwnerName, tblOwner.txtOwnerSecondName, tblOwner.txtOwnerSurname,\r\n\t\ttblFlat.intStorey, tblFlat.fltArea, tblFlat.intCount\r\nFrom tblFlat inner join tblOwner on tblFlat.intOwnerId = tblOwner.intOwnerId";
                    command.Connection = connection;

                    SqlDataReader reader = command.ExecuteReader();

                    List<string> data = new List<string>();

                    while (reader.Read())
                    {

                        var s = "";
                        s = reader["txtFlatAddress"].ToString() + reader["txtOwnerName"].ToString() + reader["txtOwnerSecondName"].ToString() +
                            reader["txtOwnerSurname"].ToString() + " " + reader["intStorey"].ToString() + " " + reader["fltArea"].ToString() + " " + reader["intCount"].ToString();
                        data.Add(s);
                    }

                    foreach (var d in data)
                        Console.WriteLine(d);
                }
                catch
                {
                    Console.WriteLine("Error");
                }
            }
        }
    }
}
