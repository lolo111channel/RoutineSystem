using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NpcMove : MonoBehaviour
{
    [SerializeField] private Waypoints _waypoints;
    [SerializeField] private float _movementSpeed = 5.0f;
    private string _waypointID = "Way09";
    private Waypoint[] _currentPath = new Waypoint[0];
    private int _currentPathIndex = 0;

    private void Start()
    {
        _currentPath = _waypoints.FindPath(transform.position, _waypointID);
        Debug.Log(_currentPath.Length);
    }

    private void Update()
    {
        Debug.Log(_currentPath.Length);
        if (_currentPath.Length > 0)
        {
            if (_currentPathIndex > _currentPath.Length)
            {
                Array.Clear(_currentPath, 0, _currentPath.Length);
                _currentPathIndex = 0;

                return;
            }

            Waypoint waypoint = _currentPath[_currentPathIndex];
            if (Vector2.Distance(transform.position, waypoint.transform.position) < 0.5)
            {
                _currentPathIndex++;
            }

            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, waypoint.transform.position, _movementSpeed * Time.deltaTime);
        }


    }
}
