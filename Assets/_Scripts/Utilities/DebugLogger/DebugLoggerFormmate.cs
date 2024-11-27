using System;

namespace Core.Utility.DebugTool
{
    public static class DebugLoggerFormat
    {
        public static string Color(this string str, string color)
        {
            return $"<color={color}>{str}</color>";
        }

        public static string Color(this object strObj, HtmlDebugColor color)
        {
            if (strObj is null)
            {
                return "";
            }

            string newStr;

            try
            {
                newStr = strObj.ToString();
            }
            catch
            {
                throw new Exception("Cannot convert object to string!");
            }

            if (color is HtmlDebugColor.Bool)
            {
                if (strObj is bool result)
                {
                    color = result ? HtmlDebugColor.Green : HtmlDebugColor.Red;
                }
                else
                {
                    color = HtmlDebugColor.Gray;
                }
            }

            string resultColor = color.ToString();
            return $"<color={resultColor}>{newStr}</color>";
        }

        public static string Size(this string str, int fontSize)
        {
            return $"<size={fontSize}>{str}</size>";
        }

        public static string Size(this object strObj, int fontSize)
        {
            string newStr;
            try
            {
                newStr = strObj.ToString();
            }
            catch
            {
                throw new Exception("Cannot convert object to string!");
            }

            return $"<size={fontSize}>{newStr}</size>";
        }
    }
}