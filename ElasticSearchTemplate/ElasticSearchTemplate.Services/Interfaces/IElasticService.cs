using ElasticSearchTemplate.Domain;
using ElasticSearchTemplate.Domain.Entities;
using Nest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElasticSearchTemplate.Services.Interfaces
{
    public interface IElasticService
    {
        Task<Book> CreateBook(Book book);

        Task<Book> GetBookById(int id);

        Task<Typography> CreateTypography(Typography typography);

        Task<Typography> GetTypographyById(int id);

        Task<List<Book>> SearchIndex(string pattern);

        Task<List<IndexName>> GetAllIndexes();

        Task CreateIndex(string indexName);

        void Seed();

        Task<int> GetCount();
    }
}
