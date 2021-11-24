using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public enum EnemyState { Patrol, Chase, Attack, Dead }

[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class Knight : MonoBehaviour
{
    private bool initCheck;
    private bool lookAtTarget;
    Quaternion targetRot;
    // WaypointStuff
    public bool cirularList;
    private bool desendingList;
    private float waitTime;

    private bool goToPos;
    public int waypointIndex;
    public List<WaypointBase> waypoints = new List<WaypointBase>();

    EnemyState enemyStates;
    NavMeshAgent agent;
    EnemyStats stats;
    Animator anim;

    float direction;
    float speed;
    public GameObject target;




    // Use this for initialization
    void Start()
    {
        stats = GetComponent<EnemyStats>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemyStates = EnemyState.Patrol;
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyStates)
        {
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Chase:
                Chase();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Dead:
                Dead();
                break;
        }
    }

    private void Patrol()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= stats.chaseRange)
        {
            enemyStates = EnemyState.Chase;
        }
        Move();
        WaypointBase curWaypoint = waypoints[waypointIndex];
        if (!goToPos)
        {
            agent.destination = curWaypoint.target.position;
            goToPos = true;
        }
        else
        {
            float distanceToTarget = Vector3.Distance(agent.nextPosition, curWaypoint.target.position);
            if (distanceToTarget < stats.stopDistance)
            {
                CheckWayoint(curWaypoint);

            }
        }

    }

    private void CheckWayoint(WaypointBase curWaypoint)
    {
        if (!initCheck)
        {
            lookAtTarget = curWaypoint.lookTowards;
            initCheck = true;
        }
        #region WaitTime
        waitTime += Time.deltaTime;
        if (waitTime > curWaypoint.waitTime)
        {
            if (cirularList)
            {
                if (waypoints.Count - 1 > waypointIndex)
                {
                    waypointIndex++;
                }
                else
                {
                    waypointIndex = 0;
                }
            }
            else
            {
                if (!desendingList)
                {
                    if (waypoints.Count - 1 == waypointIndex)
                    {
                        desendingList = true;
                        waypointIndex--;
                    }
                    else
                    {
                        waypointIndex++;
                    }
                }
                else
                {
                    if (waypointIndex > 0)
                    {
                        waypointIndex--;
                    }
                    else
                    {
                        desendingList = false;
                        waypointIndex++;
                    }
                }
            }
            initCheck = false;
            goToPos = false;
            waitTime = 0;
        }
        #endregion
        #region LookToward
        if (lookAtTarget)
        {
            Vector3 direction = curWaypoint.targetToLook.position - transform.position;
            direction.y = 0;
            float angle = Vector3.Angle(transform.forward, direction);
            if (angle>0.1f)
            {
                targetRot = Quaternion.LookRotation(direction);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, tragetRot, 2f);
            }
        }
        #endregion


    }

    private void Move()
    {
        agent.updatePosition = false;
        agent.updateRotation = false;

        direction = Vector3.Angle(transform.forward, agent.desiredVelocity) * Mathf.Sign(Vector3.Dot(agent.desiredVelocity, transform.right));
        speed = agent.desiredVelocity.magnitude;

        anim.SetFloat("Speed", speed, 0.0f, Time.deltaTime);
        anim.SetFloat("Direction", direction, 0.0f, Time.deltaTime);

    }

    private void Chase()
    {
        throw new NotImplementedException();
    }

    private void Attack()
    {
        throw new NotImplementedException();
    }


    private void Dead()
    {
        throw new NotImplementedException();
    }

}
