using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
public static class GlobalFunctions
{
    public static List<int> GetRandomValues(int min, int max, int amount)
    {
        List<int> values = new List<int>();
        while(values.Count < amount)
        {
            int randNumber = UnityEngine.Random.Range(min, max);
            if (!values.Contains(randNumber))
            {
                values.Add(randNumber);
            }
        }

        return values;
    }
    public static string SplitByCapitalLetters(string input)
    {
        string[] splitString = Regex.Split(input, @"(?<!^)(?=[A-Z])");
        string returnString = "";
        foreach(string part in splitString)
        {
            returnString += part + " ";
        }
        return returnString;
    }
    public static void SetAllBehaviour(GameObject target, bool status)
    {
        foreach(Behaviour collider in target.GetComponents<Behaviour>())
        {
            collider.enabled = status;
        }
    }
}
