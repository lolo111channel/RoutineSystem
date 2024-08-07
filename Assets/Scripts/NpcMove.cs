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

    public bool IsArrived = true;

    public delegate void ArrivedEventHandler(ActionWaypoint actionWaypoint);
    public event ArrivedEventHandler OnArrived;

    private void Update()
    {
        if (_currentPathIndex < _currentPath.Length)
        {
            IsArrived = false;

            Waypoint waypoint = _currentPath[_currentPathIndex];
            if (Vector2.Distance(transform.position, waypoint.transform.position) < 0.1)
            {
                _currentPathIndex++;
            }

            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, waypoint.transform.position, _movementSpeed * Time.deltaTime);
        }
        else if (!IsArrived)
        {
            IsArrived = true;
            if (_currentPath.Length > 0)
            {
                ActionWaypoint actionWaypoint = _currentPath.Last().GetComponent<ActionWaypoint>();
                OnArrived?.Invoke(actionWaypoint);
            }
        }
    }

    public void SetPath(string waypointID, string actionName = "")
    {
       
        IsArrived = false;
        _currentPath = _waypoints.FindPath(transform.position, waypointID, actionName);
        _currentPathIndex = 0;

    }
}
