using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Cares.FidsII.Util.Security
{
    public static class SecurityHelper
    {
        private const string DEFAULT_STRING = "Cares.FIDSII.UTIL";
        private static readonly string PASSWORD = DEFAULT_STRING;
        private static readonly byte[] SALT = Encoding.ASCII.GetBytes(DEFAULT_STRING);
        private static readonly int KEY_LEN = 32;
        private const int IV_LEN = 16;

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string Encrypt(string pwd)
        {
            if (string.IsNullOrEmpty(pwd))
            {
                return string.Empty;
            }

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(PASSWORD, SALT);

            Rijndael alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(KEY_LEN);
            alg.IV = pdb.GetBytes(IV_LEN);

            ICryptoTransform trans = alg.CreateEncryptor();

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, trans, CryptoStreamMode.Write))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(pwd);

                    cs.Write(bytes, 0, bytes.Length);
                    cs.FlushFinalBlock();

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static string Decrypt(string strData)
        {
            if (string.IsNullOrEmpty(strData))
            {
                return string.Empty;
            }

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(PASSWORD, SALT);

            Rijndael alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(KEY_LEN);
            alg.IV = pdb.GetBytes(IV_LEN);

            ICryptoTransform trans = alg.CreateDecryptor();

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, trans, CryptoStreamMode.Write))
                    {
                        byte[] bytes = Convert.FromBase64String(strData);

                        cs.Write(bytes, 0, bytes.Length);
                        cs.FlushFinalBlock();

                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
