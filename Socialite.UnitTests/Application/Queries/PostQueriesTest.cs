using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.Dapper;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.Infrastructure.Data;
using Socialite.Infrastructure.DTO;
using Socialite.UnitTests.Factories;
using Socialite.WebAPI.Application.Queries.Posts;
using Socialite.WebAPI.Queries.Posts;
using Xunit;

namespace Socialite.UnitTests.Application.Queries
{
    public class PostQueriesTest
    {
        private readonly Mock<IDbConnection> _dbConnectionMock;
        private readonly Mock<IDbConnectionFactory> _dbConnectionFactoryMock;

        public PostQueriesTest()
        {
            _dbConnectionMock = new Mock<IDbConnection>();

            _dbConnectionFactoryMock = new Mock<IDbConnectionFactory>();
        }

        [Fact]
        public async void FindAllAsync_ReturnsEnumerable()
        {
            var list = PostFactory.CreateDTOList().ToList().ConvertAll<PostViewModel>(p => PostViewModel.FromModel(p));

            _dbConnectionMock.SetupDapperAsync(c => c.QueryAsync<PostViewModel>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(list);

            _dbConnectionFactoryMock.Setup(x => x.CreateConnection()).Returns(_dbConnectionMock.Object);

            var postQueries = new PostQueries(_dbConnectionFactoryMock.Object);

            var result = await postQueries.FindAllAsync();

            Assert.NotNull(result);

            Assert.Equal(list.AsList().Count, result.AsList().Count);

            Assert.True(list.Select(x => x.Id).SequenceEqual(result.Select(x => x.Id)));
        }

        [Fact]
        public async void FindAsync_ReturnsPostViewModel()
        {
            var viewModel = PostViewModel.FromModel(PostFactory.CreateDTO());

            IEnumerable<PostViewModel> viewModelList = new[] { viewModel };

             _dbConnectionMock.SetupDapperAsync(c => c.QueryAsync<PostViewModel>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(viewModelList);

            _dbConnectionFactoryMock.Setup(x => x.CreateConnection()).Returns(_dbConnectionMock.Object);

            var postQueries = new PostQueries(_dbConnectionFactoryMock.Object);

            var result = await postQueries.FindAsync(viewModel.Id);

            Assert.NotNull(result);

            Assert.Equal(viewModel, result);
        }
    }
}