using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
//using System.Web.Mvc;

namespace frontend.API.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult Index()
        {
            return ResponseMessage(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("OK", Encoding.ASCII, "text/plain")
            });
        }
    }
}
