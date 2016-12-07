using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Client.Randomizer.Interfaces
{
    public interface IIntegerRandomizerService
    {
        int GetNext(int min, int max, int iterations);
    }
}
