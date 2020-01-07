using SpecsFor;
using NUnit.Framework;
using Should;
using Moq;
using SpecsFor.StructureMap;
using DailyBudget = Budgee.Domain.DailyBudget.DailyBudget;
using System;
using SpecsFor.Core;
using System.Linq;

namespace Budgee.Tests.Domain
{
    class DailyBudgetSpecs
    {
        static void InitSUT(SpecsFor<DailyBudget> state, Guid budgetId)
            => state.SUT = new DailyBudget(new Budgee.Domain.DailyBudget.DailyBudgetId(budgetId));
        public class When_create_daily_budget : SpecsFor<DailyBudget>
        {
            private Guid id = Guid.NewGuid();
            protected override void InitializeClassUnderTest()
                => InitSUT(this, id);
            [Test]
            public void Then_budget_id_should_be_set()
            {
                SUT.Id.ShouldEqual(new Budgee.Domain.DailyBudget.DailyBudgetId(id));
            }
        }
        public class When_add_income : SpecsFor<DailyBudget>
        {
            protected override void InitializeClassUnderTest()
                => InitSUT(this, Guid.NewGuid());
            protected override void When()
            {
                SUT.AddIncome(100m);
            }
            [Test]
            public  void Then_total_income_should_be_set()
            {
                SUT.TotalIncome.Amount.ShouldEqual(100m);
            }
        }
        public class When_add_income_given_has_income : SpecsFor<DailyBudget>
        {
            protected override void InitializeClassUnderTest()
                => InitSUT(this, Guid.NewGuid());
            protected override void Given()
            {
                Given<HasIncome>();
                base.Given();
            }
            protected override void When()
            {
                SUT.AddIncome(100m);
            }
            [Test]
            public void Then_income_should_be_summed()
            {
                SUT.TotalIncome.Amount.ShouldEqual(200m);
            }
        }

        public class when_remove_income_given_has_income : SpecsFor<DailyBudget>
        {
            protected override void InitializeClassUnderTest()
                => InitSUT(this, Guid.NewGuid());
            protected override void Given()
            {
                Given<HasIncome>();
                base.Given();
            }
            protected override void When()
            {
                var income = SUT.Incomes.First();
                SUT.RemoveIncome(income.Id);
            }
            [Test]
            public void Then_total_income_should_be_zero()
            {
                SUT.TotalIncome.Amount.ShouldEqual(0m);
            }
        }

        public class When_add_outgo : SpecsFor<DailyBudget>
        {
            protected override void InitializeClassUnderTest()
                => InitSUT(this, Guid.NewGuid());
            protected override void When()
            {
                SUT.AddOutgo(100m);
            }
            [Test]
            public void Then_outgo_should_be_added(){
                SUT.Outgos.ShouldNotBeEmpty();
            }
            [Test]
            public void Then_total_outgo_should_be_increased(){
                SUT.TotalOutgo.Amount.ShouldEqual(100m);
            }
        }

        public class When_remove_outgo_given_not_exist : SpecsFor<DailyBudget>
        {
            protected override void InitializeClassUnderTest()
                => InitSUT(this, Guid.NewGuid());
            protected override void When()
            {
                
            }
            [Test]
            public void Then_invalid_operation_exception_should_be_thrown(){

                Assert.Throws<InvalidOperationException>(() => SUT.RemoveOutgo(outgoId: Guid.NewGuid()));
            }
        }
        public class When_remove_outgo_given_exists : SpecsFor<DailyBudget>
        {
            protected override void InitializeClassUnderTest()
                => InitSUT(this, Guid.NewGuid());

            protected override void Given()
            {
                Given<HasOutgo>();
                base.Given();
            }
            protected override void When()
            {
                var outgo = SUT.Outgos.First();
                SUT.RemoveOutgo(outgo.Id);
            }
            [Test]
            public void Then_outgo_should_be_removed()
            {
                SUT.Outgos.ShouldBeEmpty();
            }
        }
        
        public class When_change_income_given_has_income : SpecsFor<DailyBudget>
        {
            protected override void InitializeClassUnderTest()
                => InitSUT(this, Guid.NewGuid());
            protected override void Given()
            {
                Given<HasIncome>();
                base.Given();
            }
            protected override void When()
            {
                var income = SUT.Incomes.First();
                SUT.ChangeIncome(income.Id, 50m);
            }
            [Test]
            public void Then_total_income_should_be_changes(){
                SUT.TotalIncome.Amount.ShouldEqual(50m);
            }
        }
        public class When_change_outgo_given_has_outgo : SpecsFor<DailyBudget>
        {
            protected override void InitializeClassUnderTest()
                => InitSUT(this, Guid.NewGuid());
            protected override void Given()
            {
                Given<HasOutgo>();
                base.Given();
            }
            protected override void When()
            {
                var outgo = SUT.Outgos.First();
                SUT.ChangeOutgo(outgo.Id, 50m);
            }
            [Test]
            public void Then_outgo_should_change(){
                SUT.TotalOutgo.Amount.ShouldEqual(50m);
            }
        }

        class HasIncome : IContext<DailyBudget>
        {
            public void Initialize(ISpecs<DailyBudget> state)
            {
                state.SUT.AddIncome(100m);
            }
        }

        class HasOutgo : IContext<DailyBudget>
        {
            public void Initialize(ISpecs<DailyBudget> state)
            {
                state.SUT.AddOutgo(100m);
            }
        }
    }
}
