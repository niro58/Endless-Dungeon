using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy03Script : MonoBehaviour // Fly that just goes straight to player
{
    [Header("Movement")]
    public float speed;

    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        target = GlobalVar.Player;
    }

    void Update()
    {
        Vector3 heading = target.transform.position - transform.position;
        Vector3 direction = heading / heading.magnitude;

        direction /= 10000;
        transform.position += direction * speed;
    }
}