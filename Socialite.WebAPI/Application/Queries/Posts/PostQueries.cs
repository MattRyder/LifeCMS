using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Socialite.Infrastructure.DTO;
using Socialite.WebAPI.Queries.Posts;

namespace Socialite.WebAPI.Application.Queries.Posts
{
    public class PostQueries : IPostQueries
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public PostQueries(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<PostDTO>> FindAllAsync()
        {
            using(var connection = _dbConnectionFactory.CreateConnection())
            {
                string findAllQuery = @"
                    SELECT Posts.Id, Posts.Text, PostState.Name, Posts.CreatedAt
                    FROM Posts
                    INNER JOIN PostState
                    ON Posts.StateId = PostState.Id;";

                return await connection.QueryAsync<PostDTO>(findAllQuery);
            }
        }

        public async Task<PostDTO> FindAsync(int id)
        {
            using(var connection = _dbConnectionFactory.CreateConnection())
            {
                string findByIdQuery = @"
                    SELECT Posts.Id, Posts.Text, PostState.Name, Posts.CreatedAt
                    FROM Posts
                    INNER JOIN PostState
                    ON Posts.StateId = PostState.Id
                    WHERE Posts.Id = @id";

                var result = await connection.QueryAsync<PostDTO>(findByIdQuery);

                if(result == null)
                {
                    throw new KeyNotFoundException();
                }

                return result.First();
            }
        }
    }
}