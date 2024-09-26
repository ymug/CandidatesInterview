using System;
using System.Threading.Tasks;
using InterviewTest.App.Models;

namespace InterviewTest.App.Services
{
    public class ProductAvailabilityChecker : IProductAvailabilityChecker
    {
        private static readonly Random Random = new Random();

        public async Task<ProductAvailability> IsProductAvailableAsync(IProduct product)
        {
            await Task.Delay(5000);
            return new ProductAvailability(product, Random.Next(0, 2) == 0);
        }
    }
}
