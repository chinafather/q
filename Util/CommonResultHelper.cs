using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cares.FidsII.Util
{
    public static class CommonResultHelper
    {
        public static String DealResultInfo(int[] iMessageCodes,String[] strMessageBodies) 
        {
            String strRet = "";
            for (int i = 0; i < iMessageCodes.Length; i++)
            {
                if (iMessageCodes[i] == 1)
                {
                    strRet += "success";
                }
                else if (iMessageCodes[i] == -11)
                {
                    strRet += "failno";
                }
                else if (iMessageCodes[i] == -12)
                {
                    strRet += "failexist";
                }
                else if (iMessageCodes[i] == -1)
                {
                    strRet += "failpri";
                }
                else if (iMessageCodes[i] == -90013)
                {
                    strRet += "failbytree";
                }
                else if (iMessageCodes[i] == -90012)
                {
                    strRet += "failbytree";
                }
            }
            return strRet;
        }
    }
}
