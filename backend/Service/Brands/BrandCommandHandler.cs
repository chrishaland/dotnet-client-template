using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Brands
{
    [Route("api/brand/command")]
    public class BrandCommandHandler : CommandHandlerBase<BrandCommandRequest>
    {
        private readonly Database _database;

        public BrandCommandHandler(Database database)
        {
            _database = database;
        }

        public override async Task<ActionResult<CommandResponse>> Execute([FromBody] BrandCommandRequest request, CancellationToken ct)
        {
            Guid id = Guid.Empty;
            if (request.Id == null)
            {
                id = await CreateBrand(request);
            }
            else
            {
                id = await UpdateBrand(request);
            }

            var response = new CommandResponse(id);
            return Ok(response);
        }

        private async Task<Guid> CreateBrand(BrandCommandRequest request)
        {
            var entity = _database.Brands.Add(new Brand
            {
                Name = request.Name
            });
            
            await _database.SaveChangesAsync();
            return entity.Entity.Id;
        }

        private async Task<Guid> UpdateBrand(BrandCommandRequest request)
        {
            var entity = _database.Brands.Update(new Brand
            {
#pragma warning disable CS8629 // Nullable value type may be null.
                Id = (Guid)request.Id,
#pragma warning restore CS8629 // Nullable value type may be null.
                Name = request.Name
            });
            
            await _database.SaveChangesAsync();
            return entity.Entity.Id;
        }
    }
}
