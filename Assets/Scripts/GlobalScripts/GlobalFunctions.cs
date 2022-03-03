using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System;
using UnityEngine.SceneManagement;
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
    public static bool SaveData(string data, string fileName)
    {
        var path = Path.Combine(Application.persistentDataPath, fileName);
        try
        {
            File.WriteAllText(path, data);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log("Failed To Load Data From " + path + " Error : " + e);
            return false;
        }
    }
    public static bool LoadData(string fileName, out string output)
    {
        var path = Path.Combine(Application.persistentDataPath, fileName);
        
        try
        {
            output = File.ReadAllText(path);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log("Failed To Load Data From " + path + " Error : " + e);
            output = "";
            return false;
        }
    }
    public static bool GetRandBoolean()
    {
        return (UnityEngine.Random.Range(0f, 1f) > 0.5f);
    }
    public static void MakeTransition(string action)
    {
        GameObject prefab = GlobalVar.importantPrefabs["Transition"];
        Animator anim = prefab.GetComponent<Animator>();

        prefab.GetComponent<Animator>().Play("Transition Increase");

        switch (action)
        {
            case "Death":
                break;
            case "MainMenu":
                break;
        }
    }
    public static void UpdateImportantGameObjects()
    {
        Dictionary<string,GameObject> importantObjects = GlobalVar.importantPrefabs;
        importantObjects.Clear();
        foreach(GameObject prefab in GameObject.FindGameObjectsWithTag("ImportantObjects"))
        {
            importantObjects.Add(prefab.name, prefab);
            switch (prefab.name)
            {
                case "Scripts":
                case "Transition":
                    break;
                default:
                    prefab.SetActive(false);
                    break;
            }
        }
        MakeTransition("Death");

    }
}
