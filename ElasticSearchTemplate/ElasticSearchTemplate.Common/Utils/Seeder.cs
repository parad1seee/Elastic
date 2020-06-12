using ElasticSearchTemplate.Domain.Entities;
using Nest;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ElasticSearchTemplate.Common.Utils
{
    public class Seeder
    {
        private readonly IElasticClient _elasticClient;

        public Seeder(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        
    }
}
