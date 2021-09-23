using AnimalAdoption.Common.Domain;
using AnimalAdoption.Common.Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using Xunit;

namespace AnimalAdoption.Web.Portal.UnitTests
{
    public class CartLogicTest
    {
        private DbContextOptions<AnimalAdoptionContext> _options = new DbContextOptionsBuilder<AnimalAdoptionContext>()
                     .UseInMemoryDatabase(databaseName: "AnimalDatabase")
                     .Options;

        [Fact]
        public void CartManagement_EmptyCartAddAnimal_AnAnimalIsAdded()
        {
            using (var context = new AnimalAdoptionContext(_options))
            {
                DbInitializer.Initialize(context);
                var animalId = 1;
                var quantityAmount = 1;

                var memoryCache = new MemoryCache(new MemoryCacheOptions());
                var animalService = new AnimalService(context);
                var resultingCart = new CartService(memoryCache, animalService).SetAnimalQuantity("TEST_CART", animalId, quantityAmount);

                Assert.Equal("TEST_CART", resultingCart.Id);
                Assert.Equal(1, resultingCart.CartContents.First(x => x.Id == animalId).Quantity);
            }
        }

        [Fact]
        public void CartManagement_EmptyCartAddNegativeAnimal_AnAnimalDoesNotGoIntoNegative()
        {
            using (var context = new AnimalAdoptionContext(_options))
            {
                DbInitializer.Initialize(context);
                var animalId = 1;
                var quantityAmount = -1;

                var memoryCache = new MemoryCache(new MemoryCacheOptions());
                var animalService = new AnimalService(context);
                var resultingCart = new CartService(memoryCache, animalService).SetAnimalQuantity("TEST_CART", animalId, quantityAmount);

                Assert.Equal("TEST_CART", resultingCart.Id);
                Assert.Equal(0, resultingCart.CartContents.First(x => x.Id == animalId).Quantity);
            }
        }
    }
}
