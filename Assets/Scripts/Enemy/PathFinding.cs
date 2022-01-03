﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PathFinding : MonoBehaviour // Pathfinding V.1 Still working on
{
    public float cooldown;
    private float cooldownTime;
    private float cooldownTimeCooldown;
    public GameObject checkerPrefab;// can be deleted

    public GameObject target;
    private Vector2Int lastTargetNode;
    private Vector2Int targetNode;

    public List<Vector3> route = new List<Vector3>();

    [HideInInspector]
    public CustomGrid map;
    private GameObject currentRoom;
    private void Start()
    {
        if(target == null)
        {
            target = GlobalVar.Player;
        }
        currentRoom = GlobalVar.CurrentRoom;
    }
    void Update()
    {
        map = GlobalVar.RoomMap;
        cooldownTime += Time.deltaTime;
        if(currentRoom != null && cooldownTime > cooldownTimeCooldown)
        {
            cooldownTimeCooldown = UnityEngine.Random.Range(0.1f, cooldown);
            cooldownTime = 0;
            targetNode = map.WorldToCell(target.transform.position);
            if (currentRoom == GlobalVar.CurrentRoom && (targetNode != lastTargetNode || route.Count() == 0) && map.IsAvailable(targetNode))
            {// If global variable currentRoom is not the same as currentRoom && ( targetNode is not the same as lastTargetNode || route list is empty) && if target node is available
                lastTargetNode = targetNode;
                route = getPath(map.WorldToCell(gameObject.transform.position), lastTargetNode);
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
        else if(route.Count != 0 && currentRoom != GlobalVar.CurrentRoom)
        {
            route.Clear();
        }
    }
    List<Vector3> getPath(Vector2Int startNode,Vector2Int targetNode)
    {
        Vector2Int start = startNode;
        Vector2Int target = targetNode;
        List<nodeInfo> openNodes = new List<nodeInfo>();
        List<nodeInfo> closedNodes = new List<nodeInfo>();
        nodeInfo workNode = new nodeInfo(start, (int)Vector2.Distance(target, start), start);
        openNodes.Add(workNode);

        for (int i = 0; i < GameObject.Find("Route").transform.childCount; i++)
        {
            Destroy(GameObject.Find("Route").transform.GetChild(i).gameObject);
        }
        while (openNodes.Count > 0)
        {
            nodeInfo bestOpenNode = openNodes[0];

            foreach (nodeInfo node in openNodes)
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
                return getRoute(closedNodes);
            }
            foreach (Vector2Int node in map.GetNodesAroundNode(bestOpenNode.Node))
            {
                nodeInfo nodeInfo = new nodeInfo(new Vector2Int(node.x,node.y), priceCalculation(node, start, target, bestOpenNode.Node), bestOpenNode.Node);
                Vector2Int nodeInt = new Vector2Int(node.x,node.y);
                if (!map.IsAvailable(nodeInt) || closedNodes.Any(pathNodeInfo => pathNodeInfo.Node == nodeInfo.Node))
                {
                }else 
                {
                    if (openNodes.Contains(nodeInfo) == false || nodeInfo.f_cost < priceCalculation(nodeInfo.Node, start, target, bestOpenNode.Node))
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
    List<Vector3> getRoute(List<nodeInfo> routeRaw)
    {
        List<Vector3> route = new List<Vector3>();
        List<nodeInfo> routeObj = new List<nodeInfo>();
        routeObj.Add(routeRaw.Last());
        route.Add(map.CellToWorld(routeRaw.Last().Node));
        int x = 0;
        bool endWhile = false;
        while( x < routeRaw.Count())
        {
            x++;
            foreach(nodeInfo node in routeRaw)
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
    private class nodeInfo
    {
        public Vector2Int Node;
        public float f_cost;
        public Vector2Int parent;
        public nodeInfo(Vector2Int Node, float f_cost, Vector2Int parent)
        {
            this.Node = Node;
            this.f_cost = f_cost;
            this.parent = parent;
        }
    }
    private float priceCalculation(Vector2Int node, Vector2Int start, Vector2Int target, Vector2Int parent)
    {
        return Vector2.Distance(target, node) + Vector2.Distance(start, node) + Vector2.Distance(node,parent);
    }

}
