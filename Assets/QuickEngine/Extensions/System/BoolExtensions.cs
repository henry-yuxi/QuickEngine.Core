using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Boolean extensions
/// </summary>
public static class BoolExtensions
{

    public static bool IsTrue(this bool item)
    {
        return item;
    }

    public static bool IsFalse(this bool item)
    {
        return !item;
    }

    public static bool Toggle(this bool item)
    {
        return !item;
    }

/// <summary>
/// 
/// </summary>
/// <param name="item"></param>
/// <returns>1=>true 0=>false</returns>
    public static int ToInt(this bool item)
    {
        return item ? 1 : 0;
    }



    public static string ToLowerString(this bool item)
    {
        return item.ToString().ToLower();
    }



    public static string ToYesNo(this bool item)
    {
        return item.ToString("Yes", "No");
    }



    public static string ToString(this bool item, string trueString, string falseString)
    {
        return item.ToType<string>(trueString, falseString);
    }



    public static T ToType <T>(this bool item, T trueValue, T falseValue)
    {
        return item ? trueValue : falseValue;
    }

}
