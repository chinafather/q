using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Web;
namespace Weegle.FrameWork.WeegleFrameWork
{
	/// <summary>
	/// EncryptionHelper 的摘要说明。
	/// </summary>
	public class EncryptionHelper
	{
		public EncryptionHelper()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// Base64算法加密
		/// </summary>
		/// <param name="argValue">需要加密的字符串</param>
		/// <returns>加密字符串</returns>
		public static String GetEncryptValueByBase64(String argValue)
		{
			String returnValue;
			returnValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(argValue));
			return returnValue;
		}

		/// <summary>
		/// Base64算法解密
		/// </summary>
		/// <param name="argValue">需要解密的字符串</param>
		/// <returns>解密字符串</returns>
		public static String GetDecryptValueByBase64(String argValue)
		{
			String returnValue;
			returnValue = Encoding.UTF8.GetString(Convert.FromBase64String(argValue));
			return returnValue;
		}
        /// <summary>
        /// 对输入字符串进行加密
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static String EncryptString(string src)
		{
			int iLen;
			string xWCU;
			char xChar;
			int intChar;
			int yintChar;
			char yChar;
			int xChar1;
			int xChar2;
			char yChar1;
			char yChar2;
			int i;
			string ret = "";

			iLen =  src.Length; 
			i = 1;
			xWCU = src;
			for(i = 1;i <= iLen;i ++)
			{
				xChar =  char.Parse(xWCU.Substring(i-1,1));
				intChar = (int)xChar;
				yintChar = intChar;
				if(i%2 == 0)
				{
					yintChar = intChar + 2;
				}
				else
				{
					yintChar = intChar + 1;
				}
				yintChar = yintChar ^ 11;
				yChar = (char)yintChar;
				
				xChar1 = int.Parse(System.Math.Floor((Decimal)yintChar / 15).ToString());
				if(xChar1 >= 10)
				{
					yChar1 = (char)((int)('A') + xChar1-10);
				}
				else
				{
					yChar1 =  char.Parse(xChar1.ToString().Trim());  
				}

				xChar2 = yintChar % 15;
				if(xChar2 >= 10)
				{
					yChar2 = (char)((int)('A') + xChar2-10);
				}
				else
				{
					yChar2 =  char.Parse(xChar2.ToString().Trim());  
				}

				ret = yChar2.ToString()  + yChar1.ToString()  + ret;
			}
			
			return HttpUtility.UrlEncode(HttpUtility.UrlEncode(ret));
		}
        /// <summary>
        /// 对输入字符串进行解密
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static String DecryptString(string src)
		{

			int iLen;
			string xWCU;
			char xChar;
			int intChar;
			int yintChar;
			char yChar;
			int xChar1;
			int xChar2;
			char yChar1;
			char yChar2;
			int i;
			string ret = "";
			src = HttpUtility.UrlDecode(HttpUtility.UrlDecode(src));
			iLen =  src.Length; 
			i = 1;
			xWCU = src;
			xChar1 = 0;
			xChar2 = 0;
			for(i = iLen;i > 0;i=i-2)
			{
				yChar1 =  char.Parse(xWCU.Substring(i-1,1));
				yChar2 =  char.Parse(xWCU.Substring(i-2,1));
				if(yChar1 >= 'A')
				{
					xChar1 = 10 + (int)yChar1 - (int)'A';
				}
				else
				{
					xChar1 = int.Parse(yChar1.ToString());
				}
				if(yChar2 >= 'A')
				{
					xChar2 = 10 + (int)yChar2 - (int)'A';
				}
				else
				{
					xChar2 = int.Parse(yChar2.ToString());
				} 
				yintChar = xChar1 * 15 + xChar2;
				yintChar = yintChar ^ 11 ;
				if((iLen-i+1) % 4 == 0 || (iLen-i+1) % 4 == 3  )
				{
					intChar = yintChar - 2;
				}
				else
				{
					intChar = yintChar - 1;
				}
				xChar = (char)intChar;
				ret = ret + xChar.ToString(); 
			}
			return ret;
		}
	
	}
}
