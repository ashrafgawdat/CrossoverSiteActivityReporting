using SiteActivityReporting.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteActivityReporting.Services
{
    public class ActivityService : IActivityService
    {
        private Dictionary<string, List<Activity>> values;

        public ActivityService()
        {
            values = new Dictionary<string, List<Activity>>();
        }

        public void Add(CreateActivityInput input)
        {
            var val = (int)Math.Round(input.Value);
            var newActivity = new Activity { Value = val, DateAdded = DateTime.Now };
            if (values.ContainsKey(input.Key))
            {
                values[input.Key].Add(new Activity { Value = val, DateAdded = DateTime.Now });

            }
            else
            {
                values.Add(input.Key, new List<Activity> { newActivity });
            }
        }

        public GetActivityTotalOutput Total(GetActivityTotalInput input)
        {
            if (!values.ContainsKey(input.Key))
                return null;

            var keyValues = values[input.Key];
            var sum = keyValues.Where(p => p.DateAdded >= DateTime.Now.AddHours(-12)).Sum(p => p.Value);
            return new GetActivityTotalOutput { Value = sum };
        }
    }
}
