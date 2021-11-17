using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public Gun.bulletType BulletType;
    [HideInInspector]
    public float Speed;
    [HideInInspector]
    public int Damage;
    // Start is called before the first frame update
    void Start()
    {
        Damage = GlobalVar.sumStats.Damage;
        Speed = GlobalVar.sumStats.BulletSpeed;
    }
    void Update()
    {
        switch (BulletType)// Bullet movement, it is set on bullet creation in Gun.cs
        {
            case Gun.bulletType.Straight:
                transform.position += transform.right * (Speed / 50);
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer != 8 || col.gameObject.layer != 8)
        {
            Destroy(gameObject);
        }
    }
}
