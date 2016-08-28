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
        void Schedule(int code, string tag, long interval);
        void Cancel(int code, string tag);
    }
}
