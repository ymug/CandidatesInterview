using System.Threading.Tasks;
using InterviewTest.App.Models;

namespace InterviewTest.App.Services
{
    public interface IProductAvailabilityChecker
    {
        public Task<ProductAvailability> IsProductAvailableAsync(IProduct product);
    }
}
