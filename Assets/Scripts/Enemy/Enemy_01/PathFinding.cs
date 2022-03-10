using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PathFinding : MonoBehaviour // Pathfinding V.1 Still working on
{
    public float cooldown;
    private float cooldownTimeRemaining;
    private float randCooldownTimer;
    public GameObject checkerPrefab;// can be deleted

    public GameObject target;
    private Vector2Int lastTargetNode;
    private Vector2Int targetNode;

    public List<Vector3> route = new List<Vector3>();

    [HideInInspector]
    public EnemyGrid map;
    private GameObject currentRoom;
    private void Start()
    {
        if(target == null)
        {
            target = GlobalVar.player.player;
        }
    }
    void Update()
    {
        currentRoom = GlobalVar.currentRoom;
        map = GlobalVar.roomMap;
        cooldownTimeRemaining += Time.deltaTime;
        if(currentRoom != null && cooldownTimeRemaining > randCooldownTimer )
        {
            randCooldownTimer = UnityEngine.Random.Range(0.1f, cooldown);
            cooldownTimeRemaining = 0;
            targetNode = map.WorldToCell(target.transform.position);
            if ((targetNode != lastTargetNode || route.Count() == 0) && map.IsAvailable(targetNode))
            {// If global variable currentRoom is not the same as currentRoom && ( targetNode is not the same as lastTargetNode || route list is empty) && if target node is available
                lastTargetNode = targetNode;
                route = GetRawRoute(map.WorldToCell(gameObject.transform.position), lastTargetNode);
                /*
                foreach (Vector3 node in route)
                {
                    GameObject routeDraw = Instantiate(checkerPrefab);
                    routeDraw.GetComponent<SpriteRenderer>().color = Color.black;
                    routeDraw.name = node.x + " : " + node.y;
                    routeDraw.transform.position = node;
                    //Debug.Log(routeDraw.transform.position);
                    routeDraw.transform.parent = GameObject.Find("Not Important").transform.GetChild(1);
                }*/
            }
        }
        else if(route.Count != 0 && currentRoom != GlobalVar.currentRoom)
        {
            route.Clear();
        }
    }
    List<Vector3> GetRawRoute(Vector2Int startNode,Vector2Int targetNode)
    {
        Vector2Int start = startNode;
        Vector2Int target = targetNode;
        List<NodeInfo> openNodes = new List<NodeInfo>();
        List<NodeInfo> closedNodes = new List<NodeInfo>();
        NodeInfo workNode = new NodeInfo(start, (int)Vector2.Distance(target, start), start);
        openNodes.Add(workNode);

        for (int i = 0; i < GameObject.Find("Route").transform.childCount; i++)
        {
            Destroy(GameObject.Find("Route").transform.GetChild(i).gameObject);
        }
        while (openNodes.Count > 0)
        {
            NodeInfo bestOpenNode = openNodes[0];

            foreach (NodeInfo node in openNodes)
            {
                if(node.f_cost < bestOpenNode.f_cost)
                {
                    bestOpenNode = node;

                }
            }
            route.Add(map.CellToWorld(bestOpenNode.Node));
            
            openNodes.Remove(bestOpenNode);
            closedNodes.Add(bestOpenNode);
            if (bestOpenNode.Node == target)
            {
                return GetRoute(closedNodes);
            }
            foreach (Vector2Int node in map.GetNodesAroundNode(bestOpenNode.Node))
            {
                NodeInfo nodeInfo = new NodeInfo(new Vector2Int(node.x,node.y), PriceCalculation(node, start, target, bestOpenNode.Node), bestOpenNode.Node);
                Vector2Int nodeInt = new Vector2Int(node.x,node.y);
                if (!map.IsAvailable(nodeInt) || closedNodes.Any(pathNodeInfo => pathNodeInfo.Node == nodeInfo.Node))
                {
                }else 
                {
                    if (openNodes.Contains(nodeInfo) == false || nodeInfo.f_cost < PriceCalculation(nodeInfo.Node, start, target, bestOpenNode.Node))
                    {
                        if (!openNodes.Any(pathNodeInfo => pathNodeInfo.Node == nodeInfo.Node))
                        {
                            /*
                            GameObject routeDraw = Instantiate(checkerPrefab);
                            routeDraw.GetComponent<SpriteRenderer>().color = Color.blue;
                            routeDraw.transform.position = map.CellToWorld(nodeInfo.Node);
                            routeDraw.transform.parent = GameObject.Find("Route").transform;
                            */
                            openNodes.Add(nodeInfo);
                        }
                    }
                }
            }

        }
        return new List<Vector3>();
    }
    List<Vector3> GetRoute(List<NodeInfo> routeRaw)
    {
        List<Vector3> route = new List<Vector3>();
        List<NodeInfo> routeObj = new List<NodeInfo>();
        routeObj.Add(routeRaw.Last());
        route.Add(map.CellToWorld(routeRaw.Last().Node));
        int x = 0;
        bool endWhile = false;
        while( x < routeRaw.Count())
        {
            x++;
            foreach(NodeInfo node in routeRaw)
            {
                if(node.Node == routeObj.Last().parent)
                {
                    if (node.Node == map.WorldToCell(transform.position))
                    {
                        endWhile = true;
                        break;
                    }
                    route.Add(map.CellToWorld(node.Node));
                    routeObj.Add(node);
                }
                
            }
            if (endWhile)
            {
                break;
            }
        }
        return route;
    }
    private class NodeInfo
    {
        public Vector2Int Node;
        public float f_cost;
        public Vector2Int parent;
        public NodeInfo(Vector2Int Node, float f_cost, Vector2Int parent)
        {
            this.Node = Node;
            this.f_cost = f_cost;
            this.parent = parent;
        }
    }
    private float PriceCalculation(Vector2Int node, Vector2Int start, Vector2Int target, Vector2Int parent)
    {
        float price = Vector2.Distance(target, node) + Vector2.Distance(start, node) + Vector2.Distance(node, parent);
        return price;
    }

}
