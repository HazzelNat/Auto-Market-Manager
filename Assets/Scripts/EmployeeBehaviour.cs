using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Pathfinding;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class EmployeeBehaviour : MonoBehaviour
{
    bool isHovered;

    public Collider2D collider2d;
    public Transform target;
    public State state;
    public Vector3 targetPosition;
    public Vector3 mousePosition;

    [Header("Pathfinding and Moving")]
    Pathfinding.Path path;
    int currentWaypoint = 0;
    public bool reachTarget = false;
    Seeker seeker;
    public float speed;
    [SerializeField] public float nextWaypointDistance = .1f;

    [Header("Other Employee")]
    List<GameObject> otherEmployee = new List<GameObject>();

    public enum State{
        Idle,
        Selected,
        Moving,
        DoingTask,
        Squished
    };

    private void Start(){
        state = State.Idle;
        seeker = GetComponent<Seeker>();
        // GameObject employeeList = GameObject.Find("EmployeeList");

        // for(int i=0 ; i<emp)
    }

    private void Update(){
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        MouseRaycast();

        Movement(state);
        Debug.Log(target.position);
    }

    void MouseRaycast(){
        var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    
        if (collider2d.bounds.IntersectRay(mouseRay)){
            isHovered = true;
        } else {
            isHovered = false;
        } 
    }

    void Movement(State state){
        switch(state)
        {
            case State.Idle:
                if(Input.GetMouseButton(0)){                                        // Idle -> Selected
                    if(isHovered){
                        ChangeState(State.Selected);
                    }
                } 
                break;
            
            case State.Selected:
                targetPosition = mousePosition;
                
                targetPosition.x = Mathf.RoundToInt(targetPosition.x);
                targetPosition.y = Mathf.RoundToInt(targetPosition.y);
                targetPosition.z = 0;

                if(Input.GetMouseButton(0)){
                    if(isHovered){
                        ChangeState(State.Selected);                                // Selected -> Selected
                    } else if(!isHovered){
                        if(targetPosition != transform.position){
                            target.position = targetPosition;
                            
                            seeker.StartPath(transform.position, target.position, OnCompletePath);

                            ChangeState(State.Moving);                              // Selected -> Moving (if nothing blocked)
                        } else {
                            Debug.Log("Selected Blocked");
                            ChangeState(State.Selected);                            // Selected -> Selected (Blocked)
                        }
                    }
                } 

                break;

            case State.Moving:
                if(path == null){
                    ChangeState(State.Selected);
                    return;
                }

                if(currentWaypoint >= path.vectorPath.Count - 1){
                    if(target.position != transform.position){
                        transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
                        reachTarget = false;
                        Debug.Log("HM");
                    } else {
                        reachTarget = true;
                        Debug.Log("HMM");
                    }
                    
                } else {
                    reachTarget = false;

                    transform.position = Vector3.MoveTowards(transform.position, path.vectorPath[currentWaypoint], speed);

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
                ChangeState(State.Idle);                                            // Doing Task -> Idle (after task)
                
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

    void CheckOtherEmployee()
    {

    }

}
