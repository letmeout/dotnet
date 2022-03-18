using System.Threading;
using System.Threading.Tasks;
 
namespace SampleMVC.Service
{
    public interface IWorker
    {
        Task DoWork(CancellationToken cancellationToken);
    }
}