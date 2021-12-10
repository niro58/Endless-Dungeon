using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public Gun.BulletType bulletType;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public int damage;
    [HideInInspector]
    public float range;

    private Vector3 startPos;
    void Start()
    {
        damage = GlobalVar.playerStats.Damage;
        speed = GlobalVar.playerStats.BulletSpeed;
        range = GlobalVar.playerStats.BulletRange;
        startPos = transform.position;
    }
    void Update()
    {
        if(Vector2.Distance(startPos, transform.position) >= range)
        {
            Destroy(gameObject);
        }
        else
        {
            switch (bulletType)// Bullet movement, it is set on bullet creation in Gun.cs
            {
                case Gun.BulletType.Straight:
                    transform.position += transform.right * (speed / 50);
                    break;
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer != 8 || col.gameObject.layer != 9)
        {
            Destroy(gameObject);
        }
    }
}
