using Dapper;
using MY_E_COMMERCE.DataAccess;
using System.Data;
using Microsoft.Data;

namespace MY_E_COMMERCE.Repositories
{
    public class TestRepository
    {

        private readonly ISqlConnectionFactory _connectionFactory;

        public TestRepository (ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        //public async Task<bool> TestConnectionAsync()
        //{

        //    try
        //    {
        //        using var connection = _connectionFactory.CreateConnection();
        //        var result = await connection.ExecuteScalarAsync<int>("select 1");
        //        return result == 1;
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine("Db connection failed " + ex.Message);
        //        return false;
        //    }

        //}

        public bool TestConnection()
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();
                connection.Open();
                var result = connection.ExecuteScalar<int>("select 1");
                return result == 1;
            }
            catch (Exception ex)
            {

                Console.WriteLine("db error : " + ex.Message);
                throw;
            }
        }

        

            
    }
}
