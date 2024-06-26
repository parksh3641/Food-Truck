using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoneyUnitString
{
    static readonly string[] CurrencyUnits = new string[] { "", "k"};

    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    /// 
    public static string ToCurrencyString(long number)
    {
        if (number != 0)
        {
            return string.Format("{0:#,###}", number);
        }
        else
        {
            return "0";
        }

        //if (Mathf.Abs((float)number) >= 1000)
        //{
        //    return (number / 1000f).ToString("F1") + "k";
        //}
        //else
        //{
        //    return number.ToString();
        //}

        //return number.ToString();
    }
    //public static string ToCurrencyString(this double number)
    //{
    //    string zero = "0";

    //    if (-1d < number && number < 1d)
    //    {
    //        return zero;
    //    }

    //    if (double.IsInfinity(number))
    //    {
    //        return "Infinity";
    //    }

    //    //  ???? ???? ??????
    //    //string significant = (number < 0) ? "-" : string.Empty;
    //    string significant = "";

    //    //  ?????? ????
    //    string showNumber = string.Empty;

    //    //  ???? ??????
    //    string unityString = string.Empty;

    //    //  ?????? ?????? ?????? ???? ?????? ???? ?????????? ?????? ?? ????
    //    string[] partsSplit = number.ToString("E").Split('+');

    //    //  ????
    //    if (partsSplit.Length < 2)
    //    {
    //        return zero;
    //    }

    //    //  ???? (?????? ????)
    //    if (!int.TryParse(partsSplit[1], out int exponent))
    //    {
    //        Debug.LogWarningFormat("Failed - ToCurrentString({0}) : partSplit[1] = {1}", number, partsSplit[1]);
    //        return zero;
    //    }

    //    //  ???? ?????? ??????
    //    int quotient = exponent / 3;

    //    //  ???????? ?????? ?????? ?????? ????(10?? ?????????? ????)
    //    int remainder = exponent % 3;

    //    //  1A ?????? ???? ????
    //    if (exponent < 3)
    //    {
    //        showNumber = System.Math.Truncate(number).ToString();
    //    }
    //    else
    //    {
    //        //  10?? ?????????? ?????? ?????? ???????? ?????? ????.
    //        var temp = double.Parse(partsSplit[0].Replace("E", "")) * System.Math.Pow(10, remainder);

    //        //  ???? ?????????????? ????????.
    //        showNumber = temp.ToString("F3");
    //    }

    //    unityString = CurrencyUnits[quotient];

    //    return string.Format("{0}{1}{2}", significant, showNumber, unityString);
    //}
}
