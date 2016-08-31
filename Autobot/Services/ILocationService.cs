using Autobot.Common;
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
        Task<Response> AddGeofence(Rule rule);

        Task<Response> RemoveGeofence(Rule rule);

        Task<Trigger> HandleLocationTrigger(Trigger trigger);

        bool IsLocationTrigger(Trigger trigger);
    }
}
