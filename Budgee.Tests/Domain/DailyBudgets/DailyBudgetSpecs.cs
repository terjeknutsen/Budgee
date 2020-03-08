using NUnit.Framework;
using Should;
using SpecsFor.StructureMap;
using System;
using SpecsFor.Core;
using System.Linq;
using Budgee.Domain.DailyBudgets;

namespace Budgee.Tests.Domain
{
    class DailyBudgetSpecs
    {
        static void InitSUT(SpecsFor<DailyBudget> state, Guid budgetId, params IContext<DailyBudget>[] contexts)
        {

             state.SUT = new DailyBudget(new DailyBudgetId(budgetId), new BudgetName("budgetName"));
            foreach (var context in contexts)
                context.Initialize(state);
        }
        public class When_create_daily_budget : SpecsFor<DailyBudget>
        {
            private Guid id = Guid.NewGuid();
            protected override void InitializeClassUnderTest()
                => InitSUT(this, id);
            [Test]
            public void Then_budget_id_should_be_set()
            {
                SUT.Id.ShouldEqual(new DailyBudgetId(id));
            }
            [Test]
            public void Then_snapshot_should_be_set(){
                SUT.Snapshot.ShouldNotBeNull();
            }
            [Test]
            public void Then_daily_amount_should_equal_zero(){
                SUT.Snapshot.Daily.ShouldEqual(DailyAmount.FromDecimal(0m));
            }
            [Test]
            public void Then_available_amount_should_equal_zero(){
                SUT.Snapshot.Available.ShouldEqual(Available.FromDecimal(0m));
            }
            [Test]
            public void Then_budget_name_should_be_set(){
                SUT.Name.ShouldEqual(new BudgetName("budgetName"));
            }
        }
        public class When_add_income : SpecsFor<DailyBudget>
        {
            protected override void InitializeClassUnderTest()
                => InitSUT(this, Guid.NewGuid());
            protected override void When()
            {
                SUT.AddIncome(100m,"description", NOW);
            }
            [Test]
            public void Then_income_should_be_added(){
                SUT.Incomes.ShouldNotBeEmpty();
            }
        }
        public class When_add_income_given_period_is_set : SpecsFor<DailyBudget>
        {
            protected override void InitializeClassUnderTest()
            => InitSUT(this, Guid.NewGuid(),new TenDayPeriodSet());
            
            protected override void When()
            {
                SUT.AddIncome(100m, "description", NOW);
            }
            [Test]
            public void Then_daily_amount_should_be_set(){
                SUT.Snapshot.Daily.ShouldBeGreaterThan(DailyAmount.FromDecimal(0m));
            }
            [Test]
            public void Then_available_amount_should_be_set(){
                SUT.Snapshot.Available.ShouldBeGreaterThan(Available.FromDecimal(0m));
            }
        }
        public class When_add_income_given_has_income : SpecsFor<DailyBudget>
        {
            protected override void InitializeClassUnderTest()
                => InitSUT(this, Guid.NewGuid(),new TenDayPeriodSet(),new HasIncome());
            
            protected override void When()
            {
                SUT.AddIncome(100m,"description", NOW);
            }
            [Test]
            public void Then_income_should_be_added(){
                SUT.Incomes.Count.ShouldBeGreaterThan(1);
            }
            [Test]
            public void Then_daily_amount_should_be_doubled(){
                SUT.Snapshot.Daily.ShouldEqual(DailyAmount.FromDecimal(20m));
            }
            [Test]
            public void Then_available_amount_should_be_correct(){
                SUT.Snapshot.Available.ShouldEqual(Available.FromDecimal(20m));
            }
        }

        public class when_remove_income_given_has_income : SpecsFor<DailyBudget>
        {
            protected override void InitializeClassUnderTest()
                => InitSUT(this, Guid.NewGuid(),new TenDayPeriodSet(),new HasIncome());
        
            protected override void When()
            {
                var income = SUT.Incomes.First();
                SUT.RemoveIncome(income.Id, NOW);
            }
            [Test]
            public void Then_income_should_be_removed(){
                SUT.Incomes.ShouldBeEmpty();
            }
            [Test]
            public void Then_snapshot_should_be_updated(){
                SUT.Snapshot.Daily.ShouldEqual(DailyAmount.FromDecimal(0m));
            }

        }

        public class When_add_outgo : SpecsFor<DailyBudget>
        {
            protected override void InitializeClassUnderTest()
                => InitSUT(this, Guid.NewGuid());
            protected override void When()
            {
                SUT.AddOutgo(100m,"description", NOW);
            }
            [Test]
            public void Then_outgo_should_be_added(){
                SUT.Outgos.ShouldNotBeEmpty();
            }
        }

        public class When_remove_outgo_given_not_exist : SpecsFor<DailyBudget>
        {
            protected override void InitializeClassUnderTest()
                => InitSUT(this, Guid.NewGuid());
         
