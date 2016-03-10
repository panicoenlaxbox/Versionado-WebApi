using System.Collections.Generic;
using System.Web.Http;
using Versionado.Infrastructure;
using Versionado.Models;

namespace Versionado.Controllers
{
    public class OrdersController : ApiController
    {
        [VersionedRoute("Orders", Version.Current)]
        public IEnumerable<Order> Get()
        {
            var customers = new List<Order>()
            {
                new Order() { Id = 1, Name = "Customer 1"},
                new Order() { Id = 2, Name = "Customer 2"}
            };
            return customers;
        }
    }
}