using System.Collections.Generic;
using System.Web.Http.Routing;

namespace Versionado.Infrastructure
{
    internal class VersionedRouteAttribute : RouteFactoryAttribute
    {
        public VersionedRouteAttribute(string template, int allowedVersion, bool allowPreviousVersions = false)
            : base(template)
        {
            AllowedVersion = allowedVersion;
            AllowPreviousVersions = allowPreviousVersions;
        }

        public int AllowedVersion { get; }

        public bool AllowPreviousVersions { get; }

        public override IDictionary<string, object> Constraints
        {
            get
            {
                var constraints = new HttpRouteValueDictionary();
                constraints.Add("version", new VersionConstraint(AllowedVersion, AllowPreviousVersions));
                return constraints;
            }
        }
    }
}