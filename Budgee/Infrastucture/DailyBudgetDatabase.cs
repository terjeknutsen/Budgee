using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budgee.Infrastucture
{
    public class DailyBudgetDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(()=> {
            return new SQLiteAsyncConnection(DbConstants.DatabasePath, DbConstants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public DailyBudgetDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }
        async Task InitializeAsync()
        {
            if(!initialized)
            {
                
                if(!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Dtos.WriteModels.DailyBudget).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Dtos.WriteModels.DailyBudget)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<Dtos.WriteModels.DailyBudget>> GetItemsAsync() => Database.Table<Dtos.WriteModels.DailyBudget>().ToListAsync();
        public Task<Dtos.WriteModels.DailyBudget> GetItemAsync(Guid id) => Database.Table<Dtos.WriteModels.DailyBudget>().Where(b => b.Id == id).FirstOrDefaultAsync();
        public Task<int> AddItemAsync(Dtos.WriteModels.DailyBudget item) => Database.InsertAsync(item);
        public Task<int> UpdateItemAsync(Dtos.WriteModels.DailyBudget item) => Database.UpdateAsync(item);
        public Task<int> DeleteItemAsync(Dtos.WriteModels.DailyBudget item) => Database.DeleteAsync(item);
    }
}
