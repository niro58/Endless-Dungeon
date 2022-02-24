using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02Script : MonoBehaviour // Enemy that goes in random directions
{
    private EnemyStats stats;
    [Header("Movement")]
    private float speed;
    [SerializeField]
    private float speedRandOffset;
    private float cooldownCount = 0;
    [SerializeField]
    private float minDistance;
    [SerializeField]
    private float maxDistance;

    private List<Vector2> availableDirections;
    private Vector3 moveBy;

    private EnemyGrid roomMap;
    // Start is called before the first frame update
    void Start()
    {
        stats = gameObject.GetComponent<EnemyStats>();
        speed = stats.speed;
        availableDirections = new List<Vector2>();

        roomMap = GlobalVar.roomMap;

    }

    void Update()
    {
        speed = stats.speed;
        cooldownCount -= Time.deltaTime;
        transform.position += moveBy;
        if(cooldownCount <= 0)
        {
            cooldownCount -= Time.deltaTime;
            transform.position += moveBy;
            if (cooldownCount <= 0)
            {
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        availableDirections.Add(new Vector2(x, y));
                    }
                }
                cooldownCount = Random.Range(speed - speedRandOffset, speed + speedRandOffset);
                int layerMask = 1 << 9 | 1 << 6;
                layerMask = ~layerMask;
                for (int i = 0; i < availableDirections.Count; i++)
                {
                    float moveDistance = Random.Range(minDistance, maxDistance);
                    Vector2 direction = availableDirections[Random.Range(0, availableDirections.Count)];
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, moveDistance + 0.3f, layerMask);
                    Vector2 pos = transform.position;
                    if (hit == false)
                    {
                        //Debug.DrawLine(transform.position, transform.position + (new Vector3(direction.x, direction.y, 0) * (moveDistance + 0.15f)), Color.green, 5f);

                        LeanTween.move(gameObject, pos + (direction * moveDistance), cooldownCount);
                        break;
                    }
                    else
                    {
                        //Debug.DrawLine(transform.position, transform.position + (new Vector3(direction.x, direction.y, 0) * (moveDistance + 0.15f)), Color.red, 5f);
                        availableDirections.Remove(direction);
                    }
                }
                availableDirections.Clear();
            }
        }
    }
}
