using System.Collections.Generic;

namespace ElasticSearchTemplate.Domain.Entities
{
    public class Book : BaseEntity, IEntity
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public List<string> Genres { get; set; }

        public double Price { get; set; }
    }
}
