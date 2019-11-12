using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IEffect
    {
        Task Stop();
        Task Start(IEnumerable<Device> devices);
    }
}
