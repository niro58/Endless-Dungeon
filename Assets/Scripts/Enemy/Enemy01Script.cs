using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Script : MonoBehaviour // enemy that follows player by pathfinding
{
    private List<Vector3> route = new List<Vector3>();
    
    private PathFinding pathfindingScript;
    private CustomGrid pathfindingGrid;

    private EnemyStats stats;
    private Animator anim;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        stats = gameObject.GetComponent<EnemyStats>();
        pathfindingScript = gameObject.GetComponent<PathFinding>();
        anim = stats.animator;
        speed = stats.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(pathfindingGrid == null && pathfindingScript.map != null)
        {
            pathfindingGrid = pathfindingScript.map;
        }
        route = pathfindingScript.route;
        if(route.Count > 0)
        {
            if (Vector2.Distance(transform.position, route[route.Count - 1]) < 0.01f)// If distance between player and the last node is 0.01f, remove the last node
            {
                route.RemoveAt(route.Count - 1);
            }
            if(route.Count != 0)// If there's still more nodes, move towards the next node
            {
                anim.SetBool("isMoving", true);
                transform.position = Vector2.MoveTowards(transform.position, route[route.Count - 1], speed * Time.deltaTime);
            }
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }
}
