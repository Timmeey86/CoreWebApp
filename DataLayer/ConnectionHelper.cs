using Microsoft.Extensions.Configuration;

namespace DataLayer
{
    /// <summary>
    /// This class is responsible for the making connecting to databases easier
    /// </summary>
    public static class ConnectionHelper
    {
        public static string ConnectionString(IConfiguration config, string name = "Default")
        {
            return config.GetConnectionString(name);
        }
    }
}
