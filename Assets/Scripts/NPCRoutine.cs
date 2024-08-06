using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NpcMove))]
public class NPCRoutine : MonoBehaviour
{
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private List<Routine> _routines = new();
    private NpcMove _npcMove;

    private void OnEnable()
    {
        _npcMove = GetComponent<NpcMove>();
        if (_timeManager != null)
            _timeManager.OnTimeChange += OnTimeChange;
    }

    private void OnDisable()
    {
        if (_timeManager != null)
            _timeManager.OnTimeChange -= OnTimeChange;
    }

    private void OnTimeChange(int hours, int minutes)
    {
        foreach (var routine in _routines)
        {
            if (routine.StartHour <= hours && routine.EndHour > hours)
            { 
                _npcMove.SetPath(routine.WaypointID.name);
                break;
            }
        }
    }



}
