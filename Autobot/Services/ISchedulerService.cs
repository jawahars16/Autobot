using Autobot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autobot.Services
{
    public interface ISchedulerService
    {
        void Schedule(Rule rule);
        void Cancel(Rule rule);
    }
}
