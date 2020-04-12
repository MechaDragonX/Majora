using Chromely.Core.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Majora.Controllers
{
    class MajoraController : ChromelyController
    {
        [HttpGet(Route = "/play")]
        public ChromelyResponse PlayMusic(ChromelyRequest request)
        {
            return new ChromelyResponse()
            {
                RequestId = request.Id
            };
        }
    }
}
