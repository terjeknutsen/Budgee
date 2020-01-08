using Budgee.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Budgee.Mobile.DailyBudgets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DailyBudgetPage : ContentPage
    {
        public DailyBudgetPage(IApplicationService applicationService)
        {
            InitializeComponent();
            BindingContext = new DailyBudgetViewModel(applicationService);
        }
    }
}