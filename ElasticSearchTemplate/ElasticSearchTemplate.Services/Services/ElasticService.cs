using ElasticSearchTemplate.Domain;
using ElasticSearchTemplate.Domain.Entities;
using ElasticSearchTemplate.Services.Interfaces;
using Nest;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchTemplate.Services.Services
{
    public class ElasticService : IElasticService
    {
        private readonly IElasticClient _elasticClient;

        public ElasticService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<Book> CreateBook(Book book)
        {
            await _elasticClient.IndexAsync(book, i => i.Index("test-domain"));

            return book;
        }

        public async Task<Book> GetBookById(int id)
        {
            var book = await _elasticClient.GetAsync<Book>(id);

            return book.Source;
        }

        public async Task<Typography> CreateTypography(Typography typography)
        {
            var result = await _elasticClient.IndexDocumentAsync(typography);

            return typography;
        }

        public async Task<Typography> GetTypographyById(int id)
        {
            var typography = await _elasticClient.GetAsync<Typography>(id);

            return typography.Source;
        }

        public async Task<List<Book>> SearchIndex(string pattern)
        {
            var result = await _elasticClient.SearchAsync<Book>(x => x
                 .AllIndices()
                 .From(0)
                 .Size(10)
                 .Query(q => q
                     .Match(m => m
                        .Field(x => x.Name)
                        .Query(pattern)
                     )
                 )
            );

            return result.Documents.ToList();
        }

        public async Task<List<IndexName>> GetAllIndexes()
        {
            var result = await _elasticClient.Indices.GetAsync(new GetIndexRequest(Indices.All));

            return result.Indices.Keys.ToList();
        }

        public async Task CreateIndex(string indexName)
        {
            _elasticClient.Indices.Create(indexName);
        }

        public async Task<int> GetCount()
        {
            var result = await _elasticClient.CountAsync<Typography>(selector: x => x.AllIndices());

            return (int)result.Count;
        }

        public void Seed()
        {
            Parallel.Invoke(
                    () => CreateBook("new-index", 1),
                    () => CreateTypography("test-domain", 1),
                    () => CreateBook("new-index", 100000),
                    () => CreateTypography("test-domain", 100000),
                    () => CreateTypography("new-index", 200000),
                    () => CreateTypography("test-domain", 300000),
                    () => CreateBook("new-index", 200000),
                    () => CreateBook("new-index", 300000),
                    () => CreateTypography("test-domain", 400000),
                    () => CreateBook("new-index", 400000),
                    () => CreateTypography("new-index", 500000),
                    () => CreateBook("new-index", 500000),
                    () => CreateTypography("test-domain", 600000)
                );
        }

        private void CreateBook(string indexName, int startId)
        {
            for (var i = startId; i < startId + 100000; i++)
            {
                _elasticClient.Index(new Book
                {
                    Id = i,
                    Author = "Danil",
                    Genres = new List<string> { "Genres" },
                    Name = "Amet justo donec enim diam vulputate ut. Libero nunc consequat interdum varius sit amet. Lorem ipsum dolor sit amet consectetur. Volutpat lacus laoreet non curabitur gravida arcu ac tortor. Facilisi etiam dignissim diam quis enim lobortis scelerisque. Et netus et malesuada fames. Scelerisque in dictum non consectetur a erat nam at. Eros in cursus turpis massa tincidunt dui. Adipiscing vitae proin sagittis nisl rhoncus mattis. Diam quis enim lobortis scelerisque fermentum dui faucibus in ornare. Libero id faucibus nisl tincidunt. Sapien pellentesque habitant morbi tristique senectus et netus et. Vitae sapien pellentesque habitant morbi tristique senectus et netus et. Placerat in egestas erat imperdiet sed euismod nisi. Sed viverra tellus in hac habitasse. Sagittis aliquam malesuada bibendum arcu. Imperdiet sed euismod nisi porta lorem mollis aliquam ut porttitor. Ac orci phasellus egestas tellus rutrum."
                }, indexRequest => indexRequest.Index(indexName));
            }

        }

        private void CreateTypography(string indexName, int startId)
        {
            for (var i = startId; i < startId + 100000; i++)
            {
                _elasticClient.Index(new Typography
                {
                    Id = i,
                    Address = "Address",
                    Name = "Nunc id cursus metus aliquam eleifend mi in. Massa tincidunt dui ut ornare lectus sit amet est placerat. Vestibulum lectus mauris ultrices eros in cursus turpis. Quam viverra orci sagittis eu volutpat odio facilisis mauris. Libero nunc consequat interdum varius sit amet. Ipsum nunc aliquet bibendum enim facilisis. Augue lacus viverra vitae congue eu consequat ac felis. Facilisis sed odio morbi quis commodo odio. Luctus accumsan tortor posuere ac ut. Non pulvinar neque laoreet suspendisse interdum consectetur libero id. Maecenas ultricies mi eget mauris pharetra et ultrices neque. Laoreet sit amet cursus sit amet dictum sit."
                }, indexRequest => indexRequest.Index(indexName));
            }
        }
    }
}
