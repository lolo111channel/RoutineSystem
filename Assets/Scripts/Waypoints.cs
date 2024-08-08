using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    private Waypoint[] _waypoints = new Waypoint[0];

    public List<Waypoint> FindPath(Vector2 start, string waypointID)
    {

        if (_waypoints.Length <= 0)
            _waypoints = GetComponentsInChildren<Waypoint>();

        Waypoint nearestWaypoint = GetNearestPoint(start);
        List<List<Waypoint>> paths = CreatePaths(nearestWaypoint, waypointID);
        List<List<Waypoint>> pathsThatEndOnWaypointID = paths.Select(x=>x).Where(x=>x.Last().name == waypointID).ToList();

        Dictionary<int, float> pathsDistance = new();

        for (int i = 0; i < pathsThatEndOnWaypointID.Count; i++)
        {
            pathsDistance.Add(i, 0.0f);
            if (pathsThatEndOnWaypointID[i].Count - 1 < 1)
            {
                continue;
            }

            for (int j = 0; j < pathsThatEndOnWaypointID[i].Count - 1; ++j)
            {
                Waypoint way1 = pathsThatEndOnWaypointID[i][j];
                Waypoint way2 = pathsThatEndOnWaypointID[i][j + 1];
                float distance = Vector2.Distance(way1.transform.position, way2.transform.position);

                pathsDistance[i] += distance;
            }
        }

        List<Waypoint> nearestPath = pathsThatEndOnWaypointID.Count > 0 ? pathsThatEndOnWaypointID[0] : new();
        if (pathsDistance.Count > 0)
        {
            nearestPath = pathsThatEndOnWaypointID[pathsDistance.Aggregate((x,y) => x.Value < y.Value ? x : y).Key];
        }
        else
        {
            nearestPath.Add(nearestWaypoint);
        }

        return nearestPath;
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


    public void DisplayPath(List<Waypoint> path, string text = "")
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
            if (!waypoint.IsDisable)
            {
                float nearestDistance = Vector2.Distance(pos, nearestPoint.transform.position);
                float currentDistance = Vector2.Distance(pos, waypoint.transform.position);
                if (currentDistance < nearestDistance)
                    nearestPoint = waypoint;
            }
        }

        return nearestPoint;
    }
}
