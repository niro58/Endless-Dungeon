using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingFollowRoute : MonoBehaviour
{
    private List<Vector3> route = new List<Vector3>();
    
    private PathFinding pathfindingScript;
    private CustomGrid pathfindingGrid;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        pathfindingScript = gameObject.GetComponent<PathFinding>();

    }

    // Update is called once per frame
    void Update()
    {
        if(pathfindingGrid == null && pathfindingScript.map != null)
        {
            pathfindingGrid = pathfindingScript.map;
        }
        route = pathfindingScript.route;

        if(route.Count != 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, route[0], speed * Time.deltaTime);
        }
    }
}
