using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using UnityEngine.Tilemaps;

public class EmployeeBehaviour : MonoBehaviour
{
    bool isHovered;

    public Collider2D collider2d;
    public Transform target;
    public State state;
    public Vector3 targetPosition;
    public Vector3 mousePosition;
    public GameObject arrowSelect;
    public Tilemap[] tilemap;
    private bool completeTask;

    [Header("Pathfinding and Moving")]
    Pathfinding.Path path;
    int currentWaypoint = 0;
    public bool reachTarget = false;
    Seeker seeker;
    public float speed;
    public float normalSpeed;
    public float slowSpeed;
    private string tileName;
    private float nextWaypointDistance = .1f;
    private int shortestTarget;
    private float distance;
    private float shortestDistance;
    [SerializeField] private GameObject boxes;
    [SerializeField] private Transform[] shelf;
    [SerializeField] private Transform[] cashier;
    private GameObject currentLocation;

    [Header("Tasks")]
    public GameObject box;
    private bool emptyHanded;
    FollowTarget followTarget;
    private bool stillDoingTask;
    private bool destroyBox;
    [SerializeField] private TaskProgressManager taskProgress;

    [Header("Other Employee")]
    List<GameObject> otherEmployee = new List<GameObject>();

    [Header("Audio")]
[SerializeField] private AudioSource audioSource;
[SerializeField] private AudioClip pickupBoxSound;
[SerializeField] private AudioClip stockingSound;
[SerializeField] private AudioClip taskCompleteSound;

    public enum State
    {
        Idle,
        Selected,
        Moving,
        DoingTask,
        Squished
    };

    private void Start()
    {
        arrowSelect.SetActive(false);
        state = State.Idle;
        seeker = GetComponent<Seeker>();
        target.transform.parent = null;
        emptyHanded = true;


        audioSource = GetComponent<AudioSource>();
    if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();

    }

    private void Update()
    {
        if (!emptyHanded)
        {
            speed = slowSpeed;
        }
        else
        {
            speed = normalSpeed;
        }

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        MouseRaycast();

        Movement(state);

        if (state == State.Selected)
        {
            arrowSelect.SetActive(true);
        }
        else
        {
            arrowSelect.SetActive(false);
        }
    }

    void MouseRaycast()
    {
        var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (collider2d.bounds.IntersectRay(mouseRay))
        {
            isHovered = true;
        }
        else
        {
            isHovered = false;
        }
    }

