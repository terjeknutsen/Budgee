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
            database.CreateTableAsync<Domain.DailyBudget.DailyBudget>().Wait();
        }

        public Task<List<Domain.DailyBudget.DailyBudget>> GetDailyBudgetAsync()
            => database.Table<Domain.DailyBudget.DailyBudget>().ToListAsync();

        public Task<int> SaveDailyBudgetAsync(Domain.DailyBudget.DailyBudget dailyBudget)
            => database.InsertAsync(dailyBudget);
    }
}
