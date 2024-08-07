using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcRoutineAction", menuName = "NPC/NpcRoutineAction", order = 1)]
public class Action : ScriptableObject
{
    public string ActionName = "TestAction";
    public string AnimationName = "Test";
}
