using MySql.Data.MySqlClient;

namespace InventoryManagement
{
    internal class DatabaseConnection
    {
        
        private readonly string connectionString = "Server=127.0.0.1; Database= INVENTORY_MANAGEMENT; uid=root;pwd=root;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
