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
<<<<<<< HEAD
            public string Name { get; set; } 
=======
>>>>>>> 4cfac43ef23ba3f92c02fb306d94fb193648e2be
            public DateTime Start { get; set; }
            public DateTime End{ get; set; }
        }
    }
}
