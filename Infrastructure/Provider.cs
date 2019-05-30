using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public abstract class Provider
    {
        public abstract IEnumerable<OperationType> SupportedOperations { get; }
        public abstract Task<IEnumerable<Device>> Discover();
    }
}
