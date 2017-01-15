using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogDog_WEB.Class
{
    public class Tools
    {
        public static String EncodeString(String key)
        {
            int SecretCode;
            String Enstring = "";
            for (int i = 0; i < key.Length; i++)
            {
                SecretCode = (int)key[i];
                if (SecretCode > 99)
                {
                    SecretCode -= 100;
                    if (SecretCode < 10)
                    {
                        SecretCode += 30;
                    }
                }
                Enstring = Enstring + (ReverseNum(SecretCode));
                if (i != key.Length - 1)
                {
                    Enstring += ":";
                }
            }
            return Enstring;
        }

        public static String ReverseNum(int Ncode)
        {
            string receiveString = Ncode.ToString();
            receiveString = receiveString[1].ToString() + receiveString[0].ToString();
            return receiveString;
        }

        public static String DecodeString(String key)
        {
            String Destring = "";
            string[] Scode = key.Split(':');
            Char chCode;
            int temp;
            foreach (String num in Scode)
            {
                temp = int.Parse(num);
                if (temp < 10)
                {
                    temp *= 10;
                    if (temp >= 30 && temp <= 39)
                    {
                        temp = temp - 30 + 100;
                    }
                    else if (temp >= 10 && temp <= 29)
                    {
                        temp += 100;
                    }
                }
                else
                {
                    temp = int.Parse(ReverseNum(int.Parse(num)));
                    if (temp >= 30 && temp <= 39)
                    {
                        temp = temp - 30 + 100;
                    }
                    else if (temp >= 10 && temp <= 29)
                    {
                        temp += 100;
                    }
                }
                chCode = (char)temp;
                Destring += chCode.ToString();
            }
            return Destring;
        }

        public static String GenSpecialCh(int num)
        {
            string Gs;
            switch (num)
            {
                case 1:
                    Gs = "@";
                    break;
                case 2:
                    Gs = "#";
                    break;
                case 3:
                    Gs = "*";
                    break;
                case 4:
                    Gs = "$";
                    break;
                case 5:
                    Gs = "&";
                    break;
                case 6:
                    Gs = "!";
                    break;
                case 7:
                    Gs = "%";
                    break;
                case 8:
                    Gs = "$";
                    break;
                default:
                    Gs = "@";
                    break;
            }
            return Gs;
        }
    }
}