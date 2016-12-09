using System.Threading.Tasks;

namespace Raisins.Client.Randomizer.Interfaces
{
    public interface IIntegerRandomizerService
    {
        int GetNext(int min, int max);

        Task<int> GetNextAsync(int min, int max);
    }
}
