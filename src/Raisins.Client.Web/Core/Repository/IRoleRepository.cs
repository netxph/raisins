using Raisins.Client.Web.Models;

namespace Raisins.Client.Web.Core.Repository
{
    public interface IRoleRepository
    {
        Role Get(string roleName);
        void Add(Role role);
    }
}