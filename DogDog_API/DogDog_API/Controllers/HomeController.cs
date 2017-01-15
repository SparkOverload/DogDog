using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DogDog_API.Controllers
{
    public class HomeController : Controller
    {
        public String Index()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.Append("<head></head>");
            sb.Append("<body>");
            sb.Append("<h1 style='color:red'>Server API Status : OK</h1>");
            sb.Append("<h1>Server Version : 0.0.1</h1>");
            sb.Append("<h1>Time : " + DateTime.Now + "</h1>");
            sb.Append("</body></html>");
            return sb.ToString();
        }
    }
}