using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairMovement : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.position = Input.mousePosition;
    }
}
