using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Versionado.Infrastructure;
using Versionado.Models;
using Version = Versionado.Infrastructure.Version;

namespace Versionado.Controllers
{
    public class CustomersController : ApiController
    {
        [VersionedRoute("Customers", Version.Current)]
        public IEnumerable<Customer> Get()
        {
            var customers = new List<Customer>()
            {
                new Customer() { Id = 1, FriendlyName = "Customer 1"},
                new Customer() { Id = 2, FriendlyName = "Customer 2"}
            };
            return customers;
        }

        [VersionedRoute("CustomersByCity", Version.Current, true)]
        public IEnumerable<string> GetByCity(string city)
        {
            var customers = new List<Customer>()
            {
                new Customer() { Id = 1, FriendlyName = "Customer 1"},
                new Customer() { Id = 2, FriendlyName = "Customer 2"}
            };
            return customers.Select(c => c.FriendlyName);
        }
    }
}
