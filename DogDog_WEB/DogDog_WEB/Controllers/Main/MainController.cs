using DogDog_WEB.Class;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DogDog_WEB.Controllers.Main
{
    public class MainController : Controller
    {
        public ActionResult MainPage()
        {
            var client = new HttpClient();

            if (DataStore.validateCookie(Request) == false)
            {
                clearCookie();
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

        #region LoginSubmit
        public string LoginSubmit(String user, String pwd)
        {
            Random random = new Random();
            var client = new HttpClient();
            if (user != "" && pwd != "")
            {
                var response = client.PostAsJsonAsync(DataStore.SignIn, JsonConvert.SerializeObject(new object[1]{
                 new{
                 username = Tools.EncodeString(user),
                 password = Tools.EncodeString(pwd)
                }}));
                string resultMessage = response.Result.Content.ReadAsStringAsync().Result;
                if (resultMessage.StartsWith("Error"))
                {
                    return "Error:ชื่อผู้ใช้ไม่ถูกต้อง หรือ ชื่อนี้มีการใช้งาน ณ ตอนนี้ โปรดตรวจสอบ";
                }
                var result = JsonConvert.DeserializeObject<JObject>(resultMessage);
                HttpCookie cookie_id = new HttpCookie(DataStore.COOKIE_UID);
                cookie_id.Value = result["id"]
                    + Tools.GenSpecialCh(random.Next(1, 9)) + result["role"];
                Response.Cookies.Add(cookie_id);

                HttpCookie cookie_data = new HttpCookie(DataStore.COOKIE_UDATA);
                cookie_data.Value = result["fname"]
                    + Tools.GenSpecialCh(random.Next(1, 9)) + result["lname"]
                    + Tools.GenSpecialCh(random.Next(1, 9)) + result["url_pro_img"];
                Response.Cookies.Add(cookie_data);

                return "Success:สำเร็จ!";

            }
            else
            {
                return "Error:ชื่อผู้ใช้หรือรหัสผ่านไม่ควรเป็นช่องว่าง โปรดตรวจสอบ";
            }

        }
        #endregion

        #region LogoutSubmit
        public ActionResult LogoutSubmit()
        {
            var client = new HttpClient();
            var response = client.PostAsJsonAsync(DataStore.SignOut, JsonConvert.SerializeObject(new object[1]{
                new{
                u_id = Request.Cookies[DataStore.COOKIE_UID].Value.Split('@', '#', '*', '$', '&', '!', '%', '$')[0]
                }}));
            clearCookie();
            return RedirectToAction("MainPage");
        }
        #endregion

        public void clearCookie()
        {
            HttpCookie cookie_id = new HttpCookie(DataStore.COOKIE_UID);
            cookie_id.Expires = DateTime.Today.AddDays(-365);
            Response.Cookies.Add(cookie_id);
            HttpCookie cookie_data = new HttpCookie(DataStore.COOKIE_UDATA);
            cookie_data.Expires = DateTime.Today.AddDays(-365);
            Response.Cookies.Add(cookie_data);
        }

    }
}