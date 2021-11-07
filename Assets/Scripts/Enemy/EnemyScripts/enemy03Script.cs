using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy03Script : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float minMoveTime;
    [SerializeField]
    private float maxMoveTime;
    private float cooldownCount = 0;
    [SerializeField]
    private float minDistance;
    [SerializeField]
    private float maxDistance;

    private Rigidbody2D rb;
    private List<Vector2> availableDirections;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        availableDirections = new List<Vector2>();
    }

    void Update()
    {
        cooldownCount -= Time.deltaTime;

        if (cooldownCount <= 0)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    availableDirections.Add(new Vector2(x, y));
                }
            }
            cooldownCount = Random.Range(minMoveTime, maxMoveTime);
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            for (int i = 0; i < availableDirections.Count; i++)
            {
                float moveDistance = Random.Range(minDistance, maxDistance);
                Vector2 direction = availableDirections[Random.Range(0, availableDirections.Count)];
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, moveDistance + 0.15f, layerMask);
                Vector2 pos = transform.position;
                if (hit == false)
                {
                    //Debug.DrawLine(transform.position, transform.position + (new Vector3(direction.x, direction.y, 0) * (moveDistance + 0.15f)), Color.green, 5f);
                    LeanTween.move(gameObject, pos + (direction * moveDistance), cooldownCount);
                    break;
                }
                else
                {
                    // Debug.DrawLine(transform.position, transform.position + (new Vector3(direction.x, direction.y, 0) * (moveDistance + 0.15f)), Color.red, 5f);
                    availableDirections.Remove(direction);
                }
            }
            availableDirections.Clear();
        }
    }
    private void OnValidate()
    {
        if (minDistance <= 0)
        {
            minDistance = 0.001f;
        }
        else if (minDistance > maxDistance)
        {
            minDistance = maxDistance - 0.0001f;
        }
        else if (maxDistance <= 0)
        {
            maxDistance = 0.001f;
        }
        else if (minMoveTime <= 0)
        {
            minMoveTime = 0.001f;
        }
        else if (minMoveTime >= maxMoveTime)
        {
            minMoveTime = maxMoveTime - 0.0001f;
        }
        else if (maxMoveTime <= 0)
        {
            maxMoveTime = 0.001f;
        }
    }
}