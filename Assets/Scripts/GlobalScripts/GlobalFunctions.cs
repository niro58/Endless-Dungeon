using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalFunctions
{
    public static List<int> getRandomValues(int min, int max, int amount)
    {
        List<int> values = new List<int>();
        while(values.Count < amount)
        {
            int randNumber = Random.Range(min, max);
            if (!values.Contains(randNumber))
            {
                values.Add(randNumber);
            }
        }

        return values;
    }
}
