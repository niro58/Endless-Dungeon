using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerationCall : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D hit)
    {
        // On collision with door it moves the player forward to next room
        if (hit.collider.gameObject.tag == "Door")
        {
            Vector2Int newRoomDirection = hit.gameObject.GetComponent<DoorStats>().doorDirectionVector;
            StartCoroutine(moveToNextRoom(newRoomDirection, hit));
        }
    }

    IEnumerator moveToNextRoom(Vector2Int dir, Collision2D hit)
    {
        if (GameObject.FindGameObjectsWithTag("Scripts")[0].GetComponent<RoomGeneration>().generateNextRoom(dir, hit.transform.parent.parent.position))
        {
            GlobalVar.canMove = false;
            yield return new WaitForSeconds(0.3f);
            GlobalVar.canMove = true;
        }

        transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, transform.position.z);
        Vector3 movePos = Vector2.Scale(dir, new Vector2(0.7f, 0.7f));
        transform.position += movePos;

        Vector2Int inversedDir = Vector2Int.Scale(dir, new Vector2Int(-1, -1));
        int layerMask = 1 >> 10;
        layerMask = ~layerMask;
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, inversedDir, 0.5f, layerMask);
        if (rayHit)
        {
            Debug.Log(rayHit.collider.name);
            GlobalVar.currentRoom = rayHit.transform.parent.parent.parent.gameObject;
        }
    }
}
