using System;

namespace Weegle.FrameWork.WeegleFrameWork
{
	/// <summary>
	/// NumberHelper 的摘要说明。
	/// </summary>
	public class NumberHelper
	{
		public NumberHelper()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public static string GetCaptital(double dblNumeric)
		{
			string N2S = "";
			string strPlace_1 = "";
			string strPlace_2 = "";
			string strPlace_4 = "";
			string strPlace_5 = "";
			string strPlace_6 = "";
			string strPlace_7 = "";
			double intLen = 0;

			strPlace_1 = "零壹贰叁肆伍陆柒捌玖";
			strPlace_2 = "仟佰拾万仟佰拾元角分";

			strPlace_4 = (dblNumeric * 100).ToString().Trim();
			if( strPlace_4.Length  > 10 && dblNumeric > 0 && dblNumeric < 99999999.99 )
			{
				intLen = Convert.ToInt32(dblNumeric * 100) / 100;
				if (dblNumeric - intLen >= 0.005 )
				{
					intLen = intLen + 0.01;
				}
				strPlace_4 = (intLen * 100).ToString().Trim();
			}

			if (strPlace_4.Length  > 10 || dblNumeric < 0 )
				N2S = "大于等于 99999999.99 的数值不能转换";
			else
			{
				strPlace_4 = GetFillBlanks(10 - strPlace_4.Length) + strPlace_4;
				N2S = "";
				for(int i = 0; i < 10; i ++ )
				{
					strPlace_5 = Convert.ToString(strPlace_4[i]);
					if (strPlace_5 != " ")
					{
						strPlace_6 = Convert.ToString(strPlace_1[Convert.ToInt32(strPlace_5)]);
						strPlace_7 = Convert.ToString(strPlace_2[i]);
						if (strPlace_5 == "0" && i != 3 && i != 7)
							strPlace_7 = "";
						if ((i== 8 && strPlace_4.Substring(i, 2) == "00") || (strPlace_5 == "0" && (i == 3 || i == 7 || i == 9)))
							strPlace_6 = "";
						N2S = N2S + strPlace_6 + strPlace_7;
						if (Convert.ToString(strPlace_4[i]) == "0" && strPlace_4[i] != '0' && (i == 3 || i == 7))
							N2S = N2S + "零";
					}
				}
				if (strPlace_5 == "0" && N2S != "")
					N2S = N2S + "整";
			}
			return N2S;
		}

		public static string GetFillBlanks(int num)
		{
			string ret = "";
			for(int i = 0;i < num; i ++ )
			{
				ret += " ";
			}
			return ret;
		}

		public static string GetFillZeros(int num)
		{
			string ret = "";
			for(int i = 0;i < num; i ++ )
			{
				ret += "0";
			}
			return ret;
		}

		public static string IncreaseByDegrees(string initNum)
		{
			try
			{
				string initNum1 = InterceptNumber(initNum,false);
				string initNum2 = initNum.Substring(0,initNum.Length - initNum1.Length);
				int len = initNum1.Length;
				int initNum3 = int.Parse(initNum1) ;
				int len1 = initNum3.ToString().Length;
				initNum3 += 1;
				int len2 = initNum3.ToString().Length;
				return initNum2 + initNum3.ToString(GetFillZeros(len + ( len2 - len1)));
			}
			catch
			{
				return "1";
			}
		}


		public static string InterceptNumber(string original,bool leftToRight)
		{
			string ret = "";
			string model = "1234567890";
			if(leftToRight)
			{
				for(int i = 0;i < original.Length ; i ++ )
				{
					if(model.IndexOf(original[i]) != -1)
					{
						ret += Convert.ToString(original[i]);
					}
					else
					{
						break;
					}
				}
			}
			else
			{
				for(int i = original.Length -1 ; i >= 0; i --)
				{
					if(model.IndexOf(original[i]) != -1)
					{
						ret = ret.Insert(0,Convert.ToString(original[i]));
					}
					else
					{
						break;
					}
				}
			}
			return ret;
		}

	}
}
