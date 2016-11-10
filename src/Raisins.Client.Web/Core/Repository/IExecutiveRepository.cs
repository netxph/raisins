using Raisins.Client.Web.Models;
using System.Collections.Generic;

namespace Raisins.Client.Web.Core.Repository
{
    public interface IExecutiveRepository
    {
        IEnumerable<Executive> GetAll();
    }
}