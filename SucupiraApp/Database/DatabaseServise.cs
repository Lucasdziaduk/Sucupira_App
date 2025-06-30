using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace SucupiraApp.Database
{
    public class DatabaseService
    {
        private readonly string connectionString;

        public DatabaseService()
        {
 
            connectionString = "Host=ep-dawn-boat-a8m31qhw-pooler.eastus2.azure.neon.tech;" +
                               "Database=neondb;" +
                               "Username=neondb_owner;" +
                               "Password=npg_MAxRqW9vjc2C;" +
                               "SSL Mode=Require;" +
                               "Trust Server Certificate=true;";
        }

        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connectionString);
        }
    }
}
