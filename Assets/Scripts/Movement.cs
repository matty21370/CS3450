using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _agent.speed = movementSpeed;
    }

    public void SetDestination(Vector3 position)
    {
        _agent.SetDestination(position);
    }

    public void Stop()
    {
        _agent.isStopped = true;
    }

    public void SetSpeed(float speed)
    {
        movementSpeed = speed;
        _agent.speed = movementSpeed;
    }
}
