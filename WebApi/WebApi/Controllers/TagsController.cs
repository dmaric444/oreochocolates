using SGTIN96;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class TagsController : ApiController
    {
        [Route("decodetag")]
        public SGTIN96Decoder Get(string hex)
        {
            SGTIN96Decoder sgtin96Decoder = new SGTIN96Decoder(hex);
            return sgtin96Decoder;
        }

    }
}
