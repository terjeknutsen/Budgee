using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Budgee.Infrastucture
{
    public sealed class SQLiteDatabase
    {
        readonly SQLiteAsyncConnection database;

        public SQLiteDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Domain.DailyBudgets.DailyBudget>().Wait();
        }

        public Task<List<Domain.DailyBudgets.DailyBudget>> GetDailyBudgetAsync()
            => database.Table<Domain.DailyBudgets.DailyBudget>().ToListAsync();

        public Task<int> SaveDailyBudgetAsync(Domain.DailyBudgets.DailyBudget dailyBudget)
            => database.InsertAsync(dailyBudget);
    }
}
