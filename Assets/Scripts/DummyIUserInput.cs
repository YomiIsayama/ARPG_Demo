using SingleInstance;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DummyIUserInput : IUserInput
{
    public enum FSMstate
    {
        None,
        Patrol,
        Chase,
        Attack,
    }
    public FSMstate curState;
    public NavMeshAgent agent;
    public Transform player;
    public Transform wp1;
    public Transform wp2;
    public Transform wp3;

    private bool checkWp = true;
    private bool wp1to2 = false;
    private bool wp2to3 = false;
    private bool wp3to1 = false;

    private StateManager stateManager;
    private TargetManager targetManager;

    void Awake()
    {
        stateManager = this.GetComponent<StateManager>();
        targetManager = this.GetComponent<TargetManager>();
    }
    void Start()
    {
        curState = FSMstate.Patrol;
        agent = this.GetComponent<NavMeshAgent>();
        //Enemy_randomwayPoint();
    }
    void Update()
    {
        //UpdateDmagDvec(Dup,Dright);
        switch (curState)
        {
            case FSMstate.Patrol:
                StatePatrol();
                break;
            case FSMstate.Chase:
                StateChase();
                break;
            case FSMstate.Attack:
                StateAttack();
                break;
        }
        if (stateManager.HP <= 0)
        {
            inputEnabled = false;
            curState = FSMstate.None;
            Destroy(this.gameObject, 5.0f);
        }

    }
    void StatePatrol()
    {
        inputEnabled = true;
        Dup = 1;
        Dright = 0;
        UpdateDmagDvec(Dup, Dright);
        Enemy_Waypoint();

        if (Enemy_trigger.is_trigger == true)
        {
            curState = FSMstate.Chase;
            checkWp = false;
        }

    }
    void StateChase()
    {
        inputEnabled = true;
        Dup = 2;
        Dright = 0;
        UpdateDmagDvec(Dup, Dright);
        transform.LookAt(player.position);
        agent.SetDestination(player.position);
        if (Vector3.Distance(this.transform.position, player.position) <= 2)
        {
            curState = FSMstate.Attack;
        }
        if (Vector3.Distance(this.transform.position, player.position) >= 6)
        {
            curState = FSMstate.Patrol;
            checkWp = true;
        }
    }
    void StateAttack()
    {
        inputEnabled = false;
        Dup = 0;
        Dright = 0;
        UpdateDmagDvec(Dup, Dright);
        transform.LookAt(player.position);
        attack = true;
        if (Vector3.Distance(this.transform.position, player.position) >= 4)
        {
            curState = FSMstate.Chase;
            attack = false;
        }
    }

    //private void Enemy_randomwayPoint()
    //{
    //    wayPoint = new Vector3(this.transform.position.x + Random.Range(-10, 10), 0, this.transform.position.z + Random.Range(-10, 10));
    //}


    private void Enemy_Waypoint()
    {
        if (checkWp == true)
        {
            agent.SetDestination(wp1.position);
            wp1to2 = true;
            checkWp = false;
        }
        else if (Vector3.Distance(this.transform.position, wp1.position) < 2 && wp1to2 == true)
        {
            agent.SetDestination(wp2.position);
            wp2to3 = true;
            wp1to2 = false;
        }
        else if (Vector3.Distance(this.transform.position, wp2.position) < 2 && wp2to3 == true)
        {
            agent.SetDestination(wp3.position);
            wp3to1 = true;
            wp2to3 = false;
        }
        else if (Vector3.Distance(this.transform.position, wp3.position) < 2 && wp3to1 == true)
        {
            agent.SetDestination(wp1.position);
            wp3to1 = false;
            wp1to2 = true;
        }
    }





}
