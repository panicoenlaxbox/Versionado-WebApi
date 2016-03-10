using System.Collections.Generic;
using System.Web.Http;
using Versionado.Infrastructure;
using Versionado.Models;

namespace Versionado.Controllers
{
    public class CustomersV1Controller : ApiController
    {
        [VersionedRoute("Customers", Version.Previous)]
        public IEnumerable<Customer1> Get()
        {
            var customers = new List<Customer1>()
            {
                new Customer1() { Id = 1, Name = "Customer 1"},
                new Customer1() { Id = 2, Name = "Customer 2"}
            };
            return customers;
        }
    }
}