using System;
using System.Security.Cryptography;
using System.Text;

//using RBD.Client.Components;

namespace RBD.Client.Services
{
    public static class CryptoHelper
    {
        private const string KEY = "cryptokey";
        private static readonly byte[] IV = new byte[8] { 240, 3, 45, 29, 0, 76, 173, 59 };

        #region Encrypt

        /// <summary>
        /// Шифруем бинарник
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Encrypt(this byte[] data)
        {
            byte[] result;

            var des = new TripleDESCryptoServiceProvider();
            var MD5 = new MD5CryptoServiceProvider();

            des.Key = MD5.ComputeHash(ASCIIEncoding.UTF8.GetBytes(KEY));
            des.IV = IV;
            result = des.CreateEncryptor().TransformFinalBlock(data, 0, data.Length);

            return result;
        }

        /// <summary>
        /// Возвращается зашифрованная Base64 строка
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string EncryptAsUnicode(this string text)
        {
            var data = ASCIIEncoding.Unicode.GetBytes(text);
            var bytes = Encrypt(data);
            return Convert.ToBase64String(bytes);
        }

        #endregion

        #region Decrypt

        /// <summary>
        /// Расшифровка из бинарника
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Decrypt(this byte[] data)
        {
            byte[] result;

            var des = new TripleDESCryptoServiceProvider();
            var MD5 = new MD5CryptoServiceProvider();

            des.Key = MD5.ComputeHash(ASCIIEncoding.UTF8.GetBytes(KEY));
            des.IV = IV;
            result = des.CreateDecryptor().TransformFinalBlock(data, 0, data.Length);

            return result;
        }

        /// <summary>
        /// На вход подается Base64 строка, возвращается Unicode строка
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string DecryptAsUnicode(this string text)
        {
            var data = Convert.FromBase64String(text);
            var bytes = Decrypt(data);
            return ASCIIEncoding.Unicode.GetString(bytes);
        }

        public static byte[] DecryptAsBase64(this string text)
        {
            var data = Convert.FromBase64String(text);
            return Decrypt(data);
        }

        #endregion
    }
}
