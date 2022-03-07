using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public void Awake()
    {
        GlobalVar.playerStats.LoadData();
        GlobalFunctions.UpdateImportantGameObjects();

        StartCoroutine(GlobalFunctions.MakeTransition("SceneEnter", "Transition_Shrink"));
    }
    public void LoadScene(int scene)
    {
        StartCoroutine(GlobalFunctions.MakeTransition("SceneChange", "Transition_Increase", scene));
    }
}
