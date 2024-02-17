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
        public IdeaHubRepository(IOptions<ConnectionStringsConfig> connectionString) => ConnectionString = connectionString.Value.DefaultConnection;
        //public IdeaHubRepository(string connectionString)
        //{
        //    ConnectionString = connectionString;
        //}


        public async Task<IEnumerable<RequestEntity>> GetIdeasAsync()
        {
            try
            {
                var connectionString = "Data Source=localhost;Initial Catalog=NAU;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                var connection = new SqlConnection(connectionString);
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

        private static class SqlCommands
        {
            public static readonly string GetIdeas =
                @"SELECT
                Name,
                Type,
                Status,
                Description
            FROM [dbo].[Request]";
        }
        //-----------------------------------------------END EXAMPLE CODE-------------------------------------------------------------
    }
}
