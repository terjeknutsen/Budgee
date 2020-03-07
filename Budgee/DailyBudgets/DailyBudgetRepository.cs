using Budgee.Domain.DailyBudgets;
using Budgee.Infrastucture;
using System.Threading.Tasks;

namespace Budgee.DailyBudgets
{
    public sealed class DailyBudgetRepository : IDailyBudgetRepository
    {
        private readonly DailyBudgetDatabase database;

        public DailyBudgetRepository(DailyBudgetDatabase database)
        {
            this.database = database;
        }
        public async Task Add(DailyBudget entity)
        {
            var item = new Dtos.WriteModels.DailyBudget
            {
                Id = entity.Id,
                Start = entity.Period.FromA,
                End = entity.Period.ToB
            };
            await database.AddItemAsync(item);
        }
        public async Task Update(DailyBudget entity)
        {
            var item = new Dtos.WriteModels.DailyBudget
            {
                Id = entity.Id,
                Start = entity.Period.FromA,
                End = entity.Period.ToB
            };
            await database.UpdateItemAsync(item);
        }
        public async Task<bool> Exists(DailyBudgetId id)
        {
            var budget = await database.GetItemAsync(id);
            return budget != null; 
        }

        public async Task<DailyBudget> Load(DailyBudgetId id)
        {
           var entity = await database.GetItemAsync(id);
            return new DailyBudget(id);    
        }
    }
}
