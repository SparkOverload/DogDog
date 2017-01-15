using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogDog_API.Class
{
    public class DataUser
    {
        public String username { set; get; }
        public String password { set; get; }
        public int? role { set; get; }
    }

    public class DataProflieUser
    {
        public String fname { set; get; }
        public String lname { set; get; }
        public int? age { set; get; }
        public String address { set; get; }
        public int? province { set; get; }
        public int? district { set; get; }
        public String tel { set; get; }
        public String email { set; get; }
        public int? gender { set; get; }
        public String url_pro_img { set; get; }
        public DateTime date_signup { set; get; }
    }
}