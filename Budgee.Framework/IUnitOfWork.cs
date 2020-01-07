using System.Threading.Tasks;

namespace Budgee.Framework
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
