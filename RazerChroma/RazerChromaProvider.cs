using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazerChroma
{
    public class RazerChromaProvider : Provider
    {
        public override IEnumerable<OperationType> SupportedOperations => throw new NotImplementedException();

        public override Task<IEnumerable<Device>> Discover()
        {
            throw new NotImplementedException();
        }
    }
}
