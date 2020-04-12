using Chromely;
using Chromely.Core;
using Chromely.Core.Network;
using Majora.Controllers;
using System;

namespace Majora
{
    class Majora : ChromelyBasicApp
    {
        public override void Configure(IChromelyContainer container)
        {
            base.Configure(container);
            container.RegisterSingleton(typeof(ChromelyController), Guid.NewGuid().ToString(), typeof(MajoraController));
        }
    }
}
