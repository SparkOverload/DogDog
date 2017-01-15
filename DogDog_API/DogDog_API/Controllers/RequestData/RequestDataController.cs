using DogDog_API.Class;
using DogDog_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace DogDog_API.Controllers.RequestData
{
    public class RequestDataController : ApiController
    {

        DogDogEntities db = new DogDogEntities();

        public HttpResponseMessage Post([FromBody]String json)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            StringBuilder sb = new StringBuilder();
            dynamic data = JsonConvert.DeserializeObject<ExpandoObject[]>(json)[0];
            int u_id = int.Parse(Tools.DecodeString(data.u_id));
            int u_role = int.Parse(data.u_role);
            var query_u_data = (from _query in db.proflie_user
                                where _query.user.id == u_id
                                select _query).FirstOrDefault();
            if (query_u_data == null)
            {
                response.Content = new StringContent("Error");
            }
            else
            {
                sb.Append("{");
                sb.Append("\"id\":");
                sb.Append("\"" + Tools.EncodeString(query_u_data.user.id.ToString()) + "\"");
                sb.Append(",\"role\":");
                sb.Append("\"" + (query_u_data.user.role == 1 ? "Admin" : "Member") + "\"");
                sb.Append(",\"fname\":");
                sb.Append("\"" + query_u_data.fname + "\"");
                sb.Append(",\"lname\":");
                sb.Append("\"" + query_u_data.lname + "\"");
                sb.Append(",\"url_pro_img\":");
                sb.Append("\"" + query_u_data.url_pro_img + "\"");
                sb.Append(",\"dateSignup\":");
                sb.Append("\"" + query_u_data.date_signup + "\"");
                sb.Append(",\"address\":");
                sb.Append("\"" + query_u_data.address + "\"");
                sb.Append(",\"province\":");
                sb.Append("\"" + query_u_data.province + "\"");
                sb.Append(",\"district\":");
                sb.Append("\"" + query_u_data.district + "\"");
                sb.Append(",\"email\":");
                sb.Append("\"" + query_u_data.email + "\"");
                sb.Append(",\"tel\":");
                sb.Append("\"" + query_u_data.tel + "\"");
                sb.Append(",\"gender\":");
                sb.Append("\"" + query_u_data.gender + "\"");
                sb.Append(",\"age\":");
                sb.Append("\"" + query_u_data.age + "\"");
                sb.Append("}");
                response.Content = new StringContent(sb.ToString());
            }
            return response;
        }
    }
}