using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Raisins.Client.Web.Services
{
    public class EnumHelper
    {
        public static List<SelectListItem> GetEnumSelectList<TEnum>(params TEnum[] ignoreList)
        {
            List<SelectListItem> enumList = new List<SelectListItem>();
            foreach (TEnum data in Enum.GetValues(typeof(TEnum)))
            {
                if (!ignoreList.Contains(data))
                {
                    enumList.Add(new SelectListItem
                    {
                        Text = data.ToString(),
                        Value = ((int)Enum.Parse(typeof(TEnum), data.ToString())).ToString()
                    });
                }
            }
            return enumList;
        }

        public static string GetName<TEnum>(string enumValue)
        {
            int enumIntValue = 0;
            bool result = Int32.TryParse(enumValue, out enumIntValue);
            if (result == true)
            {
                return Enum.GetName(typeof(TEnum), enumIntValue);
            }
            else
            {
                return enumValue;
            }
        }
    }
}