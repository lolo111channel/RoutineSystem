using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NpcMove : MonoBehaviour
{
    [SerializeField] private Waypoints _waypoints;
    [SerializeField] private float _movementSpeed = 5.0f;
    private Waypoint[] _currentPath = new Waypoint[0];
    private int _currentPathIndex = 0;


    private void Update()
    {
        if (_currentPathIndex < _currentPath.Length)
        {
    
            Waypoint waypoint = _currentPath[_currentPathIndex];
            if (Vector2.Distance(transform.position, waypoint.transform.position) < 0.5)
            {
                _currentPathIndex++;
            }

            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, waypoint.transform.position, _movementSpeed * Time.deltaTime);
        }
    }

    public void SetPath(string waypointID)
    {
        _currentPath = _waypoints.FindPath(transform.position, waypointID);
        _currentPathIndex = 0;
    }
}
