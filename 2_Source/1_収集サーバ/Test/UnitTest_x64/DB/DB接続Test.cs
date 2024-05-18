using System;
using System.Data.SqlClient;

namespace test
{
    public class TestClass
    {
        public void Test_DB接続()
        {
            using (var connection = new SqlConnection("Server=IP;Initial Catalog=DB;Persist Security Info=true;"))
            {
                var command = new SqlCommand("SELECT TOP 10 Id, Name, Price FROM Products", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[0]}:{reader[1]} ${reader[2]}");
                     }
                 }
            }
        }
    }
}
