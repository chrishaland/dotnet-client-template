using NUnit.Framework;
using Repository.Models;
using Service.Brands;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tests.IntegrationTests
{
    [TestFixture]
    public class Brand_query_handler_tests
    {
        [Test]
        public async Task Get_brand()
        {
            var brand = SUT.Database.Brands.Add(new Brand { Name = "TESTLA" });
            await SUT.Database.SaveChangesAsync();

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/brand/query");
            var data = new BrandQueryRequest { Id = brand.Entity.Id };
            var (response, content) = await SUT.SendHttpRequest<BrandQueryResponse>(request, data);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(content.Brand.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(content.Brand.Id, Is.EqualTo(brand.Entity.Id));
                Assert.That(content.Brand.Name, Is.EqualTo(brand.Entity.Name));
            });
        }

        [TestCase("00000000-0000-0000-0000-000000000000")]
        [TestCase("7ea12409-aaa3-4e2d-937d-8fcf4103dbd8")]
        public async Task Invalid_brand_id_returns_not_found(string guid)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/brand/query");
            var data = new BrandQueryRequest { Id = Guid.Parse(guid) };
            var (response, _) = await SUT.SendHttpRequest<BrandQueryResponse>(request, data);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
