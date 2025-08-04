using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace MY_E_COMMERCE.DataAccess
{

    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();

    }
    public class SqlConnectionFactory : ISqlConnectionFactory
    {

        private readonly IConfiguration _config;
        public SqlConnectionFactory(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection CreateConnection() {

            return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        }






    }
}
