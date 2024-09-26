using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterviewTest.App.Models;

namespace InterviewTest.App.Services
{
    public interface IProductAvailabilityChecker
    {
        public Task<ProductAvailability> IsProductAvailableAsync(IProduct product);
    }
}
