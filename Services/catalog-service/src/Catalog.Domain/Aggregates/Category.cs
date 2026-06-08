using CatalogService.Domain.Aggregates;
using CatalogService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.Aggregates
{
     
    public sealed class Category : AggregateRoot<Guid>
    {
        public string Name { get; private set; } = default!;

        public string? Description { get; private set; }

        private readonly List<Product> _products = [];

        public IReadOnlyCollection<Product> Products =>
            _products.AsReadOnly();

        private Category()
        {
        }

        public Category(
            Guid id,
            string name,
            string? description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
