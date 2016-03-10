using System.Net.Http.Headers;
using System.Web.Http;
using Versionado.Models;

namespace Versionado.Infrastructure
{
    public static class HttpConfigurationExtensions
    {
        public static void RegisterCustomMediaTypeFormatters(this HttpConfiguration config)
        {
            const string vendorName = "analyticalways";
            config.Formatters.Insert(0, new TypedJsonMediaTypeFormatter(
                typeof(string), new MediaTypeHeaderValue(string.Format("application/vnd.{0}-v1+json", vendorName))));
            config.Formatters.Insert(1, new TypedJsonMediaTypeFormatter(
                typeof(string), new MediaTypeHeaderValue(string.Format("application/vnd.{0}-v2+json", vendorName))));
            config.Formatters.Insert(2, new TypedJsonMediaTypeFormatter(
                typeof(Customer), new MediaTypeHeaderValue(string.Format("application/vnd.{0}-v2+json", vendorName))));
        }
    }
}