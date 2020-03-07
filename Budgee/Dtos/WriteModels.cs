using SQLite;
using System;

namespace Budgee.Dtos
{
    public static class WriteModels
    {
        public class DailyBudget
        {
            [PrimaryKey]
            public Guid Id { get; set; }
            public string Name { get; set; } 
            public DateTime Start { get; set; }
            public DateTime End{ get; set; }
        }
    }
}
