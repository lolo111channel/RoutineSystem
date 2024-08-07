using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWaypoint : MonoBehaviour
{
    public Action Action;
    public bool IsBusy = false;


    public void PlayAction(GameObject npc)
    {
        Animator animator = npc.GetComponent<Animator>();
        animator.Play(Action.AnimationName);
    }
}
