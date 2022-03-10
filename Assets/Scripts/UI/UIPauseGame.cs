using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseGame : MonoBehaviour
{
    public bool pauseOnEnable;
    public bool startOnDisable;
    private void OnEnable()
    {
        if (pauseOnEnable)
        {
            Time.timeScale = 0f;
        }
    }
    private void OnDisable()
    {
        if (startOnDisable)
        {
            Time.timeScale = 1f;
        }
    }
}
