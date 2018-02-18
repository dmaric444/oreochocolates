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
        public HttpResponseMessage Get(string hex)
        {
            SGTIN96Decoder sgtin96Decoder = new SGTIN96Decoder(hex);
            if (sgtin96Decoder.CodeIsValid)
                return Request.CreateResponse<SGTIN96Decoder>(HttpStatusCode.OK, sgtin96Decoder);
            else
                return Request.CreateResponse<object>(HttpStatusCode.BadRequest, new { message = "Tag is not valid" });
        }

    }
}
