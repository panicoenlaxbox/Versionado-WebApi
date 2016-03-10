using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http.Routing;

namespace Versionado.Infrastructure
{
    internal class VersionConstraint : IHttpRouteConstraint
    {
        public const string VersionHeaderName = "X-Api-Version";

        private const int DefaultVersion = 1;

        public VersionConstraint(int allowedVersion, bool allowPreviousVersions)
        {
            AllowedVersion = allowedVersion;
            AllowPreviousVersions = allowPreviousVersions;
        }

        public int AllowedVersion
        {
            get;
        }

        public bool AllowPreviousVersions { get; }

        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            if (routeDirection == HttpRouteDirection.UriResolution)
            {
                int version = GetVersionHeader(request) ?? DefaultVersion;
                if (version == AllowedVersion || (version < AllowedVersion && AllowPreviousVersions))
                {
                    return true;
                }
            }
            return false;
        }

        private int? GetVersionHeader(HttpRequestMessage request)
        {
            string versionAsString = null;
            IEnumerable<string> headerValues;
            if (request.Headers.TryGetValues(VersionHeaderName, out headerValues) && headerValues.Count() == 1)
            {
                versionAsString = headerValues.First();
            }
            else
            {
                var accept = request.Headers.Accept.Where(a => a.Parameters.Count(p => p.Name == "version") > 0);
                if (accept.Any())
                {
                    versionAsString = accept.First().Parameters.Single(s => s.Name == "version").Value;
                }
                else
                {
                    var applicationPattern = @"^application\/vnd\..*-v\d+\+.*$";
                    accept = request.Headers.Accept.Where(a => Regex.IsMatch(a.MediaType, applicationPattern, RegexOptions.Singleline));

                    if (accept.Any())
                    {
                        var applicationVersionPattern = @"^application\/vnd\..*-v(?<version>\d+)\+.*$";
                        versionAsString = Regex.Match(accept.First().MediaType, applicationVersionPattern, RegexOptions.Singleline).Groups["version"].Value;
                    }
                }
            }
            if (versionAsString == null)
            {
                return null;
            }
            int version;
            if (int.TryParse(versionAsString, out version))
            {
                return version;
            }
            return null;
        }
    }
}