using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : Enemy
{
    [Header("State Conditions & Patrol")] public bool isPatrol = false;
    public float patroltWaypointDistance = 2f;
    public Transform[] patrolPoints;
    public int patrolIndex;
    public float waitTime = 0.5f;
    private bool waitAtPatrol = false;

    // private void Patrol()
    // {
    //     if (currentState != EnemyStates.IDLE) return;
    //     target = patrolPoints[patrolIndex];
    //     if (!waitAtPatrol && Vector3.Distance(transform.position, patrolPoints[patrolIndex].position) <
    //         patroltWaypointDistance)
    //     {
    //         waitAtPatrol = true;
    //         patrolIndex++;
    //         patrolIndex = patrolIndex % patrolPoints.Length; //modulo: remainder used to wrap to 0
    //         StartCoroutine(WaitAtPatrol(waitTime));
    //     }
    // }
}