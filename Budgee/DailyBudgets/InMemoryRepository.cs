using Budgee.Domain.DailyBudgets;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budgee.DailyBudgets.DailyBudgets
{
    public sealed class InMemoryRepository : IDailyBudgetRepository
    {
        private List<DailyBudget> elements = new List<DailyBudget>();
        public Task Add(DailyBudget entity)
        {
            elements.Add(entity);
            return Task.CompletedTask;
        }

        public Task<bool> Exists(DailyBudgetId id)
        {
            return Task.FromResult(elements.Any(e => e.Id == id));
        }

        public Task<DailyBudget> Load(DailyBudgetId id)
        {
            return Task.FromResult(elements.FirstOrDefault(e => e.Id == id));
        }

        public async Task Update(DailyBudget entity)
        {
            var element = await Load(entity.Id);
            var index = elements.IndexOf(element);
            elements[index] = entity;
        }
    }
}
