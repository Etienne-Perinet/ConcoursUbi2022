using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : EnemyManager
{
    private const float targetDistance = 1.75f;
    
    private Vector3 target;
    private Transform playerTarget;
    private GameObject detectionBar;
    private List<Vector3> waypoints;
    private Vector3 lastInteractionPosition;

    private List<Transform> players;
    private float farSightRange;
    private int waypointsIndex = -1;
    private bool playerInteractionGo = false;


    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float maxDetectionTime = 1f;
    [SerializeField] private float maxFarSightRange = 35f;
    [SerializeField] private float minFarSightRange = 10f;
    [SerializeField] private float closeSightRange = 5f;
    [SerializeField] private float sightAngle = 75f;
    [SerializeField] private float searchRange = 10f;
    [SerializeField] private LayerMask playersMask;
    [SerializeField] private bool chooseWaypoints;


    void Awake() 
    {
        NavMesh = GetComponent<NavMeshAgent>();
        detectionBar = GameObject.FindGameObjectWithTag("DetectionBar");
        detectionBar.SetActive(false);

        players = new List<Transform>();
        FeedTransformList("Player", players);

        waypoints = new List<Vector3>();
        FeedPositionList("Waypoint", waypoints);

        SetWaypointIndex();
    }

    public override void Start()
    {
        base.Start();
        NavMesh.speed = walkSpeed;
        State.RunCurrentState();
    }

    void Update()
    {
        Timer += Time.deltaTime;
        IncreasePlayerStress();
        State.RunCurrentState();
        
        // Pour dessiner les lignes du champ de vision (à enlever plus tard)
        Vector3 rightDirection = Quaternion.AngleAxis(sightAngle / 2, Vector3.up) * transform.forward * farSightRange;
        Vector3 leftDirection = Quaternion.AngleAxis(-sightAngle / 2, Vector3.up) * transform.forward * farSightRange;
        Debug.DrawRay(transform.position, rightDirection, Color.blue);
        Debug.DrawRay(transform.position, leftDirection, Color.blue);
        Debug.DrawRay(transform.position, transform.forward * farSightRange, Color.red);
        foreach(var player in players) Debug.DrawRay(transform.position, player.position - transform.position, Color.yellow);
        Debug.DrawRay(transform.position, target - transform.position, Color.black);
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(0.2f, 0.2f, 0.2f, 0.5f);
        Gizmos.DrawWireSphere(LastPlayerPosition, searchRange);
        Gizmos.color = new Color(255f, 0.2f, 0.2f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, closeSightRange);

        // UnityEditor.Handles.Label(Vector3.zero, State.ToString());
    }

    private void OnTriggerEnter(Collider other) 
    {
        //Debug.Log(other.tag);
        if(other.tag == "Door") 
        {
            StopEnemy();
            NormalDoor door = other.GetComponent<NormalDoor>();
            if(door.IsLocked) 
                door.Unlock(transform.position);
            else
                door.ToggleDoor(transform.position);
            State.RunNextState();
        }
            
    }

    void FeedTransformList(string tag, List<Transform> tranforms)
    {
        GameObject[] waypointsObj = GameObject.FindGameObjectsWithTag(tag);
        foreach (var obj in waypointsObj)
            tranforms.Add(obj.transform);
    }

    
    void FeedPositionList(string tag, List<Vector3> positions)
    {
        GameObject[] waypointsObj = GameObject.FindGameObjectsWithTag(tag);
        foreach (var obj in waypointsObj)
            positions.Add(obj.transform.position);
    }

    void IncreasePlayerStress()
    {
        walkSpeed = SpeedExpFunction(3, 5, walkSpeed);
        runSpeed = SpeedExpFunction(5, 9, runSpeed);
        farSightRange = maxFarSightRange;
    }

    float SpeedExpFunction(float min, float max, float currentSpeed)
    {
        if(currentSpeed < max)
            return (float) 0.5*Mathf.Pow(1.009f, Timer) + min;
        return currentSpeed;
    }

    void SightRangeExpFunction()
    {
        if(farSightRange < maxFarSightRange)
            farSightRange = (float) 0.0001*Mathf.Pow(1.1f, Timer) + minFarSightRange;
    }

    public override void Patrol()
    {
        if(playerInteractionGo) ChangeTarget(lastInteractionPosition);
        else ChangeTarget(waypoints[waypointsIndex]);
        NavMesh.speed = walkSpeed;
        CheckWaypointDistance();
    }

    public override void Detect(float detectionValue)
    {
        detectionBar.SetActive(true);
        // transform.LookAt(target);
        NavMesh.isStopped = true;
        detectionBar.GetComponent<DetectionBarAI>().DetectionChange(detectionValue);
    }

    public override void Chase()
    {
        ChangeTarget(playerTarget.position);
        if(CheckTargetDistance(4))
            NavMesh.speed = walkSpeed;
        else
            NavMesh.speed = runSpeed;
    }

    public override void Search()
    {
        NavMesh.speed = walkSpeed;
        if(CheckTargetDistance() || NavMesh.velocity == Vector3.zero)
            ChangeTarget(RandomNavSpot());
    }

    public override void Idle()
    {
        NavMesh.isStopped = true;
    }

    public override void ReinitializeBar() 
    {
        detectionBar.GetComponent<DetectionBarAI>().ReinitializeBar();
        detectionBar.SetActive(false);
    } 

    public Vector3 RandomNavSpot()
    {
        Vector3 randomDirection = Random.insideUnitSphere * searchRange + LastPlayerPosition;
        UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, searchRange, UnityEngine.AI.NavMesh.AllAreas);

        return  hit.position;
    }

    public override void ChangeTarget(Vector3 newTarget)
    {
        target = newTarget;
        NavMesh.SetDestination(target);
    }

    public override bool IsPlayerInArea() 
    {

        if(Physics.CheckSphere(transform.position, closeSightRange, playersMask)) // close view
        {
            playerTarget = PlayerMinDistance();
            return CheckObstacles();
        }
        else if(Physics.CheckSphere(transform.position, farSightRange, playersMask)) // far view
        {
            playerTarget = PlayerMinDistance();
            Vector3 playerDirection = (playerTarget.position - transform.position).normalized;

            return (Vector3.Angle(transform.forward, playerDirection) <= sightAngle / 2) && CheckObstacles();
        } 

        return false;
    } 

    public bool CheckObstacles()
    {
        float playerDistance = Vector3.Distance(transform.position, playerTarget.position);
        Ray rayForward = new Ray(transform.position, playerTarget.position - transform.position);

        if(Physics.Raycast(rayForward, out RaycastHit hit, playerDistance) && hit.collider.tag == "Player") 
        {
            LastPlayerPosition = playerTarget.position;
            return true;
        }

        return false;
    }

    Transform PlayerMinDistance()
    {
        float[] playersDistance = new float[players.Count];
        int playerIndex= 0;

        for(int i = 0; i < players.Count; i++)
        {
            playersDistance[i] = Vector3.Distance(transform.position, players[i].position);
            playerIndex = (playersDistance[i] <= playersDistance[playerIndex] ? i : playerIndex);
        }  

        return players[playerIndex];
    }
    
    int NextWaypoints() => (waypointsIndex + 1) % waypoints.Count;

    int ChooseNextWaypoints()
    {
        playerTarget = PlayerMinDistance();
        int minIndex = NextWaypoints();
        float minDistance = Vector3.Distance(playerTarget.position, waypoints[0]);
        float currentDistance;

        for(int i = 0; i < waypoints.Count; i++)
        {
            currentDistance = Vector3.Distance(playerTarget.position, waypoints[i]);
            if (currentDistance <= minDistance && i != waypointsIndex)
            {
                minDistance = currentDistance;
                minIndex = i;
            }
        }
        return minIndex;
    } 

    public override float CurrentDetectionTime()
    {
        float newTime = 0.05f * Vector3.Distance(transform.position, target);

        return (newTime >= maxDetectionTime ? maxDetectionTime : newTime);
    }

    void SetWaypointIndex() 
    {
         waypointsIndex = (chooseWaypoints ? ChooseNextWaypoints() : NextWaypoints());
    }
     
    void CheckWaypointDistance()
    {
        if(CheckTargetDistance() || NavMesh.velocity == Vector3.zero) 
        {
            if(playerInteractionGo) playerInteractionGo = false;
            SetWaypointIndex();
        }
    }

    bool CheckTargetDistance(float distance = targetDistance) => Vector3.Distance(transform.position, target) <= distance;

    public void NotifyEnemy(Vector3 playerPosition)
    {
        playerInteractionGo = true;
        lastInteractionPosition = playerPosition;
    }
}
