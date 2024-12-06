using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using JetBrains.Annotations;
using Pathfinding;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class CustomerBehaviour : MonoBehaviour
{
    public enum State{
        Thinking,
        Moving,
        DoingTask,
        Leaving
    };
    public State state;

    [Header("Emote")]
    [SerializeField] private GameObject thinkingEmoji;
    [SerializeField] private GameObject madEmoji;

    [Header("Checks")]
    private bool alreadyDetermined;
    public bool firstTime;
    public bool transactionFinished;
    public Tilemap[] tilemap;
    private int randomBehaviour;
    private int randomItem;
    private int randomWants;
    private int randomCashier;
    private bool completeTask;
    public bool isLeaving;
    [SerializeField] MoneyManager moneyManager;

    [Header("Target")]
    [SerializeField] private GameObject customerSpawn;
    [SerializeField] private GameObject[] customerTargets;
    [SerializeField] private GameObject[] customerCashiers;
    private Vector3 currentTarget;
    public Vector3 targetPosition;
    private GameObject currentTask;

    [Header("Inventory")]
    public string[] itemName;
    public int[] itemNumber;
    public float totalCost;
    public string wants;
    public Dictionary<string, int> inventory = new Dictionary<string, int>()
    {
        {"Snacks", 0},
        {"Beverage", 0},
        {"Ice Cream", 0}
    };

    Dictionary<string, float> itemCostCashier = new Dictionary<string, float>()
    {
        {"Snacks", 4f},
        {"Beverage", 3f},
        {"Ice Cream", 2.5f}
    };

    [Header("Pathfinding and Moving")]
    Pathfinding.Path path;
    [SerializeField] private float nextWaypointDistance = .1f;
    int currentWaypoint = 0;
    public bool reachTarget = false;
    Seeker seeker;
    public float speed;

    private void Start(){
        state = State.Thinking;
        seeker = GetComponent<Seeker>();
        firstTime = true;

        randomWants = Random.Range(0, 3);

        GetItemWants(randomWants);
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
                } else if (!firstTime && !alreadyDetermined){
                    randomBehaviour = Random.Range(0, 2);
                }
                
                if(!alreadyDetermined){
                    switch (randomBehaviour){
                        case 0:                 // Search item
                            randomItem = Random.Range(0, 10);

                            currentTask = customerTargets[randomItem];

                            if(currentTask.name == wants){
                                currentTarget = customerTargets[randomItem].transform.position;
                                StartCoroutine(Thinking(currentTask));
                                seeker.StartPath(transform.position, currentTarget, OnCompletePath);
                                alreadyDetermined = true;
                            }

                            
                            // ChangeState(State.Moving);
                            
                            break;

                        case 1:                 // Checkout
                            if(totalCost == 0){
                                ChangeState(State.Leaving);
                                break;
                            }

                            randomCashier = Random.Range(0, 2);
                            currentTarget = customerCashiers[randomCashier].transform.position;
                            currentTask = customerCashiers[randomCashier];

                            // ChangeState(State.Moving);
                            StartCoroutine(Thinking(currentTask));
                            seeker.StartPath(transform.position, currentTarget, OnCompletePath);
                            alreadyDetermined = true;

                            break;
                    }

                    break;
                }

                break;
                

            case State.Moving:
                alreadyDetermined = false;

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
                    firstTime = false;

                    if(isLeaving){
                        Destroy(gameObject);
                    }
                    ChangeState(State.DoingTask);                                   // Moving -> Doing Task (Reach target)
                }

                break;

            case State.DoingTask:
                DoingTask();
                
                break;

            case State.Leaving:
                isLeaving = true;
                currentTarget = customerSpawn.transform.position;
                currentTask = customerSpawn;

                seeker.StartPath(transform.position, currentTarget, OnCompletePath);
                StartCoroutine(Thinking(currentTask));

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
        switch (currentTask.transform.parent.name){
            case "Shelf":
                Stock stock = currentTask.GetComponent<Stock>();
                bool isBrought = stock.DecreaseStock();

                if(isBrought){
                    inventory[currentTask.name] += 1;
                } else {
                    ChangeEmoji(madEmoji);
                }

                foreach(var item in inventory)
                {
                    totalCost = totalCost + (inventory[item.Key] * itemCostCashier[item.Key]);
                }

                ChangeState(State.Thinking);

                break;

            case "Cashier":
                CustomerCashier customerCashier = currentTask.GetComponent<CustomerCashier>();

                if (customerCashier.transactionFinished) {
                    customerCashier.RemoveCustomer();
                    ChangeState(State.Leaving);
                } else {
                    if (customerCashier.customer == null) {
                        customerCashier.AddCustomer(gameObject);
                    }
                }

                Debug.Log(state);

                break;
        }
    }

    private void GetItemWants(int i)
    {
        switch(i){
            case 0:
                wants = "Ice Cream";
                break;

            case 1:
                wants = "Snacks";
                break;
                
            case 2:
                wants = "Beverage";
                break;
                    
            default:
                break;
        }
    }

    IEnumerator Thinking(GameObject task)
    {
        if(madEmoji.activeSelf != true){
            ChangeEmoji(thinkingEmoji);
        }

        yield return new WaitForSecondsRealtime(3f);

        ChangeEmoji(null);
        currentTask = task;
        ChangeState(State.Moving);
    }

    private void ChangeEmoji(GameObject emoji)
    {
        madEmoji.SetActive(false);
        thinkingEmoji.SetActive(false);

        if(emoji){
            emoji.SetActive(true);
        }
    }
}
