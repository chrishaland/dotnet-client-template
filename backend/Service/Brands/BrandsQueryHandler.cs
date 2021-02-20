using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Brands
{
    [Route("api/brands/query")]
    public class BrandsQueryHandler : QueryHandlerBase<BrandsQueryRequest, BrandsQueryResponse>
    {
        private readonly Database _database;

        public BrandsQueryHandler(Database database)
        {
            _database = database;
        }

        public override async Task<ActionResult<BrandsQueryResponse>> Execute([FromBody] BrandsQueryRequest request, CancellationToken ct)
        {
            var entities = await _database.Brands
                .ToListAsync();

            var brands = entities
                .Select(b => new BrandDto(b.Id, b.Name))
                .ToArray();

            return Ok(new BrandsQueryResponse 
            { 
                Brands = brands
            });
        }
    }
}
