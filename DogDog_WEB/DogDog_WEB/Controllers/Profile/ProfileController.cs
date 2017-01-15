using DogDog_WEB.Class;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DogDog_WEB.Controllers.Profile
{
    public class ProfileController : Controller
    {

        #region RegisterSignUp
        [HttpPost]
        public String RegisterSignUp(String user1, String pwd1, String email, int? gender1)
        {
            var client = new HttpClient();
            var response = client.PostAsJsonAsync(DataStore.SignUp, JsonConvert.SerializeObject(new object[2]{
             new{
               username = Tools.EncodeString(user1),
               password = Tools.EncodeString(pwd1),
               email = email,
               gender = gender1
             },
             new{
               active = 0
             }}));
            string resultMessage = response.Result.Content.ReadAsStringAsync().Result;
            if (resultMessage.StartsWith("Success"))
            {
                return "Success" + " : ลงทะเบียนผู้ใช้เรียบร้อย..";
            }
            else
            {
                return "Error" + " : เกิดข้อผิดพลาด username ถูกใช้ไปแล้ว..";
            }

        }
        #endregion RegisterSignUp

        #region UpdateProfile
        [HttpPost]
        public String UpdateProfile(HttpPostedFileBase up_pic, string p_fname, string p_lname, int? p_age, string p_tel,
                                    string p_add, int? p_province, int? p_district, string p_email)
        {
            var client = new HttpClient();
            string mypic = null;
            if (up_pic != null)
            {
                var allowedExtensions = new[] {
                ".JPG", ".Jpg", ".png", ".jpg", ".jpeg" ,".PNG" ,".gif"
                };

                var ext = Path.GetExtension(up_pic.FileName); //getting the extension(ex-.jpg)  
                if (allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    string name = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString()
                                  + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()
                                  + DateTime.Now.Millisecond.ToString();
                    mypic = name + ext; //appending the name with id  
                    // store the file inside ~/project folder(Img)  
                    var path = Path.Combine(Server.MapPath("../Img/ProfileImage"), mypic);
                    up_pic.SaveAs(path);
                }
                else
                {
                    return "Error:Image Extension is not support...";
                }
            }

            var response = client.PostAsJsonAsync(DataStore.SignUp, new JavaScriptSerializer().Serialize(new object[2]{
             new{
                fname = p_fname,
                lname = p_lname,
                age = p_age,
                tel = p_tel,
                address = p_add,
                province = p_province,
                district = p_district,
                email = p_email,
                url_pro_img = mypic,
             },
             new{
               active = 1,
               id = Request.Cookies[DataStore.COOKIE_UID].Value.Split('@', '#', '*', '$', '&', '!', '%', '$')[0]
             }}));
            string resultMessage = response.Result.Content.ReadAsStringAsync().Result;
            if (resultMessage.StartsWith("Error"))
            {
                return "Error:Invalid...";
            }

            return "Success:Update Profile Compelete...";
        }

        #endregion UpdateProfile
    }
}