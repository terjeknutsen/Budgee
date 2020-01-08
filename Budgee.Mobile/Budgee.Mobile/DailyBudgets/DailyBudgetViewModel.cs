using Budgee.Framework;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Budgee.Mobile.DailyBudgets
{
    public class DailyBudgetViewModel : BaseViewModel
    {
        private readonly IApplicationService applicationService;

        public DailyBudgetViewModel(IApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }

        public ICommand Create => new Command(() => 
        {
        var budgetId = Guid.NewGuid();
            applicationService.Handle(new DailyBudget.Commands.V1.Create { DailyBudgetId = budgetId });
        });
    }
}
