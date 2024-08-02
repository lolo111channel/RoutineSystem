using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMove : MonoBehaviour
{
    [SerializeField] private Transform _testWaypoint;
    [SerializeField] private float _movementSpeed = 5.0f;

    private void Update()
    {
        //gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, _testWaypoint.position, _movementSpeed * Time.deltaTime);
    }
}
