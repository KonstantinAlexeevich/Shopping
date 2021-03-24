using System.Threading.Tasks;

namespace Domain.Foundation.Tactical
{
    public interface IRecoverFrom<in T>
    {
        Task LoadAsync(T snapshot) => Task.CompletedTask;
    }
}