﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Foundation
{
    public class Utils
    {
        private static string EncryptKey = "TIVN*N#W";

        #region 系统版本
        /// <summary>
        /// 版本信息类
        /// </summary>
        public class VersionInfo
        {
            public int FileMajorPart
            {
                get { return 3; }
            }
            public int FileMinorPart
            {
                get { return 0; }
            }
            public int FileBuildPart
            {
                get { return 7; }
            }
            public string ProductName
            {
                get { return "MxWeiXinPF"; }
            }
            public int ProductType
            {
                get { return 1; }
            }
        }

        #endregion

        public class BaiduLBS
        {
            public int status { get; set; }
            public BaiduLBSResult result { get; set; }
        }

        public class BaiduLBSResult
        {
            public BaiduLBSResultLocation location { get; set; }
            public int precise { get; set; }
            public int confidence { get; set; }
            public string level { get; set; }
        }

        public class BaiduLBSResultLocation
        {
            public double lng { get; set; }
            public double lat { get; set; }
        }

        #region MD5加密
        public static string MD5(string pwd)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(pwd);
            byte[] md5data = md5.ComputeHash(data);
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2, '0');

            }
            return str;
        }
        #endregion

        #region 对象转换处理
        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(object expression)
        {
            if (expression != null)
                return IsNumeric(expression.ToString());

            return false;

        }

        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(string expression)
        {
            if (expression != null)
            {
                string str = expression;
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 是否为Double类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsDouble(object expression)
        {
            if (expression != null)
                return Regex.IsMatch(expression.ToString(), @"^([0-9])[0-9]*(\.\w*)?$");

            return false;
        }

        /// <summary>
        /// 检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsValidEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^[\w\.]+([-]\w+)*@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
        }
        public static bool IsValidDoEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 检测是否是正确的Url
        /// </summary>
        /// <param name="strUrl">要验证的Url</param>
        /// <returns>判断结果</returns>
        public static bool IsURL(string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }

        /// <summary>
        /// 将字符串转换为数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>字符串数组</returns>
        public static string[] GetStrArray(string str)
        {
            return str.Split(new char[',']);
        }

        /// <summary>
        /// 将数组转换为字符串
        /// </summary>
        /// <param name="list">List</param>
        /// <param name="speater">分隔符</param>
        /// <returns>String</returns>
        public static string GetArrayStr(List<string> list, string speater)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    sb.Append(list[i]);
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(speater);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// object型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(object expression, bool defValue)
        {
            if (expression != null)
                return StrToBool(expression, defValue);

            return defValue;
        }

        /// <summary>
        /// string型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(string expression, bool defValue)
        {
            if (expression != null)
            {
                if (string.Compare(expression, "true", true) == 0)
                    return true;
                else if (string.Compare(expression, "false", true) == 0)
                    return false;
            }
            return defValue;
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int ObjToInt(object expression, int defValue)
        {
            if (expression != null)
                return StrToInt(expression.ToString(), defValue);

            return defValue;
        }

        /// <summary>
        /// 将字符串转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(string expression, int defValue)
        {
            if (string.IsNullOrEmpty(expression) || expression.Trim().Length >= 11 || !Regex.IsMatch(expression.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return defValue;

            int rv;
            if (Int32.TryParse(expression, out rv))
                return rv;

            return Convert.ToInt32(StrToFloat(expression, defValue));
        }

        /// <summary>
        /// Object型转换为decimal型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的decimal类型结果</returns>
        public static decimal ObjToDecimal(object expression, decimal defValue)
        {
            if (expression != null)
                return StrToDecimal(expression.ToString(), defValue);

            return defValue;
        }

        /// <summary>
        /// string型转换为decimal型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的decimal类型结果</returns>
        public static decimal StrToDecimal(string expression, decimal defValue)
        {
            if ((expression == null) || (expression.Length > 10))
                return defValue;

            decimal intValue = defValue;
            if (expression != null)
            {
                bool IsDecimal = Regex.IsMatch(expression, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsDecimal)
                    decimal.TryParse(expression, out intValue);
            }
            return intValue;
        }

        /// <summary>
        /// Object型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float ObjToFloat(object expression, float defValue)
        {
            if (expression != null)
                return StrToFloat(expression.ToString(), defValue);

            return defValue;
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(string expression, float defValue)
        {
            if ((expression == null) || (expression.Length > 10))
                return defValue;

            float intValue = defValue;
            if (expression != null)
            {
                bool IsFloat = Regex.IsMatch(expression, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat)
                    float.TryParse(expression, out intValue);
            }
            return intValue;
        }

        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime StrToDateTime(string str, DateTime defValue)
        {
            if (!string.IsNullOrEmpty(str))
            {
                DateTime dateTime;
                if (DateTime.TryParse(str, out dateTime))
                    return dateTime;
            }
            return defValue;
        }

        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime StrToDateTime(string str)
        {
            return StrToDateTime(str, DateTime.Now);
        }

        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime ObjectToDateTime(object obj)
        {
            return StrToDateTime(obj.ToString());
        }

        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime ObjectToDateTime(object obj, DateTime defValue)
        {
            return StrToDateTime(obj.ToString(), defValue);
        }

        /// <summary>
        /// 将对象转换为字符串
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的string类型结果</returns>
        public static string ObjectToStr(object obj)
        {
            if (obj == null)
                return "";
            return obj.ToString().Trim();
        }

        /// <summary>
        /// 取该日期的星期几
        /// </summary>
        /// <param name="dayTimes">日期</param>
        /// <returns></returns>
        public static string GetDayName(DateTime dayTimes)
        {
            string result = "";

            if (dayTimes.DayOfWeek == DayOfWeek.Monday)
            {
                result = "星期一";
            }
            else if (dayTimes.DayOfWeek == DayOfWeek.Tuesday)
            {
                result = "星期二";
            }
            else if (dayTimes.DayOfWeek == DayOfWeek.Wednesday)
            {
                result = "星期三";
            }
            else if (dayTimes.DayOfWeek == DayOfWeek.Thursday)
            {
                result = "星期四";
            }
            else if (dayTimes.DayOfWeek == DayOfWeek.Friday)
            {
                result = "星期五";
            }
            else if (dayTimes.DayOfWeek == DayOfWeek.Saturday)
            {
                result = "星期六";
            }
            else if (dayTimes.DayOfWeek == DayOfWeek.Sunday)
            {
                result = "星期日";
            }
            return result;
        }

        #endregion

        #region 分割字符串
        /// <summary>
        /// 分割字符串
        /// </summary>
        public static string[] SplitString(string strContent, string strSplit)
        {
            if (!string.IsNullOrEmpty(strContent))
            {
                if (strContent.IndexOf(strSplit) < 0)
                    return new string[] { strContent };

                return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
            }
            else
                return new string[0] { };
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <returns></returns>
        public static string[] SplitString(string strContent, string strSplit, int count)
        {
            string[] result = new string[count];
            string[] splited = SplitString(strContent, strSplit);

            for (int i = 0; i < count; i++)
            {
                if (i < splited.Length)
                    result[i] = splited[i];
                else
                    result[i] = string.Empty;
            }

            return result;
        }
        #endregion

        #region 删除最后结尾的一个逗号
        /// <summary>
        /// 删除最后结尾的一个逗号
        /// </summary>
        public static string DelLastComma(string str)
        {
            if (str.Length < 1)
            {
                return "";
            }
            return str.Substring(0, str.LastIndexOf(","));
        }
        #endregion

        #region 删除最后结尾的指定字符后的字符
        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str, string strchar)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            if (str.LastIndexOf(strchar) >= 0 && str.LastIndexOf(strchar) == str.Length - 1)
            {
                return str.Substring(0, str.LastIndexOf(strchar));
            }
            return str;
        }
        #endregion

        #region 生成指定长度的字符串
        /// <summary>
        /// 生成指定长度的字符串,即生成strLong个str字符串
        /// </summary>
        /// <param name="strLong">生成的长度</param>
        /// <param name="str">以str生成字符串</param>
        /// <returns></returns>
        public static string StringOfChar(int strLong, string str)
        {
            string ReturnStr = "";
            for (int i = 0; i < strLong; i++)
            {
                ReturnStr += str;
            }

            return ReturnStr;
        }
        #endregion

        #region 生成日期随机码
        /// <summary>
        /// 生成日期随机码
        /// </summary>
        /// <returns></returns>
        public static string GetRamCode()
        {
            #region
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
            #endregion
        }
        #endregion

        #region 生成随机字母或数字
        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="length">生成长度</param>
        /// <returns></returns>
        public static string Number(int Length)
        {
            return Number(Length, false);
        }

        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        /// <returns></returns>
        public static string Number(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            string result = "";
            System.Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }
        /// <summary>
        /// 生成随机字母字符串(数字字母混和)
        /// </summary>
        /// <param name="codeCount">待生成的位数</param>
        public static string GetCheckCode(int codeCount)
        {
            string str = string.Empty;
            int rep = 0;
            long num2 = DateTime.Now.Ticks + rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (int i = 0; i < codeCount; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }

        /// <summary>
        /// 生成随机字母和数据字符串(字母和数字混合)
        /// author:李朴 datetime:2013-12-4
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string GetLetterOrNumberRandom(int Length)
        {
            System.Threading.Thread.Sleep(3);
            long tick = DateTime.Now.Ticks;//1个以0.1纳秒为单位的时间戳，18位
            char[] constant = { '2', '3', '4', '5', '6', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(constant.Length);
            Random rd = new Random(unchecked((int)tick));
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(constant.Length)]);
            }
            return newRandom.ToString();
        }


        /// <summary>
        /// 根据日期和随机码生成订单号
        /// </summary>
        /// <returns></returns>
        public static string GetOrderNumber()
        {
            string num = DateTime.Now.ToString("yyMMddHHmmss");//yyyyMMddHHmmssms
            return num + Number(2, true).ToString();
        }
        private static int Next(int numSeeds, int length)
        {
            byte[] buffer = new byte[length];
            System.Security.Cryptography.RNGCryptoServiceProvider Gen = new System.Security.Cryptography.RNGCryptoServiceProvider();
            Gen.GetBytes(buffer);
            uint randomResult = 0x0;//这里用uint作为生成的随机数  
            for (int i = 0; i < length; i++)
            {
                randomResult |= ((uint)buffer[i] << ((length - 1 - i) * 8));
            }
            return (int)(randomResult % numSeeds);
        }
        #endregion

        #region 截取字符长度
        /// <summary>
        /// 截取字符长度
        /// </summary>
        /// <param name="inputString">字符</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        //public static string CutString(string inputString, int len)
        //{
        //    if (string.IsNullOrEmpty(inputString))
        //        return "";
        //    inputString = DropHTML(inputString);
        //    ASCIIEncoding ascii = new ASCIIEncoding();
        //    int tempLen = 0;
        //    string tempString = "";
        //    byte[] s = ascii.GetBytes(inputString);
        //    for (int i = 0; i < s.Length; i++)
        //    {
        //        if ((int)s[i] == 63)
        //        {
        //            tempLen += 2;
        //        }
        //        else
        //        {
        //            tempLen += 1;
        //        }

        //        try
        //        {
        //            tempString += inputString.Substring(i, 1);
        //        }
        //        catch
        //        {
        //            break;
        //        }

        //        if (tempLen > len)
        //            break;
        //    }
        //    //如果截过则加上半个省略号 
        //    byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
        //    if (mybyte.Length > len)
        //        tempString += "…";
        //    return tempString;
        //}
        #endregion

        #region 清除HTML标记
        //public static string DropHTML(string Htmlstring)
        //{
        //    if (string.IsNullOrEmpty(Htmlstring)) return "";
        //    //删除脚本  
        //    Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
        //    //删除HTML  
        //    Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);

        //    Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
        //    Htmlstring.Replace("<", "");
        //    Htmlstring.Replace(">", "");
        //    Htmlstring.Replace("\r\n", "");
        //    Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
        //    return Htmlstring;
        //}
        #endregion
        #region 去除HTML标记
        ///<summary>   
        ///去除HTML标记   
        ///</summary>   
        ///<param name="NoHTML">包括HTML的源码</param>   
        ///<returns>已经去除后的文字</returns>   
        public static string NoHTML(string Htmlstring)
        {
            //Regex myReg = new Regex(@"(\<.[^\<]*\>)", RegexOptions.IgnoreCase);
            //Htmlstring = myReg.Replace(Htmlstring, "");
            //myReg = new Regex(@"(\<\/[^\<]*\>)", RegexOptions.IgnoreCase);
            //Htmlstring = myReg.Replace(Htmlstring, "");
            //return Htmlstring;

            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&ldquo;", "“", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&rdquo;", "”", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring = Htmlstring.Replace("<", "&lt;");
            Htmlstring = Htmlstring.Replace(">", "&gt;");
            return Htmlstring;
        }
        #endregion

        /// <summary>
        /// 替换html中的特殊字符
        /// </summary>
        /// <param name="theString">需要进行替换的文本。</param>
        /// <returns>替换完的文本。</returns>
        public static string HtmlEncode(string theString)
        {
            theString = theString.Replace(">", "&gt;");
            theString = theString.Replace("<", "&lt;");
            theString = theString.Replace("  ", " &nbsp;");
            theString = theString.Replace("\"", "&quot;");
            theString = theString.Replace("'", "&#39;");
            theString = theString.Replace("\r\n", "<br/> ");
            return theString;
        }

        /// <summary>
        /// 恢复html中的特殊字符
        /// </summary>
        /// <param name="theString">需要恢复的文本。</param>
        /// <returns>恢复好的文本。</returns>
        public static string HtmlDecode(string theString)
        {
            theString = theString.Replace("&gt;", ">");
            theString = theString.Replace("&lt;", "<");
            theString = theString.Replace(" &nbsp;", "  ");
            theString = theString.Replace("&quot;", "\"");
            theString = theString.Replace("&#39;", "'");
            theString = theString.Replace("<br/> ", "\r\n");
            theString = theString.Replace("&mdash;", "—");//2012-05-07新加的
            return theString;
        }

        #region 清除HTML标记且返回相应的长度
        //public static string DropHTML(string Htmlstring, int strLen)
        //{
        //    return CutString(DropHTML(Htmlstring), strLen);
        //}
        #endregion

        #region TXT代码转换成HTML格式
        /// <summary>
        /// 字符串字符处理
        /// </summary>
        /// <param name="chr">等待处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        /// //把TXT代码转换成HTML格式
        public static String ToHtml(string Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("&", "&amp;");
            sb.Replace("<", "&lt;");
            sb.Replace(">", "&gt;");
            sb.Replace("\r\n", "<br />");
            sb.Replace("\n", "<br />");
            sb.Replace("\t", " ");
            //sb.Replace(" ", "&nbsp;");
            return sb.ToString();
        }
        #endregion

        #region HTML代码转换成TXT格式
        /// <summary>
        /// 字符串字符处理
        /// </summary>
        /// <param name="chr">等待处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        /// //把HTML代码转换成TXT格式
        public static String ToTxt(String Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("&nbsp;", " ");
            sb.Replace("<br>", "\r\n");
            sb.Replace("<br>", "\n");
            sb.Replace("<br />", "\n");
            sb.Replace("<br />", "\r\n");
            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            sb.Replace("&amp;", "&");
            return sb.ToString();
        }
        #endregion

        #region 检测是否有Sql危险字符
        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果,如果安全，则返回true，否则返回false</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 检查危险字符
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string Filter(string sInput)
        {
            if (sInput == null || sInput == "")
                return null;
            string sInput1 = sInput.ToLower();
            string output = sInput;
            string pattern = @"*|and|exec|insert|select|delete|update|count|master|truncate|declare|char(|mid(|chr(|'";
            if (Regex.Match(sInput1, Regex.Escape(pattern), RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
            {
                throw new Exception("字符串中含有非法字符!");
            }
            else
            {
                output = output.Replace("'", "''");
            }
            return output;
        }

        /// <summary> 
        /// 检查过滤设定的危险字符
        /// </summary> 
        /// <param name="InText">要过滤的字符串 </param> 
        /// <returns>如果参数存在不安全字符，则返回true </returns> 
        public static bool SqlFilter(string word, string InText)
        {
            if (InText == null)
                return false;
            foreach (string i in word.Split('|'))
            {
                if ((InText.ToLower().IndexOf(i + " ") > -1) || (InText.ToLower().IndexOf(" " + i) > -1))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 过滤特殊字符
        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string Htmls(string Input)
        {
            if (Input != string.Empty && Input != null)
            {
                string ihtml = Input.ToLower();
                ihtml = ihtml.Replace("<script", "&lt;script");
                ihtml = ihtml.Replace("script>", "script&gt;");
                ihtml = ihtml.Replace("<%", "&lt;%");
                ihtml = ihtml.Replace("%>", "%&gt;");
                ihtml = ihtml.Replace("<$", "&lt;$");
                ihtml = ihtml.Replace("$>", "$&gt;");
                return ihtml;
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

        #region 检查是否为IP地址
        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        #endregion

        

        #region 获得当前绝对路径
        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        //public static string GetMapPath(string strPath)
        //{
        //    if (strPath.ToLower().StartsWith("http://"))
        //    {
        //        return strPath;
        //    }
        //    if (HttpContext.Current != null)
        //    {
        //        return HttpContext.Current.Server.MapPath(strPath);
        //    }
        //    else //非web程序引用
        //    {
        //        strPath = strPath.Replace("/", "\\");
        //        if (strPath.StartsWith("\\"))
        //        {
        //            strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
        //        }
        //        return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
        //    }
        //}
        #endregion

        #region 文件操作
        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="_filepath">文件相对路径</param>
        //public static bool DeleteFile(string _filepath)
        //{
        //    if (string.IsNullOrEmpty(_filepath))
        //    {
        //        return false;
        //    }
        //    string fullpath = GetMapPath(_filepath);
        //    if (File.Exists(fullpath))
        //    {
        //        File.Delete(fullpath);
        //        return true;
        //    }
        //    return false;
        //}

        /// <summary>
        /// 删除上传的文件(及缩略图)
        /// </summary>
        /// <param name="_filepath"></param>
        //public static void DeleteUpFile(string _filepath)
        //{
        //    if (string.IsNullOrEmpty(_filepath))
        //    {
        //        return;
        //    }
        //    string fullpath = GetMapPath(_filepath); //原图
        //    if (File.Exists(fullpath))
        //    {
        //        File.Delete(fullpath);
        //    }
        //    if (_filepath.LastIndexOf("/") >= 0)
        //    {
        //        string thumbnailpath = _filepath.Substring(0, _filepath.LastIndexOf("/")) + "mall_" + _filepath.Substring(_filepath.LastIndexOf("/") + 1);
        //        string fullTPATH = GetMapPath(thumbnailpath); //宿略图
        //        if (File.Exists(fullTPATH))
        //        {
        //            File.Delete(fullTPATH);
        //        }
        //    }
        //}

        /// <summary>
        /// 删除指定文件夹
        /// </summary>
        /// <param name="_dirpath">文件相对路径</param>
        //public static bool DeleteDirectory(string _dirpath)
        //{
        //    if (string.IsNullOrEmpty(_dirpath))
        //    {
        //        return false;
        //    }
        //    string fullpath = GetMapPath(_dirpath);
        //    if (Directory.Exists(fullpath))
        //    {
        //        Directory.Delete(fullpath, true);
        //        return true;
        //    }
        //    return false;
        //}

        /// <summary>
        /// 修改指定文件夹名称
        /// </summary>
        /// <param name="old_dirpath">旧相对路径</param>
        /// <param name="new_dirpath">新相对路径</param>
        /// <returns>bool</returns>
        //public static bool MoveDirectory(string old_dirpath, string new_dirpath)
        //{
        //    if (string.IsNullOrEmpty(old_dirpath))
        //    {
        //        return false;
        //    }
        //    string fulloldpath = GetMapPath(old_dirpath);
        //    string fullnewpath = GetMapPath(new_dirpath);
        //    if (Directory.Exists(fulloldpath))
        //    {
        //        Directory.Move(fulloldpath, fullnewpath);
        //        return true;
        //    }
        //    return false;
        //}

        /// <summary>
        /// 返回文件大小KB
        /// </summary>
        /// <param name="_filepath">文件相对路径</param>
        /// <returns>int</returns>
        //public static int GetFileSize(string _filepath)
        //{
        //    if (string.IsNullOrEmpty(_filepath))
        //    {
        //        return 0;
        //    }
        //    string fullpath = GetMapPath(_filepath);
        //    if (File.Exists(fullpath))
        //    {
        //        FileInfo fileInfo = new FileInfo(fullpath);
        //        return ((int)fileInfo.Length) / 1024;
        //    }
        //    return 0;
        //}

        /// <summary>
        /// 返回文件扩展名，不含“.”
        /// </summary>
        /// <param name="_filepath">文件全名称</param>
        /// <returns>string</returns>
        public static string GetFileExt(string _filepath)
        {
            if (string.IsNullOrEmpty(_filepath))
            {
                return "";
            }
            if (_filepath.LastIndexOf(".") > 0)
            {
                return _filepath.Substring(_filepath.LastIndexOf(".") + 1); //文件扩展名，不含“.”
            }
            return "";
        }

        /// <summary>
        /// 返回文件名，不含路径
        /// </summary>
        /// <param name="_filepath">文件相对路径</param>
        /// <returns>string</returns>
        public static string GetFileName(string _filepath)
        {
            return _filepath.Substring(_filepath.LastIndexOf(@"/") + 1);
        }

        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="_filepath">文件相对路径</param>
        /// <returns>bool</returns>
        //public static bool FileExists(string _filepath)
        //{
        //    string fullpath = GetMapPath(_filepath);
        //    if (File.Exists(fullpath))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        /// <summary>
        /// 获得远程字符串
        /// </summary>
        //public static string GetDomainStr(string key, string uriPath)
        //{
        //    string result = CacheHelper.Get(key) as string;
        //    if (result == null)
        //    {
        //        System.Net.WebClient client = new System.Net.WebClient();
        //        try
        //        {
        //            client.Encoding = System.Text.Encoding.UTF8;
        //            result = client.DownloadString(uriPath);
        //        }
        //        catch
        //        {
        //            result = "";
        //        }
        //        CacheHelper.Insert(key, result, 60);
        //    }

        //    return result;
        //}

        #endregion

        #region 读取或写入cookie
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        //public static void WriteCookie(string strName, string strValue)
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
        //    if (cookie == null)
        //    {
        //        cookie = new HttpCookie(strName);
        //    }
        //    cookie.Value = UrlEncode(strValue);
        //    HttpContext.Current.Response.AppendCookie(cookie);
        //}

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        //public static void WriteCookie(string strName, string key, string strValue)
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
        //    if (cookie == null)
        //    {
        //        cookie = new HttpCookie(strName);
        //    }
        //    cookie[key] = UrlEncode(strValue);
        //    HttpContext.Current.Response.AppendCookie(cookie);
        //}

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        //public static void WriteCookie(string strName, string key, string strValue, int expires)
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
        //    if (cookie == null)
        //    {
        //        cookie = new HttpCookie(strName);
        //    }
        //    cookie[key] = UrlEncode(strValue);
        //    cookie.Expires = DateTime.Now.AddMinutes(expires);
        //    HttpContext.Current.Response.AppendCookie(cookie);
        //}

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(分钟)</param>
        //public static void WriteCookie(string strName, string strValue, int expires)
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
        //    if (cookie == null)
        //    {
        //        cookie = new HttpCookie(strName);
        //    }
        //    cookie.Value = UrlEncode(strValue);
        //    cookie.Expires = DateTime.Now.AddMinutes(expires);
        //    HttpContext.Current.Response.AppendCookie(cookie);
        //}

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        //public static string GetCookie(string strName)
        //{
        //    if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
        //        return UrlDecode(HttpContext.Current.Request.Cookies[strName].Value.ToString());
        //    return "";
        //}

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        //public static string GetCookie(string strName, string key)
        //{
        //    if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
        //        return UrlDecode(HttpContext.Current.Request.Cookies[strName][key].ToString());

        //    return "";
        //}
        #endregion

        #region 替换指定的字符串
        /// <summary>
        /// 替换指定的字符串
        /// </summary>
        /// <param name="originalStr">原字符串</param>
        /// <param name="oldStr">旧字符串</param>
        /// <param name="newStr">新字符串</param>
        /// <returns></returns>
        public static string ReplaceStr(string originalStr, string oldStr, string newStr)
        {
            if (string.IsNullOrEmpty(oldStr))
            {
                return "";
            }
            return originalStr.Replace(oldStr, newStr);
        }
        #endregion

        #region 显示分页
        /// <summary>
        /// 返回分页页码
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="linkUrl">链接地址，__id__代表页码</param>
        /// <param name="centSize">中间页码数量</param>
        /// <returns></returns>
        public static string OutPageList(int pageSize, int pageIndex, int totalCount, string linkUrl, int centSize)
        {
            //计算页数
            if (totalCount < 1 || pageSize < 1)
            {
                return "";
            }
            int pageCount = totalCount / pageSize;
            if (pageCount < 1)
            {
                return "";
            }
            if (totalCount % pageSize > 0)
            {
                pageCount += 1;
            }
            if (pageCount <= 1)
            {
                return "";
            }
            StringBuilder pageStr = new StringBuilder();
            string pageId = "__id__";
            string firstBtn = "<a href=\"" + ReplaceStr(linkUrl, pageId, (pageIndex - 1).ToString()) + "\">«上一页</a>";
            string lastBtn = "<a href=\"" + ReplaceStr(linkUrl, pageId, (pageIndex + 1).ToString()) + "\">下一页»</a>";
            string firstStr = "<a href=\"" + ReplaceStr(linkUrl, pageId, "1") + "\">1</a>";
            string lastStr = "<a href=\"" + ReplaceStr(linkUrl, pageId, pageCount.ToString()) + "\">" + pageCount.ToString() + "</a>";

            if (pageIndex <= 1)
            {
                firstBtn = "<span class=\"disabled\">«上一页</span>";
            }
            if (pageIndex >= pageCount)
            {
                lastBtn = "<span class=\"disabled\">下一页»</span>";
            }
            if (pageIndex == 1)
            {
                firstStr = "<span class=\"current\">1</span>";
            }
            if (pageIndex == pageCount)
            {
                lastStr = "<span class=\"current\">" + pageCount.ToString() + "</span>";
            }
            int firstNum = pageIndex - (centSize / 2); //中间开始的页码
            if (pageIndex < centSize)
                firstNum = 2;
            int lastNum = pageIndex + centSize - ((centSize / 2) + 1); //中间结束的页码
            if (lastNum >= pageCount)
                lastNum = pageCount - 1;
            pageStr.Append("<span>共" + totalCount + "记录</span>");
            pageStr.Append(firstBtn + firstStr);
            if (pageIndex >= centSize)
            {
                pageStr.Append("<span>...</span>\n");
            }
            for (int i = firstNum; i <= lastNum; i++)
            {
                if (i == pageIndex)
                {
                    pageStr.Append("<span class=\"current\">" + i + "</span>");
                }
                else
                {
                    pageStr.Append("<a href=\"" + ReplaceStr(linkUrl, pageId, i.ToString()) + "\">" + i + "</a>");
                }
            }
            if (pageCount - pageIndex > centSize - ((centSize / 2)))
            {
                pageStr.Append("<span>...</span>");
            }
            pageStr.Append(lastStr + lastBtn);
            return pageStr.ToString();
        }
        #endregion

        #region URL处理
        /// <summary>
        /// URL字符编码
        /// </summary>
        //public static string UrlEncode(string str)
        //{
        //    if (string.IsNullOrEmpty(str))
        //    {
        //        return "";
        //    }
        //    str = str.Replace("'", "");
        //    return HttpContext.Current.Server.UrlEncode(str);
        //}

        /// <summary>
        /// URL字符解码
        /// </summary>
        //public static string UrlDecode(string str)
        //{
        //    if (string.IsNullOrEmpty(str))
        //    {
        //        return "";
        //    }
        //    return HttpContext.Current.Server.UrlDecode(str);
        //}

        /// <summary>
        /// 组合URL参数
        /// </summary>
        /// <param name="_url">页面地址</param>
        /// <param name="_keys">参数名称</param>
        /// <param name="_values">参数值</param>
        /// <returns>String</returns>
        //public static string CombUrlTxt(string _url, string _keys, params string[] _values)
        //{
        //    StringBuilder urlParams = new StringBuilder();
        //    try
        //    {
        //        string[] keyArr = _keys.Split(new char[] { '&' });
        //        for (int i = 0; i < keyArr.Length; i++)
        //        {
        //            if (!string.IsNullOrEmpty(_values[i]) && _values[i] != "0")
        //            {
        //                _values[i] = UrlEncode(_values[i]);
        //                urlParams.Append(string.Format(keyArr[i], _values) + "&");
        //            }
        //        }
        //        if (!string.IsNullOrEmpty(urlParams.ToString()) && _url.IndexOf("?") == -1)
        //            urlParams.Insert(0, "?");
        //        //李朴-----add  2014-3-30 begin ----
        //        if (!string.IsNullOrEmpty(urlParams.ToString()) && _url.Contains("&") && _url.LastIndexOf("&") < (_url.Length - 1))
        //        {
        //            _url += "&";
        //        }
        //        //李朴-----add  2014-3-30 end ----
        //    }
        //    catch
        //    {
        //        return _url;
        //    }
        //    return _url + DelLastChar(urlParams.ToString(), "&");
        //}
        #endregion

        #region URL请求数据
        /// <summary>
        /// HTTP POST方式请求数据
        /// </summary>
        /// <param name="url">URL.</param>
        /// <param name="param">POST的数据</param>
        /// <returns></returns>
        public static string HttpPost(string url, string param)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "*/*";
            request.Timeout = 15000;
            request.AllowAutoRedirect = false;

            StreamWriter requestStream = null;
            WebResponse response = null;
            string responseStr = null;

            try
            {
                requestStream = new StreamWriter(request.GetRequestStream());
                requestStream.Write(param);
                requestStream.Close();

                response = request.GetResponse();
                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                request = null;
                requestStream = null;
                response = null;
            }

            return responseStr;
        }

        /// <summary>
        /// HTTP GET方式请求数据.
        /// </summary>
        /// <param name="url">URL.</param>
        /// <returns></returns>
        public static string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            //request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            request.Timeout = 15000;
            request.AllowAutoRedirect = false;

            WebResponse response = null;
            string responseStr = null;

            try
            {
                response = request.GetResponse();

                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                request = null;
                response = null;
            }

            return responseStr;
        }

        /// <summary>
        /// 执行URL获取页面内容
        /// </summary>
        //public static string UrlExecute(string urlPath)
        //{
        //    if (string.IsNullOrEmpty(urlPath))
        //    {
        //        return "error";
        //    }
        //    StringWriter sw = new StringWriter();
        //    try
        //    {
        //        HttpContext.Current.Server.Execute(urlPath, sw);
        //        return sw.ToString();
        //    }
        //    catch (Exception)
        //    {
        //        return "error";
        //    }
        //    finally
        //    {
        //        sw.Close();
        //        sw.Dispose();
        //    }
        //}
        #endregion

        #region 操作权限菜单
        /// <summary>
        /// 获取操作权限
        /// </summary>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, string> ActionType()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Show", "显示");
            dic.Add("View", "查看");
            dic.Add("Add", "添加");
            dic.Add("Edit", "修改");
            dic.Add("Delete", "删除");
            dic.Add("Audit", "审核");
            dic.Add("Reply", "回复");
            dic.Add("Confirm", "确认");
            dic.Add("Cancel", "取消");
            dic.Add("Invalid", "作废");
            dic.Add("Build", "生成");
            dic.Add("Instal", "安装");
            dic.Add("Unload", "卸载");
            dic.Add("Back", "备份");
            dic.Add("Restore", "还原");
            dic.Add("Replace", "替换");
            return dic;
        }
        #endregion

        #region 替换URL
        /// <summary>
        /// 替换扩展名
        /// </summary>
        public static string GetUrlExtension(string urlPage, string staticExtension)
        {
            int indexNum = urlPage.LastIndexOf('.');
            if (indexNum > 0)
            {
                return urlPage.Replace(urlPage.Substring(indexNum), "." + staticExtension);
            }
            return urlPage;
        }
        /// <summary>
        /// 替换扩展名，如没有扩展名替换默认首页
        /// </summary>
        public static string GetUrlExtension(string urlPage, string staticExtension, bool defaultVal)
        {
            int indexNum = urlPage.LastIndexOf('.');
            if (indexNum > 0)
            {
                return urlPage.Replace(urlPage.Substring(indexNum), "." + staticExtension);
            }
            if (defaultVal)
            {
                if (urlPage.EndsWith("/"))
                {
                    return urlPage + "index." + staticExtension;
                }
                else
                {
                    return urlPage + "/index." + staticExtension;
                }
            }
            return urlPage;
        }
        #endregion

        #region 迁移

        /// <summary>
        /// 生成随机用户名
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomString(int length)
        {
            string randStr = string.Empty;
            Random rd = new Random();
            byte[] str = new byte[length];
            int i;
            for (i = 0; i < length - 1; i++)
            {
                int a = 0;
                while (!((a >= 48 && a <= 57) || (a >= 97 && a <= 122)))
                {
                    a = rd.Next(48, 122);
                }
                str[i] = (byte)a;
            }
            string username = new string(UnicodeEncoding.ASCII.GetChars(str));
            Random r = new Random(unchecked((int)DateTime.Now.Ticks));
            string s1 = ((char)r.Next(97, 122)).ToString();
            username = username.Replace("\0", "");
            randStr = s1 + username;
            return randStr;
        }

        public static string ComputeSignature(string key, string data)
        {
            using (var algorithm = KeyedHashAlgorithm.Create("HmacSHA1".ToUpperInvariant()))
            {
                algorithm.Key = Encoding.UTF8.GetBytes(key.ToCharArray());
                return Convert.ToBase64String(
                    algorithm.ComputeHash(Encoding.UTF8.GetBytes(data.ToCharArray())));
            }
        }

        #endregion


        /// <summary>
        /// 将时间戳转换为日期类型，并格式化
        /// </summary>
        /// <param name="longDateTime"></param>
        /// <returns></returns>
        public static DateTime LongDateTimeToDateTimeString(string longDateTime)
        {
            long unixDate = long.Parse(longDateTime);
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddMilliseconds(unixDate).ToLocalTime();
        }

        /// <summary>
        /// 获取本机的IP地址
        /// </summary>
        public static string GetLocalIp()
        {
            string tempip = string.Empty;
            try
            {
                ////获取本机外网ip的url
                //string getIpUrl = "https://www.ipip.net/ip.html";//网上获取ip地址的网站
                //WebRequest wr = WebRequest.Create(getIpUrl);
                //Stream s = wr.GetResponse().GetResponseStream();
                //StreamReader sr = new StreamReader(s, Encoding.UTF8);
                ////读取网站的数据
                //string all = sr.ReadToEnd();
                ////解析出需要的数据
                ////int start = all.IndexOf("<th colspan=\"3\">您的当前IP: <span style=\"color: rgb(243, 102, 102);\">");
                //int start = all.IndexOf("<tr>\r\n            <td>当前IP</td>");
                //int end = all.IndexOf("</span></td>\r\n        </tr>");
                //tempip = all.Substring(start, end - start).Replace("<tr>\r\n            <td>当前IP</td>\r\n            <td colspan=\"6\"><span style=\"margin-left: -200px;\">", "");
                //sr.Close();
                //s.Close();

                string getIpUrl = "http://ip.taobao.com/service/getIpInfo2.php?ip=myip";
                WebRequest wr = WebRequest.Create(getIpUrl);
                Stream s = wr.GetResponse().GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.UTF8);
                //读取网站的数据
                string all = sr.ReadToEnd();

                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<TaoBaoGetIpInterfaceReturnDTO>(all);
                if (result.Code == 0)
                {
                    tempip = result.Data.Ip;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("获取IP地址出错");
            }
            return tempip;
        }

        public static string GetIpAddress()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
            for (int i = 0; i < ipEntry.AddressList.Length; i++)
            {
                //从IP地址列表中筛选出IPv4类型的IP地址
                //AddressFamily.InterNetwork表示此IP为IPv4,
                //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                if (ipEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    return ipEntry.AddressList[i].ToString();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// HMACSHA1算法加密并返回ToBase64String
        /// </summary>
        /// <param name="EncryptText">签名参数字符串</param>
        /// <param name="EncryptKey">密钥参数</param>
        /// <returns>返回一个签名值(即哈希值)</returns>
        public static string ToBase64hmac(string EncryptText)
        {
            HMACSHA1 myHMACSHA1 = new HMACSHA1(Encoding.UTF8.GetBytes(EncryptKey));
            byte[] byteText = myHMACSHA1.ComputeHash(Encoding.UTF8.GetBytes(EncryptText));
            return System.Convert.ToBase64String(byteText);
        }

        /// <summary>
        /// 获取地址的经纬度
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static string GetLatLngByBaidu(string address)
        {
            try
            {
                string getIpUrl = "http://api.map.baidu.com/geocoder/v2/?address=" + address + "&output=json&ak=KNjT4nRnZsv9ESKV8D5dSsP0gWBUtMBH";
                WebRequest wr = WebRequest.Create(getIpUrl);
                Stream s = wr.GetResponse().GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.UTF8);
                //读取网站的数据
                string result = sr.ReadToEnd();
                //解析出需要的数据
                //{
                //"status": 0,
                //"result": {
                //  "location": {
                //      "lng": 112.93220728907756,
                //      "lat": 28.21451539967663
                //      },
                //  "precise": 1,
                //  "confidence": 80,
                //  "level": "道路"
                //  }
                //}
                sr.Close();
                s.Close();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("出错", ex);
            }

        }

        /// <summary>
        /// 文件转为流
        /// </summary>
        public static Stream FileToStream(string fileName)
        {
            // 打开文件
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[]
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary>
        /// 网络图片转存为流
        /// </summary>
        public static Stream GetNetImageToStream(string imgurl)
        {
            if (string.IsNullOrEmpty(imgurl))
            {
                return null;
            }
            // 打开图片路径  
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(imgurl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response.GetResponseStream();
        }

        public static MemoryStream StreamToMemoryStream(Stream stream)
        {
            MemoryStream memoryStream = new MemoryStream();
            const int bufferLength = 1024;
            int actual;
            byte[] buffer = new byte[bufferLength];
            while ((actual = stream.Read(buffer, 0, bufferLength)) > 0)
            {
                memoryStream.Write(buffer, 0, actual);
            }
            memoryStream.Position = 0;
            return memoryStream;
        }

        #region 隐藏手号中间
        public static string GetDesmobile(string mobile)
        {
            string _mobile = string.Empty;
            if (mobile.Length >= 2)
            {
                _mobile += mobile.Substring(0, 3);
                _mobile += "****";
                _mobile += mobile.Substring(mobile.Length - 4, 4);
            }
            return _mobile;
        }
        #endregion

        /// <summary>
        /// 调用此函数后使此两种图片合并，类似相册，有个
        /// 背景图，中间贴自己的目标图片
        /// </summary>
        /// <param name="sourceImg">粘贴的源图片</param>
        /// <param name="destImg">粘贴的目标图片</param>
        //public static Image CombinImage(string sourceImg, string destImg, int left, int top)
        //{
        //    Image imgBack = Image.FromStream(GetNetImageToStream(sourceImg));     //相框图片  
        //    Image img = Image.FromStream(GetNetImageToStream(destImg));        //照片图片

        //    //从指定的System.Drawing.Image创建新的System.Drawing.Graphics        
        //    Graphics g = Graphics.FromImage(imgBack);

        //    int sourcewidth = imgBack.Width;
        //    int sourceheight = imgBack.Height;

        //    int destwidth = img.Width;
        //    int destheight = img.Height;

        //    //int x = sourcewidth / 2 - destwidth / 2;
        //    //int y = sourceheight / 2 - destheight / 2;

        //    g.DrawImage(imgBack, 0, 0, sourcewidth, sourceheight);      // g.DrawImage(imgBack, 0, 0, 相框宽, 相框高); 
        //    //g.FillRectangle(Brushes.Black, 16, 16, (int)112 + 2, ((int)73 + 2));//相片四周刷一层黑色边框

        //    //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);
        //    g.DrawImage(img, left, top, destwidth, destheight);
        //    GC.Collect();
        //    return imgBack;
        //}

        /// <summary>
        /// 获取指定驱动器的空间总大小(单位为GB)
        /// </summary>
        /// <param name="str_HardDiskName">只需输入代表驱动器的字母即可</param>
        /// <returns></returns>
        public static long GetHardDiskSpace(string str_HardDiskName)
        {
            long totalSize = new long();
            str_HardDiskName = str_HardDiskName + ":\\";
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    totalSize = drive.TotalSize / (1024 * 1024 * 1024);
                }
            }
            return totalSize;
        }

        /// <summary>  
        /// 获取指定驱动器的剩余空间总大小(单位为GB)
        /// </summary>
        /// <param name=”str_HardDiskName”>只需输入代表驱动器的字母即可</param>
        /// <returns></returns>  
        public static long GetHardDiskFreeSpace(string str_HardDiskName)
        {
            long freeSpace = new long();
            str_HardDiskName = str_HardDiskName + ":\\";
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    freeSpace = drive.TotalFreeSpace / (1024 * 1024 * 1024);
                }
            }
            return freeSpace;
        }
    }
}
