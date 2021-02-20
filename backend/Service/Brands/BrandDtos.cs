using System;

namespace Service.Brands
{
    public sealed record BrandDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public BrandDto(Guid id, string name) => (Id, Name) = (id, name);
    }

    public sealed record BrandCommandRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public BrandCommandRequest(Guid? id, string name) => (Id, Name) = (id, name);
    }

    public sealed class BrandQueryRequest
    {
        public Guid Id { get; set; }
    }

    public sealed class BrandQueryResponse
    {
        public BrandDto Brand { get; set; } = new BrandDto(Guid.Empty, string.Empty);
    }

    public sealed class BrandsQueryRequest { }
    public sealed class BrandsQueryResponse 
    {
        public BrandDto[] Brands { get; set; } = Array.Empty<BrandDto>();
    }

    
}
