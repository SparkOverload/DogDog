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

namespace DogDog_API.Controllers.Profile
{
    public class ProfileSubmitController : ApiController
    {

        DogDogEntities db = new DogDogEntities();

        public HttpResponseMessage Post([FromBody]String json)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            StringBuilder sb = new StringBuilder();
            dynamic status_id = JsonConvert.DeserializeObject<ExpandoObject[]>(json)[1];
            if (status_id.active == 0)
            {
                dynamic obj = JsonConvert.DeserializeObject<ExpandoObject[]>(json)[0];
                DataUser u = new DataUser
                {
                    username = Tools.DecodeString(obj.username),
                    password = Tools.DecodeString(obj.password),
                    role = 2
                };
                DataProflieUser p = new DataProflieUser
                {
                    fname = "User",
                    lname = "Member",
                    tel = "0812345678",
                    gender = unchecked((int)obj.gender),
                    email = obj.email,
                    date_signup = DateTime.Now,
                    url_pro_img = "Avatar.jpg"
                };

                var query = (from _query in db.users
                             where _query.username == u.username
                             select _query);

                if (query.Count() > 0)
                {
                    response.Content = new StringContent("Error");
                    return response;
                }

                user saveuser = new user
                {
                    username = u.username,
                    password = u.password,
                    role = u.role,
                    online = 0
                };
                db.users.Add(saveuser);

                proflie_user savepro = new proflie_user
                {
                    id_user = saveuser.id,
                    fname = p.fname,
                    lname = p.lname,
                    tel = p.tel,
                    gender = p.gender,
                    email = p.email,
                    date_signup = p.date_signup,
                    url_pro_img = p.url_pro_img
                };
                db.proflie_user.Add(savepro);
                db.SaveChanges();
                response.Content = new StringContent("Success");
            }
            else if (status_id.active == 1)
            {
                DataProflieUser update_pro = JsonConvert.DeserializeObject<DataProflieUser[]>(json)[0];
                int u_id = int.Parse(Tools.DecodeString(status_id.id));
                var update_profile = (from _query in db.proflie_user
                                      where _query.user.id == u_id
                                      select _query);
                if (update_profile.Count() != 1)
                {
                    response.Content = new StringContent("Error");
                    return response;
                }
                var profile_update = update_profile.First();
                profile_update.fname = string.IsNullOrEmpty(update_pro.fname) ? profile_update.fname : update_pro.fname;
                profile_update.lname = string.IsNullOrEmpty(update_pro.lname) ? profile_update.lname : update_pro.lname;
                profile_update.age = string.IsNullOrEmpty(update_pro.age.ToString()) ? profile_update.age : update_pro.age;
                profile_update.address = string.IsNullOrEmpty(update_pro.address) ? profile_update.address : update_pro.address;
                profile_update.province = update_pro.province == null ? null : update_pro.province;
                profile_update.district = update_pro.district == null ? null : update_pro.district;
                profile_update.tel = string.IsNullOrEmpty(update_pro.tel) ? profile_update.tel : update_pro.tel;
                profile_update.email = string.IsNullOrEmpty(update_pro.email) ? profile_update.email : update_pro.email;
                profile_update.url_pro_img = string.IsNullOrEmpty(update_pro.url_pro_img) ? profile_update.url_pro_img : update_pro.url_pro_img;
                db.SaveChanges();
                response.Content = new StringContent("Success");
            }
            else
            {
                response.Content = new StringContent("Error");
            }
            return response;

        }
    }
}