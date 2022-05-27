using Microsoft.QualityTools.Testing.Fakes;
using SiteActivityReporting.Services;
using SiteActivityReporting.Services.Models;
using System;
using Xunit;

namespace SiteActivityReporting.Tests
{
    public class ActivityTests
    {
        [Fact]
        public void Add_RoundDouble_AddsRundedUp()
        {
            var key = "key1";
            var activityService = new ActivityService();
            activityService.Add(new CreateActivityInput { Key = key, Value = 3.5 });
            var val = activityService.Total(new GetActivityTotalInput { Key = key }).Value;
            Assert.Equal(4, val);
        }

        [Fact]
        public void Add_RoundDouble_AddsRundedDown()
        {
            var key = "key1";
            var activityService = new ActivityService();
            activityService.Add(new CreateActivityInput { Key = key, Value = 3.4 });
            var val = activityService.Total(new GetActivityTotalInput { Key = key }).Value;
            Assert.Equal(3, val);
        }

        [Fact]
        public void Get_HasOldValues_ReturnsOnlyNewValues()
        {
            var now = DateTime.Now;
            var key = "key1";
            var activityService = new ActivityService();
            var totalTests = 4;
            var testValue = 2;
            var totalSuccessTests = 3 * testValue;

            int val = 0;

            for (int i = 1; i <= totalTests; i++)
            {
                using (ShimsContext.Create())
                {
                    System.Fakes.ShimDateTime.NowGet =
                    () =>
                    { return now.AddHours(-i * i); };

                    activityService.Add(new CreateActivityInput { Key = key, Value = testValue });
                }
            }

            val = activityService.Total(new GetActivityTotalInput { Key = key }).Value;
            Assert.Equal(totalSuccessTests, val);
        }
    }
}
