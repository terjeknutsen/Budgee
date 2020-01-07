using Budgee.Domain.DailyBudget;
using Budgee.Infrastucture;
using System.Linq;
using System.Threading.Tasks;

namespace Budgee.DailyBudget
{
    public sealed class DailyBudgetRepository : IDailyBudgetRepository
    {
        private readonly SQLiteDatabase database;

        public DailyBudgetRepository(SQLiteDatabase database)
         =>   this.database = database;
        public async Task Add(Domain.DailyBudget.DailyBudget entity)
        {
           await database.SaveDailyBudgetAsync(entity);
        }

        public async Task<bool> Exists(DailyBudgetId id)
        {
            var budgets = await database.GetDailyBudgetAsync();
            return budgets.Any(b => b.Id == id); 
        }

        public async Task<Domain.DailyBudget.DailyBudget> Load(DailyBudgetId id)
        {
            var budgets = await database.GetDailyBudgetAsync();
            return budgets.FirstOrDefault(b => b.Id == id);
        }
    }
}
