using DogDog_API.Class;
using DogDog_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace DogDog_API.Controllers.LogIn_n_LogOut
{
    public class LogoutSubmitController : ApiController
    {
        DogDogEntities db = new DogDogEntities();

        public void Post([FromBody]String json)
        {
            dynamic obj = JsonConvert.DeserializeObject<ExpandoObject[]>(json)[0];
            int userId = int.Parse(Tools.DecodeString(obj.u_id));
            var uer_logout_result = (from _user in db.users
                                     where _user.id == userId
                                     select _user).FirstOrDefault();

            uer_logout_result.online = 0;
            db.SaveChanges();
        }
    }
}