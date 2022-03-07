using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunMovement : MonoBehaviour
{
    
    private Transform gunParent;
    private Vector3 gunScale;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        gunParent = GlobalVar.player.gunParent;
        gunScale = gunParent.transform.localScale;
        startPos = gunParent.transform.localPosition;
    }
    void Update()
    {
        if(Time.timeScale != 0)
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
            gunParent.localPosition = startPos + pos;
            gunParent.rotation = angleAxis;

            if (gunParent.eulerAngles.z > 90 && gunParent.eulerAngles.z < 270)
            {
                gunParent.localScale = new Vector3(gunScale.x, -gunScale.y, gunScale.z);
            }
            else
            {
                gunParent.localScale = gunScale;
            }
        }
    }
}
