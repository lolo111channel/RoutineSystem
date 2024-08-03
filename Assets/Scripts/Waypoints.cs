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
        List<List<Waypoint>> paths = CreatePaths(nearestWaypoint, waypointID);
        var pathsThatEndOnWaypointID = paths.Select(x=>x).Where(x=>x.Last().name == waypointID);

        foreach (var path in paths)
        {
            DisplayPath(path);
        }

        foreach (var path in pathsThatEndOnWaypointID)
        {
            DisplayPath(path);
        }
    

        return null;
    }

    private List<List<Waypoint>> CreatePaths(Waypoint startWaypoint, string waypointID, Waypoint previouseWay = null, List<Waypoint> previousPath = null)
    {
        List<List<Waypoint>> paths = new();
        List<Waypoint> path = previousPath != null ? new(previousPath) : new() { startWaypoint };
        

        foreach (var con in startWaypoint.Connections)
        {
            List<Waypoint> p = new(path);

            if (previouseWay != null)
            {
                if (con.name == previouseWay.name)
                    continue;
            }


            if (p.Any(x => x.name == con.name))
                continue;
           

            p.Add(con);
            if (con.Connections.Select(x=>x).Where(x=>p.Any(c=>c.name == x.name)).ToList().Count >= con.Connections.Count || con.name == waypointID)
            {
                paths.Add(new(p));
                continue;
            }

            var subPaths = CreatePaths(con, waypointID, startWaypoint, p);
            paths.AddRange(subPaths);        
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
