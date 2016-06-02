 //****************************************************************************
 //** �ļ���: GUIDHelp.cs	                                          
 //**         Copyright 2005 by Shanghai HJ Software System Co.,Ltd            
 //**         All Rights Reserved 	                                          
 //** ������: Michael	                                                      
 //** �� �� : 2005-8-1 
 //** �޸���:	                                                              
 //** �� �� :	                                                              
 //** �� �� :	                                                              
 //**       
 //** �� �� :		                                                              
 //****************************************************************************
using System;

namespace Weegle.FrameWork.WeegleFrameWork
{
	/// <summary>
	/// GUIDHelp ��ժҪ˵����
	/// </summary>
	public class GUIDHelp
	{
		public GUIDHelp()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ���� GUID �������ݿ�������ض���ʱ����������߼���Ч��
		/// </summary>
		/// <returns>COMB (GUID ��ʱ������) ���� GUID ����</returns>
		public static Guid NewComb() 
		{ 
			byte[] guidArray = System.Guid.NewGuid().ToByteArray(); 
			DateTime baseDate = new DateTime(1900,1,1); 
			DateTime now = DateTime.Now; 
			// Get the days and milliseconds which will be used to build the byte string 
			TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks); 
			TimeSpan msecs = new TimeSpan(now.Ticks - (new DateTime(now.Year, now.Month, now.Day).Ticks)); 

			// Convert to a byte array 
			// Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
			byte[] daysArray = BitConverter.GetBytes(days.Days); 
			byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds/3.333333)); 

			// Reverse the bytes to match SQL Servers ordering 
			Array.Reverse(daysArray); 
			Array.Reverse(msecsArray); 

			// Copy the bytes into the guid 
			Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2); 
			Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4); 

			return new System.Guid(guidArray); 
		} 

		/// <summary>
		/// �� SQL SERVER ���ص� GUID ������ʱ����Ϣ
		/// </summary>
		/// <param name="guid">����ʱ����Ϣ�� COMB </param>
		/// <returns>ʱ��</returns>
		public static DateTime GetDateFromComb(System.Guid guid) 
		{ 
			DateTime baseDate = new DateTime(1900,1,1); 
			byte[] daysArray = new byte[4]; 
			byte[] msecsArray = new byte[4]; 
			byte[] guidArray = guid.ToByteArray(); 
            
			// Copy the date parts of the guid to the respective byte arrays. 
			Array.Copy(guidArray, guidArray.Length - 6, daysArray, 2, 2); 
			Array.Copy(guidArray, guidArray.Length - 4, msecsArray, 0, 4); 

			// Reverse the arrays to put them into the appropriate order 
			Array.Reverse(daysArray); 
			Array.Reverse(msecsArray); 

			// Convert the bytes to ints 
			int days = BitConverter.ToInt32(daysArray, 0); 
			int msecs = BitConverter.ToInt32(msecsArray, 0); 

			DateTime date = baseDate.AddDays(days); 
			date = date.AddMilliseconds(msecs * 3.333333); 

			return date; 
		}
	}
}
