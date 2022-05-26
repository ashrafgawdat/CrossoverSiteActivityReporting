using Pose;
using SiteActivityReporting.Services;
using SiteActivityReporting.Services.Models;
using System;
using Xunit;

namespace SiteActivityReporting.Tests
{
    public class ActivityTests
    {
        [Fact]
        public void AddValue_RoundDoubleUp()
        {
            var key = "key1";
            var activityService = new ActivityService();
            activityService.Add(new CreateActivityInput { Key = key, Value = 3.5 });
            var val = activityService.Total(new GetActivityTotalInput { Key = key }).Value;
            Assert.Equal(4, val);
        }

        [Fact]
        public void AddValue_RoundDoubleDown()
        {
            var key = "key1";
            var activityService = new ActivityService();
            activityService.Add(new CreateActivityInput { Key = key, Value = 3.4 });
            var val = activityService.Total(new GetActivityTotalInput { Key = key }).Value;
            Assert.Equal(3, val);
        }

        [Fact]
        public void UserCreated()
        {
            var key = "key1";
            var activityService = new ActivityService();

            activityService.Add(new CreateActivityInput { Key = key, Value = 2 });
            //activityService.Add(new CreateActivityInput { Key = key, Value = 2 });
            //activityService.Add(new CreateActivityInput { Key = key, Value = 2 });

            int val = 0;

            Shim shim = Shim.Replace(() => DateTime.Now).With(() => new DateTime(2022, 05, 26));
            PoseContext.Isolate(() =>
            {
                activityService.Add(new CreateActivityInput { Key = key, Value = 2 });
                //var val2 = activityService.Total(new GetActivityTotalInput { Key = key }).Value;
            }, shim);

            //for (int i = 1; i <= 4; i++)
            //{
            //    Shim shim = Shim.Replace(() => DateTime.Now).With(() => DateTime.Now.AddHours(-i*i));

            //    PoseContext.Isolate(() =>
            //    {
            //        date = DateTime.Now;
            //        activityService.Add(new CreateActivityInput { Key = key, Value = 2 });
            //    }, shim);
            //}


            //Assert.Equal(new DateTime(2021, 07, 20), date);

            val = activityService.Total(new GetActivityTotalInput { Key = key }).Value;

            Assert.Equal(119, val);
        }
    }
}
