using DogDog_WEB.Class;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DogDog_WEB.Controllers.Petshop
{
    public class PetshopController : Controller
    {
        public ActionResult PetshopPage(string error)
        {
            var client = new HttpClient();

            if (DataStore.validateCookie(Request) == false)
            {
                return View();
            }
            else
            {
                var response = client.PostAsJsonAsync(DataStore.RequestData, JsonConvert.SerializeObject(new object[1]{
             new{
               u_id = Request.Cookies[DataStore.COOKIE_UID].Value.Split('@', '#', '*', '$', '&', '!', '%', '$')[0],
               u_role = Request.Cookies[DataStore.COOKIE_UID].Value.Split('@', '#', '*', '$', '&', '!', '%', '$')[1]
             }}));
                string resultMessage = response.Result.Content.ReadAsStringAsync().Result;
                if (resultMessage.StartsWith("Error"))
                {
                    ViewBag.error = "เรียกข้อมูลไม่สำเร็จ";
                }
                else
                {
                    var result = JsonConvert.DeserializeObject<JObject>(resultMessage);
                    ViewBag.u_id = result["id"];
                    ViewBag.role = result["role"];
                    ViewBag.fname = result["fname"];
                    ViewBag.lname = result["lname"];
                    ViewBag.proimg = result["url_pro_img"];
                    ViewBag.datesignup = result["dateSignup"].ToString().Split(' ')[0];
                    ViewBag.address = result["address"];
                    ViewBag.province = result["province"];
                    ViewBag.district = result["district"];
                    ViewBag.email = result["email"];
                    ViewBag.tel = result["tel"];
                    ViewBag.gender = result["gender"];
                    ViewBag.age = result["age"];
                }
                return View();
            }
        }
    }
}