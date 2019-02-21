using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Socialite.Domain.AggregateModels.PostAggregate;
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

        public async Task<IEnumerable<PostViewModel>> FindAllAsync()
        {
            using(var connection = _dbConnectionFactory.CreateConnection())
            {
                string findAllQuery = @"
                    SELECT Posts.Id, Posts.Text, PostStates.Name as State, Posts.CreatedAt
                    FROM Posts
                    INNER JOIN PostStates
                    ON Posts.StateId = PostStates.Id;";

                return await connection.QueryAsync<PostViewModel>(findAllQuery);
            }
        }

        public async Task<PostViewModel> FindAsync(int id)
        {
            using(var connection = _dbConnectionFactory.CreateConnection())
            {
                string findByIdQuery = @"
                    SELECT Posts.Id, Posts.Text, PostStates.Name as State, Posts.CreatedAt
                    FROM Posts
                    INNER JOIN PostState
                    ON Posts.StateId = PostState.Id
                    WHERE Posts.Id = @id";

                var result = await connection.QueryAsync<PostViewModel>(findByIdQuery);

                if(result == null)
                {
                    throw new KeyNotFoundException();
                }

                return result.First();
            }
        }
    }
}