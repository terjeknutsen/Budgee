using System.Threading.Tasks;

namespace Budgee.Framework
{
    public interface IApplicationService
    {
        Task Handle(object command);
    }
}