    void Movement(State state)
    {
        switch (state)
        {
            case State.Idle:
                if (Input.GetMouseButton(0))
                {
                    if (isHovered)
                    {
                        ChangeState(State.Selected);
                    }
                }
                break;

            case State.Selected:
                targetPosition = mousePosition;

                targetPosition.x = Mathf.RoundToInt(targetPosition.x);
                targetPosition.y = Mathf.RoundToInt(targetPosition.y);
                targetPosition.z = 0;

                if (Input.GetMouseButton(0))
                {
                    if (isHovered)
                    {
                        ChangeState(State.Selected);
                    }
                    else if (!isHovered)
                    {
                        if (targetPosition != transform.position)
                        {
                            CheckTarget();
                        }
                        else
                        {
                            Debug.Log("Selected Blocked");
                            ChangeState(State.Selected);
                        }
                    }
                }
                break;

            case State.Moving:
                if (path == null)
                {
                    ChangeState(State.Selected);
                    return;
                }

                if (currentWaypoint >= path.vectorPath.Count - 1)
                {
                    if (target.position != transform.position)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                        reachTarget = false;
                    }
                    else
                    {
                        reachTarget = true;
                    }
                }
                else
                {
                    reachTarget = false;

                    transform.position = Vector3.MoveTowards(transform.position, path.vectorPath[currentWaypoint], speed * Time.deltaTime);

                    float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

                    if (distance < nextWaypointDistance)
                    {
                        currentWaypoint++;
                    }
                }

                if (reachTarget)
                {
                    ChangeState(State.DoingTask);
                }
                break;

            case State.DoingTask:
                DoingTask(tileName);

                if (completeTask)
                {
                    ChangeState(State.Idle);
                }
                break;
        }
    }

    void ChangeState(State currentState)
    {
        state = currentState;
    }

    void OnCompletePath(Pathfinding.Path currentPath)
    {
        if (!currentPath.error)
        {
            path = currentPath;
            currentWaypoint = 0;
        }
    }

    private void CheckTarget()
    {
        target.position = targetPosition;

        Vector3Int targetPosInt = Vector3Int.FloorToInt(target.position);
        targetPosInt = new Vector3Int(targetPosInt.x - 1, targetPosInt.y - 1, 0);

        for (int i = 0; i < tilemap.Count(); i++)
        {
            if (tilemap[i].GetTile(targetPosInt) != null)
            {
                tileName = tilemap[i].name;
                SetTarget(tileName);
            }
        }

        if (boxes.transform.childCount != 0)
        {
            for (int i = 0; i < boxes.transform.childCount; i++)
            {
                if (targetPosition == boxes.transform.GetChild(i).position)
                {
                    box = boxes.transform.GetChild(i).gameObject;
                    target.position = targetPosition;
                    tileName = "Box";
                    SetTarget(tileName);
                    break;
                }
            }
        }
    }

    private void SetTarget(string tileName)
    {
        GetClosestTarget(tileName);
        seeker.StartPath(transform.position, target.position, OnCompletePath);
        ChangeState(State.Moving);
    }

    private void GetClosestTarget(string tile)
    {
        switch (tileName)
        {
            case "Box":
                break;

            case "Shelf":
                for (int i = 0; i < shelf.Count(); i++)
                {
                    distance = Vector3.Distance(targetPosition, shelf[i].position);

                    if (shortestDistance == 0)
                    {
                        shortestDistance = distance;
                        shortestTarget = i;
                    }

                    if (shortestDistance > distance)
                    {
                        shortestDistance = distance;
                        shortestTarget = i;
                    }
                }

                target.position = shelf[shortestTarget].position;
                currentLocation = shelf[shortestTarget].gameObject;

                shortestDistance = 0;
                shortestTarget = 0;
                break;

            case "Cashier":
                for (int i = 0; i < cashier.Count(); i++)
                {
                    distance = Vector3.Distance(targetPosition, cashier[i].position);

                    if (shortestDistance == 0)
                    {
                        shortestDistance = distance;
                        shortestTarget = i;
                    }

                    if (shortestDistance > distance)
                    {
                        shortestDistance = distance;
                        shortestTarget = i;
                    }
                }

                target.position = cashier[shortestTarget].position;
                currentLocation = cashier[shortestTarget].gameObject;

                shortestDistance = 0;
                shortestTarget = 0;
                break;
        }
    }

    private void DoingTask(string tileName)
    {
        if (tileName == null) return;

        switch (tileName)
        {
            case "Box":
                 if (emptyHanded && box != null) {
                    followTarget = box.GetComponent<FollowTarget>();
                    if (followTarget != null)
                    {
                        emptyHanded = false;
                        followTarget.Follow(gameObject);
                        if (pickupBoxSound != null) audioSource.PlayOneShot(pickupBoxSound);
                    }
                }
                ChangeState(State.Selected);
                break;

            case "Shelf":
                if (!emptyHanded)
                {
                    if (!stillDoingTask && box != null && currentLocation != null)
                    {
                        Stock stockScript = currentLocation.GetComponent<Stock>();

                        if (stockScript != null && stockScript.CheckItem(box))
                        {
                        stillDoingTask = true;
                        if (stockingSound != null) audioSource.PlayOneShot(stockingSound);
                        StartCoroutine(WaitTask(3));
                        }
                        else
                        {
                            ChangeState(State.Selected);
                        }
                    }
                }
                else
                {
                    ChangeState(State.Selected);
                }

                if (destroyBox && followTarget != null){
                followTarget.Destroy();
                followTarget = null;
                box = null;
                destroyBox = false;
                if (taskCompleteSound != null) audioSource.PlayOneShot(taskCompleteSound);
                ChangeState(State.Idle);
}
                break;

            case "Cashier":
                break;
        }
    }

    IEnumerator WaitTask(float cooldownTime)
    {
        Debug.Log("Play anim / Progress bar");

        if (taskProgress != null)
        {
            taskProgress.ShowProgressBar(cooldownTime);
        }

        yield return new WaitForSecondsRealtime(cooldownTime);

        emptyHanded = true;
        destroyBox = true;
        stillDoingTask = false;
    }
}