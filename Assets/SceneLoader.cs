using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public void LoadScene(int scene)
    {
        while (GlobalVar.importantPrefabs["Transition"].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Transition_Increase") == false)
        {

        }
        SceneManager.LoadScene(scene);
    }
}
