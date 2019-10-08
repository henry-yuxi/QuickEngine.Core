using System;
using UnityEngine;

public class MathUtil
{
    /// <summary>
    /// int-->"k、m"
    /// </summary>
    public static string ResourceCountConversion(long count)
    {
        string num = "";
        float value = 0;
        float temp = 0;
        string str = "";
        if (count >= 1000 && count < 1000000)
        {
            value = (float)count / 1000;
            str = "K";
        }
        else if (count >= 1000000)
        {
            value = (float)count / 1000000;
            str = "M";
        }
        else
        {
            num = string.Format("{0}", count);
            return num;
        }
        temp = Mathf.Floor(value * 10);
        value = (temp * 1.0f) / 10;
        if (temp % 10 == 0)
        {
            num = string.Format("{0}{1}", value, str);
        }
        else
        {
            num = string.Format("{0}{1}", value, str);
        }

        return num;
    }

    public static string ResourceCountConversion(float count)
    {
        string num = "";
        float value = 0;
        float temp = 0;
        string str = "";
        if (count >= 1000 && count < 1000000)
        {
            value = (float)count / 1000;
            str = "K";
        }
        else if (count >= 1000000)
        {
            value = (float)count / 1000000;
            str = "M";
        }
        else
        {
            num = string.Format("{0}", count);
            return num;
        }
        temp = Mathf.Floor(value * 10);
        value = (temp * 1.0f) / 10;
        if (temp % 10 == 0)
        {
            num = string.Format("{0}{1}", value, str);
        }
        else
        {
            num = string.Format("{0}{1}", value, str);
        }

        return num;
    }

    public static string ResourceTimeConversion(int time)
    {
        string str = "";
        string finalStr = "";
        int value = 0;
        int Minute = (time % 60);
        int Hour = time / 60;
        int Day = Hour / 24;
        if (Minute > 0)
        {
            str = "min";
            value = Minute;
        }
        else if (Hour > 0 && Hour < 25)
        {
            str = "h";
            value = Hour;
        }
        else if (Day > 0)
        {
            str = "d";
            value = Day;
        }
        finalStr = string.Format("{0}{1}", value, str);
        return finalStr;
    }

    /// <summary>
    /// 格式化 钻石的数量  返回 10,000,000 这种格式
    /// </summary>
    public static string SetDiamondNum(int num)
    {
        string DiaNum = "";
        string str = num.ToString();
        char[] aar1 = str.ToCharArray(0, str.Length);
        int j = 0;
        for (int i = aar1.Length; i > 0; i--)
        {
            if (j % 3 == 0 && j != 0 && j != aar1.Length)
            {
                DiaNum = "," + DiaNum;
            }
            DiaNum = aar1[i - 1] + DiaNum;
            j++;
        }
        return DiaNum;
    }

    public static string SetDiamondNum(long num)
    {
        string DiaNum = "";
        string str = num.ToString();
        char[] aar1 = str.ToCharArray(0, str.Length);
        int j = 0;
        for (int i = aar1.Length; i > 0; i--)
        {
            if (j % 3 == 0 && j != 0 && j != aar1.Length)
            {
                DiaNum = "," + DiaNum;
            }
            DiaNum = aar1[i - 1] + DiaNum;
            j++;
        }
        return DiaNum;
    }

    //根据数据的位数显示成     45M    4,125  456
    public static string ResourceCountConversion(long count, bool isTrue = true)
    {
        string num = "";
        double value = 0;
        string mark = "";
        string str = count.ToString();
        char[] array = str.ToCharArray(0, str.Length);
        switch (array.Length)
        {
            case 4:
                num = SetDiamondNum(count);
                if (!isTrue)
                {
                    value = count / 1000.0f;
                    //保留小数点后一位
                    value = Math.Floor(value * 10) / 10;
                    mark = "K";
                    num = string.Format("{0}{1}", value, mark);
                }
                break;
            case 5:
            case 6:
                value = count / 1000.0f;
                //保留小数点后一位
                //num = String.Format ("{0:N1}", value);
                value = Math.Floor(value * 10) / 10;
                mark = "K";
                num = string.Format("{0}{1}", value, mark);
                break;
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
                int kNum = 0;
                value = count / 1000000.0f;
                mark = "M";
                if (value >= 1000)
                {
                    kNum = (int)value;
                    num = string.Format("{0}{1}", SetDiamondNum(kNum), mark);
                }
                else
                {
                    value = Math.Floor(value * 10) / 10;
                    num = string.Format("{0}{1}", value, mark);
                }
                break;
            default:
                num = str;
                break;
        }
        return num;
    }

    /// <summary>
    /// 获得距离
    /// </summary>
    public static int GetMapDistance(int startX, int startY, int endX, int endY)
    {
        return Mathf.Abs(startX - endX) + Mathf.Abs(startY - endY);
    }

    public static string DateTimeToTimeString(DateTime t)
    {
        return string.Format("{0}-{1}-{2} {3}:{4}:{5}", t.Year,
            t.Month.ToString().PadLeft(2, '0'),
            t.Day.ToString().PadLeft(2, '0'), t.Hour.ToString().PadLeft(2, '0'),
            t.Minute.ToString().PadLeft(2, '0'), t.Second.ToString().PadLeft(2, '0'));
    }
}
