using System.Security.Claims;
using VergeDBAPI.Models;

namespace VergeDBAPI.Validation
{
    public class UserData
    {
        private ClaimsIdentity _identity;

        public void Identify(ClaimsIdentity identity)
        {
            _identity = identity;
        }

        public OrgRole UserRole
        {
            get
            {
                return (OrgRole)Enum.Parse(typeof(OrgRole), _identity.Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)
                    .SingleOrDefault());
            }
            set { }
        }

        public string UserOrg
        {
            get
            {
                return _identity.Claims
                    .Where(c => c.Type == ClaimTypes.GivenName)
                    .Select(c => c.Value)
                    .SingleOrDefault();
            }
            set { }
        }

        public string? UserName
        {
            get
            {
                return _identity.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value)
                    .SingleOrDefault();
            }

            set { }
        }
    }

    
}
