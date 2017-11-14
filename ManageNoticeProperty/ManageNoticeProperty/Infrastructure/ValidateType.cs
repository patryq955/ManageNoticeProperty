using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.Infrastructure
{
    public static class ValidateType
    {
        public static bool isCheck<T>(string text) where T : IConvertible
        {
            var typeT = default(T);
            var typeCode = typeT.GetTypeCode();
            try
            {
                switch (typeCode)
                {
                    case TypeCode.Double:
                        Double.Parse(text);
                        break;
                    case TypeCode.Int32:
                        int.Parse(text);
                        break;
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}