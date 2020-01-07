using System.Threading.Tasks;

namespace Budgee.Domain.DailyBudget
{
    public interface IDailyBudgetRepository
    {
        Task<DailyBudget> Load(DailyBudgetId id);
        Task Add(DailyBudget entity);
        Task<bool> Exists(DailyBudgetId id);
    }
}
