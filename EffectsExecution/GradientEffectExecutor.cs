using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EffectsExecution
{
    public class GradientEffectExecutor : EffectExecutor
    {
        public GradientEffectExecutor() : base(new GradientEffectMetadata())
        {
        }
        protected override Task StartInternal()
        {
            throw new NotImplementedException();
        }

        protected override Task StopInternal()
        {
            throw new NotImplementedException();
        }
    }
}
