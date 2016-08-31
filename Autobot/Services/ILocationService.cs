using Autobot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autobot.Services
{
    public interface ILocationService
    {
        Task<bool> AddGeofence(Rule rule);

        Task<bool> RemoveGeofence(Rule rule);

        Task<Trigger> HandleLocationTrigger(Trigger trigger);

        bool IsLocationTrigger(Trigger trigger);
    }
}
