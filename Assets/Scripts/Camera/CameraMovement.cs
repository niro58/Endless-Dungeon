using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float CameraSpeed;


    public GameObject player;
    private Vector3 camStartPos;

    void Start()
    {

        camStartPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector2.Distance(player.transform.position,camStartPos) > 0.1f)// Follows the player if he is 0.1f far away from camera
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + camStartPos, CameraSpeed);
        }
    }
}
