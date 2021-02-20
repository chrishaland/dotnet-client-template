using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Brands
{
    [Route("api/brand/query")]
    public class BrandQueryHandler : QueryHandlerBase<BrandQueryRequest, BrandQueryResponse>
    {
        private readonly Database _database;

        public BrandQueryHandler(Database database)
        {
            _database = database;
        }

        public override async Task<ActionResult<BrandQueryResponse>> Execute([FromBody] BrandQueryRequest request, CancellationToken ct)
        {
            var brand = await _database.Brands
                .SingleOrDefaultAsync(b => b.Id.Equals(request.Id));

            if (brand == null)
            {
                return NotFound();
            }
            
            return Ok(new BrandQueryResponse 
            { 
                Brand = new BrandDto(brand.Id, brand.Name) 
            });
        }
    }
}
