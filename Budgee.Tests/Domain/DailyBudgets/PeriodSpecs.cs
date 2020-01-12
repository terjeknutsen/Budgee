using Budgee.Domain.DailyBudgets;
using NUnit.Framework;
using Should;
using SpecsFor.StructureMap;
using System;
using System.Collections.Generic;
using System.Text;

namespace Budgee.Tests.Domain.DailyBudgets
{
    class PeriodSpecs
    {
        public class When_get_days_given_ten_days_period : SpecsFor<Wrapper>
        {
            private DateTime start = new DateTime(2020, 1, 1);
            private DateTime end = new DateTime(2020, 1, 11);
            private Period result;

            protected override void When()
            {
                result = SUT.Create(start, end);
            }
            [Test]
            public void Then_period_should_be_ten_days()
            {
                result.Days.ShouldEqual(10);
            }
            [Test]
            public void Then_elapsed_days_should_be_available(){
                result.ElapsedDays(new DateTime(2020, 1, 3)).ShouldEqual(3);
            }
        }

        public class Wrapper
        {
            public Period Create(DateTime start, DateTime end) => Period.Create(start, end);
        }
    }
}
