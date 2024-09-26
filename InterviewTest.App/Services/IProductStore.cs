using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterviewTest.App.Models;

namespace InterviewTest.App.Services
{
    public interface IProductStore
    {
        IEnumerable<IProduct> GetProducts();
        void ap(IProduct product);
        void rp(Guid productId);

        //Let's assume we cannot update a product
        event Action<IProduct> ProductAdded;
        event Action<Guid> ProductRemoved;
    }
}
