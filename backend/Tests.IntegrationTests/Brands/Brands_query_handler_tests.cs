using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Repository.Models;
using Service.Brands;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tests.IntegrationTests
{
    [TestFixture]
    public class Brands_query_handler_tests
    {
        [Test]
        public async Task Should_get_all_brands_from_the_database()
        {
            var brand1 = SUT.Database.Brands.Add(new Brand { Name = "TESTLA" });
            var brand2 = SUT.Database.Brands.Add(new Brand { Name = "BMVV" });
            await SUT.Database.SaveChangesAsync();

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/brands/query");
            var (response, content) = await SUT.SendHttpRequest<BrandsQueryResponse>(request, new { });

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

                var dto1 = content.Brands.Single(b => b.Id == brand1.Entity.Id);
                Assert.That(dto1.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(dto1.Id, Is.EqualTo(brand1.Entity.Id));
                Assert.That(dto1.Name, Is.EqualTo(brand1.Entity.Name));

                var dto2 = content.Brands.Single(b => b.Id == brand2.Entity.Id);
                Assert.That(dto2.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(dto2.Id, Is.EqualTo(brand2.Entity.Id));
                Assert.That(dto2.Name, Is.EqualTo(brand2.Entity.Name));
            });
        }

        [Test]
        public async Task Should_return_empty_list_when_no_brands_in_database()
        {
            await SUT.Database.DeleteAllAsync<Brand>();
            
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/brands/query");
            var (response, content) = await SUT.SendHttpRequest<BrandsQueryResponse>(request, new { });

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(content.Brands, Is.Not.Null);
                CollectionAssert.IsEmpty(content.Brands);
            });
        }
    }
}
