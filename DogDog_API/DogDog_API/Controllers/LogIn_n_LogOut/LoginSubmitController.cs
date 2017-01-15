using DogDog_API.Class;
using DogDog_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace DogDog_API.Controllers.LogIn_n_LogOut
{
    public class LoginSubmitController : ApiController
    {
        DogDogEntities db = new DogDogEntities();

        public HttpResponseMessage Post([FromBody]String json)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            StringBuilder sb = new StringBuilder();
            DataUser u = JsonConvert.DeserializeObject<DataUser[]>(json)[0];
            u.username = Tools.DecodeString(u.username);
            u.password = Tools.DecodeString(u.password);
            var query = (from _query in db.proflie_user
                         where _query.user.username == u.username &&
                               _query.user.password == u.password &&
                               _query.user.online == 0
                         select new
                         {
                             u_id = _query.user.id,
                             u_role = _query.user.role,
                             u_fname = _query.fname,
                             u_lname = _query.lname,
                             u_datesignup = _query.date_signup,
                             u_url_pro_img = _query.url_pro_img
                         });

            if (query.Count() != 1)
            {
                response.Content = new StringContent("Error");
                return response;
            }
            var user_result = query.First();

            //================ online ID ======================
            var user_online = (from _user in db.users
                               where _user.id == user_result.u_id
                               select _user).FirstOrDefault();
            user_online.online = 1;
            db.SaveChanges();
            //================ online ID ======================

            sb.Append("{");
            sb.Append("\"id\":" + "\"" + Tools.EncodeString(user_result.u_id.ToString()) + "\"");
            sb.Append(",\"role\":" + "\"" + user_result.u_role + "\"");
            sb.Append(",\"fname\":" + "\"" + user_result.u_fname + "\"");
            sb.Append(",\"lname\":" + "\"" + user_result.u_lname + "\"");
            sb.Append(",\"url_pro_img\":");
            sb.Append("\"" + user_result.u_url_pro_img + "\"");
            sb.Append("}");
            response.Content = new StringContent(sb.ToString());
            return response;

        }
    }
}