using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsParent : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
