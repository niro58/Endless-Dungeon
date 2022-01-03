using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunMovement : MonoBehaviour
{
    
    private GameObject gunParent;
    private Vector3 gunScale;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        gunParent = gameObject.transform.Find("Guns").gameObject;
        gunScale = gunParent.transform.localScale;
        startPos = gunParent.transform.localPosition;
    }
    void Update()
    {
        // Gun Rotation part, Dir = gets mouse position -> var angle transforms it into the rotation;
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angleRad = Mathf.Atan2(dir.y, dir.x);
        Quaternion angleAxis = Quaternion.AngleAxis(angleRad * Mathf.Rad2Deg, Vector3.forward);
        Vector3 pos = new Vector3(0, 0, -0.1f);
        if (angleAxis.z > 0)
        {
            pos.z = 0.1f;
        }
        pos.x = (Mathf.Cos(angleRad) * .15f);
        pos.y = (Mathf.Sin(angleRad) * .075f);
        gunParent.transform.localPosition = startPos + pos;
        gunParent.transform.rotation = angleAxis;

        if (gunParent.transform.eulerAngles.z > 90 && gunParent.transform.eulerAngles.z < 270)
        {
            gunParent.transform.localScale = new Vector3(gunScale.x, -gunScale.y, gunScale.z);
        }
        else
        {
            gunParent.transform.localScale = gunScale;
        }
    }
}
