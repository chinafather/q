using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pixysoft.Framework.Reflection;
using Pixysoft.Framework.Reflection.Core;
using System.Collections;
namespace Cares.FidsII.Util
{
    public static class ObjectCompare
    {
        /// <summary>
        /// 比较二个实体对象的public属性不同点，source和dest类型必需相同
        /// 对象属性集合放在方法外获取目的是提高比较速度 否则每次比较都要获取一次.
        /// 动态机制速度比纯反射提高不少
        /// IDynamicPropertyInfo[] pis1 = new DynamicType(typeof(T)).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        /// </summary>
        /// <param name="source">修改后类</param>
        /// <param name="dest">修改前类</param>
        /// <returns>返回字符型比较结果不同</returns>
        public static string CompareObjectByDynamicProperty(object source, object dest, IDynamicPropertyInfo[] pis1)
        {
            string ReturnString = "";
            
            if (pis1 != null)//如果获得了属性
            {
                for (int i = 0, count = pis1.Length; i < count; i++)//针对每一个属性进行循环
                {
                    object val1, val2;
                    bool CompareTrue;//比较结果
                    string Name;
                    Name = pis1[i].Name;//获取对应属性名
                    val1 = pis1[i].GetValue(source, null);//获取源值
                    val2 = pis1[i].GetValue(dest, null);//获取目标值

                    CompareTrue = true;//默认比较一样
                    if (val1 != null)//可以理解为没有赋值的不进行比较
                    {
                        #region 源不为空
                        if (val1.GetType() == typeof(string))//如果是字符型直接比较
                        {
                            string int1, int2;
                            int1 = (string)val1;
                            int2 = (string)val2;
                            if (int2 == null)
                            {
                                int2 = "";
                            }
                            if (int1 != int2)
                            {
                                CompareTrue = false;
                            }

                        }
                        else if (val1.GetType() == typeof(int))//如果是数字型直接比较
                        {
                            int int1, int2;
                            int1 = (int)val1;
                            int2 = (int)val2;
                            if (int1 != int2)
                            {
                                CompareTrue = false;
                            }
                        }
                        else if (val1.GetType() == typeof(DateTime))//如果是时间型直接比较
                        {
                            DateTime int1, int2;
                            int1 = (DateTime)val1;
                            int2 = (DateTime)val2;
                            if (int1 != int2)
                            {
                                CompareTrue = false;

                            }
                        }
                        else if (val1.GetType() == typeof(byte))//如果是字节直接比较
                        {
                            byte int1, int2;
                            int1 = (byte)val1;
                            int2 = (byte)val2;
                            if (int1 != int2)
                            {
                                CompareTrue = false;
                            }
                        }
                        else if (val1.GetType() == typeof(bool))//如果是BOOL直接比较
                        {
                            bool int1, int2;
                            int1 = (bool)val1;
                            int2 = (bool)val2;
                            if (int1 != int2)
                            {
                                CompareTrue = false;
                            }
                        }
                        else//其他类型不比较
                        {
                            continue;
                        }
                        if (CompareTrue == false)//值不同
                        {
                            ReturnString += Name + ";";
                        }
                        #endregion //源不为空
                    }
                    else if (val1 == null && val2 != null)//目标不为空则一定需要更新
                    {
                        CompareTrue = false;
                    }
                }
            }
            return ReturnString;

        }

        public static Dictionary<IDynamicPropertyInfo, String> CompareObjectByDynamicPropertyDic(object source, object dest, IDynamicPropertyInfo[] pis1)
        {
            Hashtable ht = new Hashtable();
            Dictionary<IDynamicPropertyInfo, String> dic = new Dictionary<IDynamicPropertyInfo, string>();
            string ReturnString = "";

            if (pis1 != null)//如果获得了属性
            {
                for (int i = 0, count = pis1.Length; i < count; i++)//针对每一个属性进行循环
                {
                    object val1, val2;
                    bool CompareTrue;//比较结果
                    string Name;
                    Name = pis1[i].Name;//获取对应属性名
                    val1 = pis1[i].GetValue(source, null);//获取源值
                    val2 = pis1[i].GetValue(dest, null);//获取目标值

                    CompareTrue = true;//默认比较一样
                    if (val1 != null)//可以理解为没有赋值的不进行比较
                    {
                        #region 源不为空
                        if (val1.GetType() == typeof(string))//如果是字符型直接比较
                        {
                            string int1, int2;
                            int1 = (string)val1;
                            int2 = (string)val2;
                            if (int2 == null)
                            {
                                int2 = "";
                            }
                            if (int1 != int2)
                            {
                                CompareTrue = false;
                                dic.Add(pis1[i], int2);
                            }

                        }
                        else if (val1.GetType() == typeof(int))//如果是数字型直接比较
                        {
                            int int1, int2;
                            int1 = (int)val1;
                            int2 = (int)val2;
                            if (int1 != int2)
                            {
                                CompareTrue = false;
                            }
                        }
                        else if (val1.GetType() == typeof(DateTime))//如果是时间型直接比较
                        {
                            DateTime int1, int2;
                            int1 = (DateTime)val1;
                            int2 = (DateTime)val2;
                            if (int1 != int2)
                            {
                                CompareTrue = false;

                            }
                        }
                        else if (val1.GetType() == typeof(byte))//如果是字节直接比较
                        {
                            byte int1, int2;
                            int1 = (byte)val1;
                            int2 = (byte)val2;
                            if (int1 != int2)
                            {
                                CompareTrue = false;
                            }
                        }
                        else if (val1.GetType() == typeof(bool))//如果是BOOL直接比较
                        {
                            bool int1, int2;
                            int1 = (bool)val1;
                            int2 = (bool)val2;
                            if (int1 != int2)
                            {
                                CompareTrue = false;
                            }
                        }
                        else//其他类型不比较
                        {
                            continue;
                        }
                        if (CompareTrue == false)//值不同
                        {
                            ReturnString += Name + ";";
                        }
                        #endregion //源不为空
                    }
                    else if (val1 == null && val2 != null)//目标不为空则一定需要更新
                    {
                        CompareTrue = false;
                        dic.Add(pis1[i], (string)val2);
                    }
                }
            }
            return dic;

        }
    }
}
