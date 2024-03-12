using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using NAUCountryIdeaHub.Configuration;
using NAUCountryIdeaHub.Entities;
using static Dapper.SqlMapper;

namespace NAUCountryIdeaHub.Repositories
{
    public class IdeaHubRepository : IIdeaHubRepository
    {
        //------------------------------------------------EXAMPLE CODE---------------------------------------------------------------
        ///Private Members not really needed
        public string ConnectionString { get; set; }



        ///Constructor
        //public IdeaHubRepository(IOptions<ConnectionStringsConfig> connectionString) => ConnectionString = connectionString.Value.DefaultConnection;
        public IdeaHubRepository(IConfiguration config)
        {
            ConnectionString = config.GetConnectionString("DefaultConnection");
        }


        public async Task<IEnumerable<RequestEntity>> GetIdeasAsync()
        {
            try
            {
                //var connectionString = _connectionString;

                var connection = new SqlConnection(ConnectionString);
                await connection.OpenAsync();

                var ideas = await connection.QueryAsync<RequestEntity>(SqlCommands.GetIdeas);

                //return our list of ideas from db
                return ideas;

            }
            catch (Exception ex)
            {
                //can help with debugging if run into errors/exceptions
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<RequestEntity>> GetCompletedIdeasAsync()
        {
            try
            {
                //var connectionString = _connectionString;

                var connection = new SqlConnection(ConnectionString);
                await connection.OpenAsync();

                var ideas = await connection.QueryAsync<RequestEntity>(SqlCommands.GetCompletedIdeas);

                //return our list of ideas from db
                return ideas;

            }
            catch (Exception ex)
            {
                //can help with debugging if run into errors/exceptions
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<UserEntity>> GetUsersAsync()
        {
            try
            {
                //var connectionString = _connectionString;

                var connection = new SqlConnection(ConnectionString);
                await connection.OpenAsync();

                var users = await connection.QueryAsync<UserEntity>(SqlCommands.GetUsers);

                //return our list of ideas from db
                return users;

            }
            catch (Exception ex)
            {
                //can help with debugging if run into errors/exceptions
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private static class SqlCommands
        {
            public static readonly string GetIdeas =
                @"SELECT
                Name,
                Type,
                Closed,
                Description,
                Resolution,
                DateTimeSubmitted
            FROM [dbo].[Request]";

            public static readonly string GetCompletedIdeas =
               @"SELECT
                Name,
                Type,
                Closed,
                Description,
                Resolution,
                DateTimeSubmitted
            FROM [dbo].[Request] WHERE Closed = 1";

            public static readonly string GetUsers =
                @"SELECT
                 FirstName,
                 LastName,
                 Email,
                 Password,
                 Active,
                 IsRequestAdmin,
                 IsITAdmin,
                 ReceiveEmailNotifications
            FROM [dbo].[User]";
        }
        //-----------------------------------------------END EXAMPLE CODE-------------------------------------------------------------
    }
}
