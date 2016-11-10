using Raisins.Client.Web.Persistence;

namespace Raisins.Client.Web.Migrations
{
    public interface IDbSeeder
    {

        void Seed(RaisinsDB context);

    }
}
