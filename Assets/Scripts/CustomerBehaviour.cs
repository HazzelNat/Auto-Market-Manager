using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CustomerBehaviour : MonoBehaviour
{
    public State state;

    [Header("Checks")]
    private bool firstTime;
    public Tilemap[] tilemap;
    private int randomBehaviour;
    private int randomItem;
    private int randomCashier;
    private bool completeTask;

    [Header("Target")]
    [SerializeField] private Transform customerSpawn;
    [SerializeField] private Transform[] customerTargets;
    [SerializeField] private Transform[] customerCashiers;
    private Vector3 currentTarget;
    public Vector3 targetPosition;

    [Header("Pathfinding and Moving")]
    Pathfinding.Path path;
    int currentWaypoint = 0;
    public bool reachTarget = false;
    Seeker seeker;
    public float speed;
    private string tileName;
    [SerializeField] private float nextWaypointDistance = .1f;
    private int item;

    // [Header("Task")]
    // private CheckStock;
    public enum State{
        Thinking,
        Moving,
        DoingTask,
    };

    private void Start(){
        state = State.Thinking;
        seeker = GetComponent<Seeker>();
    }

    private void Update(){
        Movement(state);
    }

    void Movement(State state){
        switch(state)
        {
            case State.Thinking:
                if(firstTime){
                    randomBehaviour = 0;
                } else {
                    randomBehaviour = Random.Range(0, 2);
                }
                
                switch (randomBehaviour){
                    case 0:                 // Search item
                        randomItem = Random.Range(0, 10);
                        currentTarget = customerTargets[randomItem].position;

                        ChangeState(State.Moving);
                        seeker.StartPath(transform.position, currentTarget, OnCompletePath);

                        break;

                    case 1:                 // Checkout
                        randomCashier = Random.Range(0, 2);
                        currentTarget = customerCashiers[randomCashier].position;

                        ChangeState(State.Moving);
                        seeker.StartPath(transform.position, currentTarget, OnCompletePath);

                        break;
                }

                

                break;

            case State.Moving:
                if(path == null){
                    ChangeState(State.Thinking);
                    return;
                }

                if(currentWaypoint >= path.vectorPath.Count - 1){
                    if(currentTarget != transform.position){
                        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
                        reachTarget = false;
                    } else {
                        reachTarget = true;
                    }                    
                } else {
                    reachTarget = false;

                    transform.position = Vector3.MoveTowards(transform.position, path.vectorPath[currentWaypoint], speed * Time.deltaTime);

                    float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

                    if(distance < nextWaypointDistance){
                        currentWaypoint++;
                    }
                }
                
                if(reachTarget){
                    ChangeState(State.DoingTask);                                   // Moving -> Doing Task (Reach target)
                }

                break;

            case State.DoingTask:
                DoingTask();

                if(completeTask){
                    ChangeState(State.Thinking);                                            // Doing Task -> Idle (after task)
                }
                
                break;
        }
    }

    void ChangeState(State currentState){
        state = currentState;
    }

    void OnCompletePath(Pathfinding.Path currentPath)
    {
        if(!currentPath.error){
            path = currentPath;
            currentWaypoint = 0;
        }
    }

    private void DoingTask()
    {
        if(tileName == "Shelf"){
            
        }
    }
}
