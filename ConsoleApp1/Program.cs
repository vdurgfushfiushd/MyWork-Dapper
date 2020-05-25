using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
            using (IDbConnection conn = new MySqlConnection(connstr))
            {
                conn.Open();
                string sql = "update t_users set Password=@Password where Id=@Id";
                var result = conn.Execute(sql, new User{ Id = "8fc66f2670774221a5a5886f8bb81dae", Password = "xxxx" });
                if (result >= 1)
                {
                    Console.WriteLine("update success");
                }
                else
                {
                    Console.WriteLine("update fail");
                }
            }
            Console.ReadKey();
        }
    }
    class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
