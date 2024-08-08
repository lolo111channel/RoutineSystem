using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NpcMove : MonoBehaviour
{
    [SerializeField] private Waypoints _waypoints;
    [SerializeField] private float _movementSpeed = 5.0f;
    private List<Waypoint> _currentPath = new();
    private int _currentPathIndex = 0;
    private bool _waitOnActionWaypoint = false;
    private string _actionName = "";

    public bool IsArrived = true;

    public delegate void ArrivedEventHandler(ActionWaypoint actionWaypoint);
    public event ArrivedEventHandler OnArrived;

    private void Update()
    {
        if (_currentPathIndex < _currentPath.Count)
        {
            IsArrived = false;

            Waypoint waypoint = _currentPath[_currentPathIndex];
            if (Vector2.Distance(transform.position, waypoint.transform.position) < 0.1)
            {
                _currentPathIndex++;
            }

            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, waypoint.transform.position, _movementSpeed * Time.deltaTime);
            gameObject.transform.position = new(gameObject.transform.position.x, gameObject.transform.position.y, -1f);
        }
        else if (!IsArrived)
        {
            IsArrived = true;
            if (_currentPath.Count > 0)
            {
                ActionWaypoint actionWaypoint = _currentPath.Last().GetComponent<ActionWaypoint>();
                OnArrived?.Invoke(actionWaypoint);
            }
        }

        if (_waitOnActionWaypoint && _actionName != "")
            SetActionWaypoint(_actionName);
    }

    public void SetPath(string waypointID, string actionName = "")
    {

        if (_currentPath.Count > 0)
        {
             var actionWaypoint = _currentPath.Last().GetComponent<ActionWaypoint>();
             if (actionWaypoint != null)
                actionWaypoint.IsBusy = false;
        }

        IsArrived = false;
        _currentPath = _waypoints.FindPath(transform.position, waypointID);

        SetActionWaypoint(actionName);
        _currentPathIndex = 0;
    }

    private void SetActionWaypoint(string actionName)
    {
        if (_currentPath.Count > 0)
        {
            if (actionName != "")
            {
                _waitOnActionWaypoint = true;
                _actionName = actionName;
                var actionWaypoints = _currentPath.Last().GetComponentsInChildren<ActionWaypoint>();
                foreach (var action in actionWaypoints)
                {
                    if (action.Action.ActionName == actionName && !action.IsBusy)
                    {
                        Waypoint newWay = action.GetComponent<Waypoint>();
                        action.IsBusy = true;
                        _currentPath.Add(newWay);
                        _waitOnActionWaypoint = false;
                        break;
                    }
                }

            }
        }
    }
}
