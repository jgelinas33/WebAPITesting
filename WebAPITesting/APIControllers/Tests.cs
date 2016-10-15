using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace WebAPITesting.APIControllers.TestControllers
{
    [RoutePrefix("testing")]
    public class Tests : ApiController
    {
        /// <summary>
        /// Handles a raw string in the html body
        /// POST http://localhost:3333/registerappapi/testing/TestRawString HTTP/1.1
        /// User-Agent: Fiddler
        /// Host: localhost:3333
        /// Content-Length: 12
        /// Content-Type: application/json
        /// "raw string"
        /// </summary>
        /// <param name="rawString"></param>
        /// <returns></returns>
        [HttpPost()]
        [Route("TestRawString")]
        public IHttpActionResult TestRawStringInBody([FromBody] string rawString)
        {
            var requestHeaders = Request.Headers;
            var requestContext = Request.GetRequestContext();
            var requestConect = Request.Content;
            return Ok(rawString);
        }
        /// <summary>
        /// POST http://localhost:3333/registerappapi/testing/testformdatacollection HTTP/1.1
        /// User-Agent: Fiddler
        /// Host: localhost:3333
        /// Content-Length: 41
        /// Content-Type: application/x-www-form-urlencoded; charset=UTF-8
        /// name=Rick&value=12&entered=12%2F10%2F2011
        /// </summary>
        [HttpPost()]
        [Route("TestFormDataCollection")]
        public IHttpActionResult TestFormData(FormDataCollection formData)
        {
            return Ok(formData);
        }
    }
}