            [Test]
            public void Then_invalid_operation_exception_should_be_thrown(){

                Assert.Throws<InvalidOperationException>(() => SUT.RemoveOutgo(outgoId: Guid.NewGuid(), NOW));
            }
        }
        public class When_remove_outgo_given_exists : SpecsFor<DailyBudget>
        {
            protected override void InitializeClassUnderTest()
                => InitSUT(this, Guid.NewGuid(),new TenDayPeriodSet(),new HasIncome(),new HasOutgo());

            protected override void When()
            {
                var outgo = SUT.Outgos.First();
                SUT.RemoveOutgo(outgo.Id, NOW);
            }
            [Test]
            public void Then_outgo_should_be_removed()
            {
                SUT.Outgos.ShouldBeEmpty();
            }
            [Test]
            public void Then_snapshot_should_be_updated(){
                SUT.Snapshot.Daily.ShouldEqual(DailyAmount.FromDecimal(10m));
            }
        }
        
        public class When_change_income_given_has_income : SpecsFor<DailyBudget>
        {
            protected override void InitializeClassUnderTest()
                => InitSUT(this, Guid.NewGuid(),new TenDayPeriodSet(),new HasIncome());
            
            protected override void When()
            {
                var income = SUT.Incomes.First();
                SUT.ChangeIncome(income.Id, 50m, NOW);
            }
            [Test]
            public void Then_income_should_be_changed()
            {
                SUT.Incomes.First().Amount.ShouldEqual(Amount.FromDecimal(50m));
            }
            [Test]
            public void Then_snapshot_should_be_updated(){
                SUT.Snapshot.Daily.ShouldBeLessThan(DailyAmount.FromDecimal(10m));
            }
        }
        public class When_change_outgo_given_has_outgo : SpecsFor<DailyBudget>
        {
            protected override void InitializeClassUnderTest()
                => InitSUT(this, Guid.NewGuid(),new TenDayPeriodSet(),new HasIncome(),new HasOutgo());
            
            protected override void When()
            {
                var outgo = SUT.Outgos.First();
                SUT.ChangeOutgo(outgo.Id, 50m,NOW);
            }
            [Test]
            public void Then_outgo_should_change(){
                SUT.Outgos.First().Amount.ShouldEqual(Amount.FromDecimal(50m));
            }
            [Test]
            public void Then_snapshot_should_be_updated()
            {
                var expected = (100 - 50) / 10;
                SUT.Snapshot.Daily.ShouldEqual(DailyAmount.FromDecimal(expected));
            }
        }

        public class When_change_period_start_given_has_snapshot : SpecsFor<DailyBudget>
        {
            protected override void InitializeClassUnderTest()
            => InitSUT(this, Guid.NewGuid(), new HasIncome(), new TenDayPeriodSet(), new HasOutgo());
            protected override void When()
            {
                SUT.ChangeStart(Currentstart().AddDays(1),NOW);
            }
            [Test]
            public void Then_snapshot_should_be_changed(){
                SUT.Snapshot.Daily.ShouldEqual(DailyAmount.FromDecimal(3.33m));
            }
        }

        public class When_change_period_end_given_has_snapshot : SpecsFor<DailyBudget>
        {
            protected override void InitializeClassUnderTest()
            => InitSUT(this, Guid.NewGuid(), new HasIncome(), new TenDayPeriodSet(), new HasOutgo());
            protected override void When()
            {
                SUT.ChangeEnd(Currentstart().AddDays(9),NOW);
            }
            [Test]
            public void Then_snapshot_should_be_changed()
            {
                SUT.Snapshot.Daily.ShouldEqual(DailyAmount.FromDecimal(3.33m));
            }
        }

        public class When_set_period_given_income_already_set : SpecsFor<DailyBudget>
        {
            protected override void InitializeClassUnderTest()
            => InitSUT(this, Guid.NewGuid(), new HasIncome());
            protected override void When()
            {
                SUT.SetPeriod(NOW, NOW.AddMonths(1));
            }
            [Test]
            public void Then_snapshot_should_be_set()
            {
                SUT.Snapshot.Daily.ShouldBeGreaterThan(DailyAmount.FromDecimal(0m));
            }
        }
        

        class HasIncome : IContext<DailyBudget>
        {
            public void Initialize(ISpecs<DailyBudget> state)
            {
                state.SUT.AddIncome(100m,"description",NOW);
            }
        }

        class HasOutgo : IContext<DailyBudget>
        {
            public void Initialize(ISpecs<DailyBudget> state)
            {
                state.SUT.AddOutgo(70m,"description",NOW);
            }
        }
        static DateTime NOW = new DateTime(2020,1,1,12,0,0);
        static DateTime Currentstart() => new DateTime(2020,1,1);
        class TenDayPeriodSet : IContext<DailyBudget>
        {
            public void Initialize(ISpecs<DailyBudget> state)
            {
                state.SUT.SetPeriod(Currentstart(), Currentstart().AddDays(10));
            }
        }
    }
}
