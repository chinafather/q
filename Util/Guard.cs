using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cares.FidsII.Util
{
    public static class Guard
    {

        public static void ArgumentNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
                throw new ArgumentNullException(argumentName);
        }

        public static void ArgumentNotNullOrEmptyString(string argumentValue, string argumentName)
        {
            ArgumentNotNull(argumentValue, argumentName);
            if (argumentValue.Trim().Length == 0)
                throw new ArgumentException("不能为空", argumentName); //此处需要添加语言集
                //throw new ArgumentException(Resources.StringCannotBeEmpty, argumentName);
        }

        public static void ArgumentNotEmptyGuid(Guid argumentValue, string argumentName)
        {
            ArgumentNotNull(argumentValue, argumentName);
            if (argumentValue == Guid.Empty)
                throw new ArgumentException("不能为空", argumentName); //此处需要添加语言集
                //throw new ArgumentException(Resources.StringCannotBeEmpty, argumentName);
        }
    }
}
