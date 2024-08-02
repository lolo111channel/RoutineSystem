using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    private Waypoint[] _waypoints;
    [SerializeField] private Transform _testTransform;

    private void Start()
    {
        _waypoints = GetComponentsInChildren<Waypoint>();

        FindPath(_testTransform.position, "Way09");
    }


    public Waypoint[] FindPath(Vector2 start, string waypointID)
    {
        Waypoint nearestWaypoint = GetNearestPoint(start);
        List<List<Waypoint>> paths = new();
        paths.Add(new() { nearestWaypoint });

        List<List<Waypoint>> test = CreatePaths(nearestWaypoint, waypointID);
        foreach (var a in test)
        {
            string d = "";
            foreach (var b in a)
            {
                d += $"{ b.name } ";
            }

            Debug.Log(d);
        }

        return null;
    }

    int test = 0;
    private List<List<Waypoint>> CreatePaths(Waypoint startWaypoint, string waypointId, Waypoint previouseWay = null, List<Waypoint> previousPath = null)
    {
        test++;
        List<List<Waypoint>> paths = new();

        List<Waypoint> path = new();
        

        foreach (var con in startWaypoint.Connections)
        {
            List<Waypoint> p = new();
            if (previousPath != null)
            {
                p = previousPath;
            }

            if (previouseWay != null)
            {
                if (con.name == previouseWay.name)
                    continue;
            }

 
            
            if (p.Any(x => x.name == con.name))
            {
              
                continue;
                
            }
            


            p.Add(con);
            DisplayPath(p,$"{test}. Current Path: ");
            if (previousPath != null)
            {
                DisplayPath(previousPath, $"{test}. Previous Path: ");
            }
            paths = CreatePaths(con, waypointId, startWaypoint, p);
        }


        return paths;
    }


    private void DisplayPath(List<Waypoint> path, string text = "")
    {
        string d = "";
        foreach (var p in path)
        {
            d += $"{ p.name} ";
        }
        if (d == "")
            return;
        Debug.Log($"{text}{d}");
    }

    public Waypoint GetNearestPoint(Vector2 pos)
    {
        Waypoint nearestPoint = _waypoints[0];
        foreach (var waypoint in _waypoints)
        {
            float nearestDistance = Vector2.Distance(pos, nearestPoint.transform.position);
            float currentDistance = Vector2.Distance(pos, waypoint.transform.position);
            if (currentDistance < nearestDistance)
                nearestPoint = waypoint;
        }

        return nearestPoint;
    }
}
