using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using NAUIdeaHub.Configuration;
using NAUIdeaHub.Entities;
using NAUIdeaHub.Models;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Numerics;
using static Dapper.SqlMapper;
using static MudBlazor.Colors;

namespace NAUIdeaHub.Repositories
{
    public class IdeaHubRepository : IIdeaHubRepository
    {
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

        //public async Task<IEnumerable<RequestEntity>> GetCompletedIdeasAsync()
        //{
        //    try
        //    {
        //        //var connectionString = _connectionString;

        //        var connection = new SqlConnection(ConnectionString);
        //        await connection.OpenAsync();

        //        var ideas = await connection.QueryAsync<RequestEntity>(SqlCommands.GetCompletedIdeas);

        //        //return our list of ideas from db
        //        return ideas;

        //    }
        //    catch (Exception ex)
        //    {
        //        //can help with debugging if run into errors/exceptions
        //        Console.WriteLine(ex.Message);
        //        throw;
        //    }
        //}

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

        public async Task<IEnumerable<RequestActionsEntity>> GetActionsAsync(int pk)
        {
            try
            {
                //var connectionString = _connectionString;

                var connection = new SqlConnection(ConnectionString);
                await connection.OpenAsync();

                var requestActions = await connection.QueryAsync<RequestActionsEntity>(SqlCommands.GetActions + pk);

                //return our list of ideas from db
                return requestActions;

            }
            catch (Exception ex)
            {
                //can help with debugging if run into errors/exceptions
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<RequestActionsEntity>> GetAllActionsAsync()
        {
            try
            {
                //var connectionString = _connectionString;

                var connection = new SqlConnection(ConnectionString);
                await connection.OpenAsync();

                var allActions = await connection.QueryAsync<RequestActionsEntity>(SqlCommands.GetAllActions);

                //return our list of ideas from db
                return allActions;

            }
            catch (Exception ex)
            {
                //can help with debugging if run into errors/exceptions
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<RequestNoteEntity>> GetNotesAsync(int requestPK)
        {
            try
            {
                var connection = new SqlConnection(ConnectionString);
                await connection.OpenAsync();

                var requestNotes = await connection.QueryAsync<RequestNoteEntity>(SqlCommands.getNotes + requestPK);

                //return our list of comments from db
                return requestNotes;

            }
            catch (Exception ex)
            {
                //can help with debugging if run into errors/exceptions
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async void LikeIdea(int ideaPK, int userPK)
        {
            try
            {
                //var connectionString = _connectionString;

                var connection = new SqlConnection(ConnectionString);
                await connection.OpenAsync();

                var requestActions = await connection.QueryAsync<RequestActionsEntity>(String.Format(SqlCommands.createLike, userPK, ideaPK));
                // Uses the String class Format() method which will allow us to insert in the values needed for the query.

            }
            catch (Exception ex)
            {
                //can help with debugging if run into errors/exceptions
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async void AlterLike(int ideaPK, int userPK, int value)
        {
            try
            {
                //var connectionString = _connectionString;

                var connection = new SqlConnection(ConnectionString);
                await connection.OpenAsync();

                var requestActions = await connection.QueryAsync<RequestActionsEntity>(String.Format(SqlCommands.alterLike, value, userPK, ideaPK));
                // Uses the String class Format() method which will allow us to insert in the values needed for the query.
                // Value sets the like (UpVote in db). If the user already liked the idea and wants to remove the like, the value
                // would be set to 0. Vice versa for if the user didn't like it but wanted to.

            }
            catch (Exception ex)
            {
                //can help with debugging if run into errors/exceptions
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async void FavoriteIdea(int ideaPK, int userPK)
        {
            try
            {
                //var connectionString = _connectionString;

                var connection = new SqlConnection(ConnectionString);
                await connection.OpenAsync();

                var requestActions = await connection.QueryAsync<RequestActionsEntity>(String.Format(SqlCommands.createFavorite, userPK, ideaPK));
                // Uses the String class Format() method which will allow us to insert in the values needed for the query.

            }
            catch (Exception ex)
            {
                //can help with debugging if run into errors/exceptions
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async void AlterFavorite(int ideaPK, int userPK, int value)
        {
            try
            {
                //var connectionString = _connectionString;

                var connection = new SqlConnection(ConnectionString);
                await connection.OpenAsync();

                var requestActions = await connection.QueryAsync<RequestActionsEntity>(String.Format(SqlCommands.alterFavorite, value, userPK, ideaPK));
                // Uses the String class Format() method which will allow us to insert in the values needed for the query.
                // Value sets the like (UpVote in db). If the user already liked the idea and wants to remove the like, the value
                // would be set to 0. Vice versa for if the user didn't like it but wanted to.

            }
            catch (Exception ex)
            {
                //can help with debugging if run into errors/exceptions
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async void AddComment(int ideaPK, string comment, int userPK)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(null, connection);

                    command.CommandText = SqlCommands.addComment;
                    SqlParameter ideaParameter = new SqlParameter("@reqID", System.Data.SqlDbType.Int, 0);
                    SqlParameter descriptionParameter = new SqlParameter("@desc", System.Data.SqlDbType.NVarChar, 5000);
                    SqlParameter authorParameter = new SqlParameter("authID", System.Data.SqlDbType.Int, 0);
                    // Creates the parameters for the parameterized query

                    ideaParameter.Value = ideaPK;
                    descriptionParameter.Value = comment;
                    authorParameter.Value = userPK;
                    // Sets the values for these parameters

                    command.Parameters.Add(ideaParameter);
                    command.Parameters.Add(descriptionParameter);
                    command.Parameters.Add(authorParameter);
                    // Adds the parameters to the query in the objects correlated @ position

                    command.Prepare();
                    command.ExecuteNonQuery();
                    // Prepares and executes the query.
                }
            }
            catch (Exception ex)
            {
                //can help with debugging if run into errors/exceptions
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async void RemoveComment(int commentID)
        {
            try
            {
                var connection = new SqlConnection(ConnectionString);
                await connection.OpenAsync();

                var requestActions = await connection.QueryAsync<RequestActionsEntity>(SqlCommands.removeComment + commentID);
                // Uses the SQL query to remove a certain comment. We are appending on the comment's id to the end of the 
                // addComment query which will finish the WHERE statement that was built

            }
            catch (Exception ex)
            {
                //can help with debugging if run into errors/exceptions
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async void CloseIdea(int ideaID, string resolution)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(null, connection);

                    command.CommandText = SqlCommands.closeIdea;
                    SqlParameter resolutionParameter = new SqlParameter("@res", System.Data.SqlDbType.NVarChar, 5000);
                    SqlParameter idParameter = new SqlParameter("@id", System.Data.SqlDbType.Int, 0);
                    // Creates the parameters for the parameterized query

                    idParameter.Value = ideaID;
                    resolutionParameter.Value = resolution;
                    // Sets the values for these parameters

                    command.Parameters.Add(idParameter);
                    command.Parameters.Add(resolutionParameter);
                    // Adds the parameters to the query in the objects correlated @ position

                    command.Prepare();
                    command.ExecuteNonQuery();
                    // Prepares and executes the query.
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
         /*
         * Returns requests by the requesting user
         */
        public async Task<IEnumerable<RequestEntity>> GetRequestsByUserAsync(int userId)
        {
            try
            {
                var connection = new SqlConnection(ConnectionString);
                await connection.OpenAsync();
                var requests = await connection.QueryAsync<RequestEntity>(String.Format(SqlCommands.getRequestsByUser, userId));
                return requests;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private static class SqlCommands
        {
           
            /*public static readonly string GetIdeas =
                @"SELECT Request.*
                FROM Request
                LEFT JOIN RequestActions ON Request.RequestID = RequestActions.RequestID AND RequestActions.UserID = {0}
                ORDER BY RequestActions.Favorite DESC";*/


            public static readonly string GetIdeas =
                @"SELECT
                RequestID,
                Name,
                Type,
                Closed,
                Description,
                URL,
                Resolution,
                DateTimeSubmitted, 
                Requestor
                FROM [dbo].[Request]";
            
            //public static readonly string GetCompletedIdeas =
            //   @"SELECT
            //    Name,
            //    Type,
            //    Closed,
            //    Description,
            //    URL,
            //    Resolution,
            //    DateTimeSubmitted
            //    FROM [dbo].[Request] WHERE Closed = 1";

            public static readonly string GetUsers =
                @"SELECT
                 UserID,
                 FirstName,
                 LastName,
                 Email,
                 Password,
                 Active,
                 IsAdmin,
                 ReceiveEmailNotifications
                FROM [dbo].[User]";

            public static readonly string GetActions =
                @"SELECT
                 UserID,
                 RequestID,
                 UpVote,
                 Favorite
                FROM [dbo].[RequestActions]
                WHERE RequestID = ";

            public static readonly string GetAllActions =
                @"SELECT
                 UserID,
                 RequestID,
                 UpVote,
                 Favorite
                FROM [dbo].[RequestActions]";
                

            public static readonly string getNotes =
                @"SELECT *
                 FROM [dbo].[RequestNote]
                 WHERE RequestID = ";

            public static readonly string createLike =
                @"INSERT INTO RequestActions
                 (UserID, RequestID, UpVote, Favorite)
                 VALUES
                 ({0}, {1}, 1, 0)";

            public static readonly string alterLike =
                @"UPDATE RequestActions
                 SET UpVote = '{0}'
                 WHERE UserID = {1} AND RequestID = {2}";

            public static readonly string createFavorite =
                @"INSERT INTO RequestActions
                 (UserID, RequestID, UpVote, Favorite)
                 VALUES
                 ({0}, {1}, 0, 1)";

            public static readonly string alterFavorite =
                @"UPDATE RequestActions
                 SET Favorite = '{0}'
                 WHERE UserID = {1} AND RequestID = {2}";

            public static readonly string addComment =
                @"INSERT INTO RequestNote
                 (RequestID, Description, Author)
                 VALUES
                 (@reqID, @desc, @autID)";

            public static readonly string removeComment =
                @"DELETE FROM RequestNote
                 WHERE RequestNoteID = ";

            public static readonly string getRequestsByUser =
                @"SELECT *
                FROM [dbo].[Request]
                WHERE Requestor = {0}";

            public static readonly string closeIdea =
                @"UPDATE Request
                 SET Closed = 1, Resolution = @res
                 WHERE RequestID = @id";
        }
    }
}
