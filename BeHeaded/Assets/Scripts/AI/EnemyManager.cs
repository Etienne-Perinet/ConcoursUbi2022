using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class EnemyManager : MonoBehaviour
{
    private PatrolState patrolState;
    private DetectState detectionState;
    private ChaseState chaseState;
    private SearchState searchState;
    private IdleState idleState;


    [field : SerializeField] public float SearchTime { get; protected set; } 
    public float DetectionTime { get => CurrentDetectionTime(); }

    public NavMeshAgent NavMesh { get; set; } 
    public EnemyState State { get; set; }
    public Vector3 LastPlayerPosition { get; protected set;}
    public float Timer { get; protected set; }


    public virtual void Start()
    {
        // Chain of responsabilities
        patrolState = new PatrolState(this);
        detectionState = new DetectState(this);
        chaseState = new ChaseState(this);
        searchState = new SearchState(this);
        idleState = new IdleState(this);


        patrolState.NextState = detectionState;
        detectionState.NextState = chaseState;
        chaseState.NextState = searchState;
        searchState.NextState = patrolState;
        idleState.NextState = patrolState;

        InitializeState();
    }

    public abstract void Patrol();
    public abstract void Detect(float detectionValue);
    public abstract void Chase();
    public abstract void Search();
    public abstract void Idle();

    public abstract bool IsPlayerInArea();
    public abstract void ChangeTarget(Vector3 newTarget);
    public abstract void ReinitializeBar();
    public abstract float CurrentDetectionTime();

    public void InitializeState() => State = patrolState;

    public void RedetectPlayer() => State = detectionState;

    public void StopEnemy() => State = idleState;
}
