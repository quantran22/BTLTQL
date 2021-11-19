using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Security.Cryptography;

namespace LTQL.Models
{
    public class StringProcess
    {
        public string GenerateKey(string id)
        {
            string strkey = "";
            string numPart = "", strPart = "", strPhanSo = "";
            numPart = Regex.Match(id, @"\d+").Value;
            strPart = Regex.Match(id, @"\D+").Value;
            //them ca so o de kich thuoc = phan so => 1+1 = 2
            int phanso = (Convert.ToInt32(numPart) + 1);
            for (int i = 0; i < numPart.Length - phanso.ToString().Length; i++)
            {
                strPhanSo += "0";
            }
            strPhanSo += phanso;
            strkey = strPart + strPhanSo;
            return strkey;
        }
        public string GetMD5 (string strInput)
        {
            string str_md5 = "";
            byte[] arrOut = System.Text.Encoding.UTF8.GetBytes(strInput);
            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            arrOut = my_md5.ComputeHash(arrOut);
            foreach (byte b in arrOut)
            {
                str_md5 += b.ToString("X2");
            }
            return str_md5;
        }
    }
}