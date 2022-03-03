using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UITransition : MonoBehaviour
{
    Animator anim;
    void OnSceneLoaded()
    {
        anim = GetComponent<Animator>();
        anim.Play("Transition_Shrink");
    }
}
