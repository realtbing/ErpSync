using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Foundation.Encrypt
{
    /// <summary>
    /// ASE加解密
    /// </summary>
    public class AESHelper
    {
        /// <summary>
        /// Aes解密
        /// </summary>
        /// <param name="str">需要解密的字符串</param>
        /// <param name="key">密钥,长度不够时空格补齐,超过时从左截取</param>
        /// <param name="iv">偏移量,长度不够时空格补齐,超过时从左截取</param>
        /// <param name="keyLenth">秘钥长度,16 24 32</param>
        /// <param name="aesMode">解密模式</param>
        /// <param name="aesPadding">填充方式</param>
        /// <returns></returns>
        public static string AesDecode(string text, string AesKey, string AesIV, int keyLenth = 16, CipherMode aesMode = CipherMode.CBC, PaddingMode aesPadding = PaddingMode.PKCS7)
        {
            if (!new List<int> { 16, 24, 32 }.Contains(keyLenth))
            {
                return null;//密钥的长度，16位密钥 = 128位，24位密钥 = 192位，32位密钥 = 256位。
            }
            var encryptedData = Convert.FromBase64String(text);
            var bKey = new Byte[keyLenth];
            Array.Copy(Convert.FromBase64String(AesKey.PadRight(keyLenth)), bKey, keyLenth);
            var bIv = new Byte[16];
            Array.Copy(Convert.FromBase64String(AesIV.PadRight(16)), bIv, 16);

            var rijndaelCipher = new RijndaelManaged
            {
                Mode = aesMode,
                Padding = aesPadding,
                Key = bKey,
                IV = bIv,
            };
            var decryptor = rijndaelCipher.CreateDecryptor(rijndaelCipher.Key, rijndaelCipher.IV);
            var rtByte = decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(rtByte);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="AesKey">秘钥</param>
        /// <param name="AesIV">16位初始向量</param>
        /// <returns></returns>
        public static string AESDecrypt(string text, string AesKey, string AesIV)
        {
            try
            {
                //16进制数据转换成byte
                byte[] encryptedData = Convert.FromBase64String(text);  // strToToHexByte(text);
                RijndaelManaged rijndaelCipher = new RijndaelManaged();
                rijndaelCipher.Key = Convert.FromBase64String(AesKey); // Encoding.UTF8.GetBytes(AesKey);
                rijndaelCipher.IV = Convert.FromBase64String(AesIV);// Encoding.UTF8.GetBytes(AesIV);
                rijndaelCipher.Mode = CipherMode.CBC;
                rijndaelCipher.Padding = PaddingMode.PKCS7;
                ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
                byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                string result = Encoding.UTF8.GetString(plainText);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
