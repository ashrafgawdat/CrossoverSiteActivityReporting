using System.Threading.Tasks;
using SiteActivityReporting.Services.Models;

namespace SiteActivityReporting.Services
{
    public interface IActivityService
    {
        void Add(CreateActivityInput input);
        GetActivityTotalOutput Total(GetActivityTotalInput input);
    }
}
