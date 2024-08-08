using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NpcMove))]
public class NPCRoutine : MonoBehaviour
{
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private List<Routine> _routines = new();
    private NpcMove _npcMove;
    [SerializeField] private Routine _previouseRoutine = new(); 
    [SerializeField] private Routine _currentRoutine = new();

    private void OnEnable()
    {
        _npcMove = GetComponent<NpcMove>();
        if (_npcMove != null)
        {
            _npcMove.OnArrived += OnArrived;
        }

        if (_timeManager != null)
            _timeManager.OnTimeChange += OnTimeChange;
    }

    private void OnDisable()
    {
        if(_npcMove != null)
        {
            _npcMove.OnArrived -= OnArrived;
        }

        if (_timeManager != null)
            _timeManager.OnTimeChange -= OnTimeChange;
    }

    private void OnTimeChange(int hours, int minutes)
    {
        if (_npcMove.IsArrived)
        {
            foreach (var routine in _routines)
            {
                if (hours >= routine.StartHour && hours < routine.EndHour)
                {
                    _currentRoutine = routine;
                    break;
                }
            }

            if (_currentRoutine != _previouseRoutine)
            {
                _npcMove.SetPath(_currentRoutine.WaypointID.name, _currentRoutine.ActionName);
                _previouseRoutine = _currentRoutine;
            }
        }
        else
        {
            Animator animator = gameObject.GetComponent<Animator>();
            animator.Play("Idle");
        }
        
    }

    private void OnArrived(ActionWaypoint actionWaypoint)
    {
        if (actionWaypoint != null)
        {
            actionWaypoint.PlayAction(gameObject);
        }
    }



}
