using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Data.Migrations.Seeder
{
    public interface IDbSeeder
    {
        void Seed(RaisinsContext context);

    }
}
