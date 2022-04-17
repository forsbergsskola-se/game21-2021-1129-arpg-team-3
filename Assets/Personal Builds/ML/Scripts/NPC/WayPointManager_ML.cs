using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

[System.Serializable]
public struct Link_ML
{
    public enum Direction_ML
    {
        UNI,
        BI
    }

    public GameObject node1;
    public GameObject node2;
    public Direction_ML dir;

}

public class WayPointManager_ML : MonoBehaviour
{
    public GameObject[] waypoints;
//    public Link[] links;

    private List<GameObject> WayPoints;
//    public Graph graph = new Graph();
    public int CurrentNumberPoints { get; private set; }
    public GameObject CurrentWayPoint { get; private set; }

    public Vector3 GetLocationOfPoint(int index)
    {
        return   CurrentWayPoint.GetComponent<WayPointDebug_ML>().GetLocationOfPoint(index);
    }

    
    void Start()
    {
        WayPoints = GameObject.FindGameObjectsWithTag("wp").ToList();
        CurrentWayPoint = WayPoints[0];
        CurrentNumberPoints = CurrentWayPoint.GetComponent<WayPointDebug_ML>().PointList.Count;
        CurrentWayPoint.GetComponent<WayPointDebug_ML>().GetLocationOfPoint(0);
        
        /**
        if (waypoints.Length > 0)
        {
            foreach (var el in waypoints)
            {
                graph.AddNode(el);
            }

            foreach (var el in links)
            {
                graph.AddEdge(el.node1, el.node2);
                if (el.dir == Link.Direction.BI)
                {
                    graph.AddEdge(el.node2, el.node1);
                }
            }
        }
        **/
    }
    
    
    void Update()
    {
    //    graph.debugDraw();
    }
}
