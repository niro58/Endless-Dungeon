using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public Gun.bulletType BulletType;
    [HideInInspector]
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
