using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public bool isFollowing;
    public bool onFloor;
    private GameObject target;
    void Start()
    {
        onFloor = true;
    }

    void Update()
    {
        if(isFollowing){
            if(onFloor){
                // Vector3 vector3 = gameObject.transform.GetChild(0).transform.position;
                transform.GetChild(0).transform.position = new Vector3(gameObject.transform.GetChild(0).transform.position.x, gameObject.transform.GetChild(0).transform.position.y + 0.7f);
                onFloor = false;
            }

            transform.position = target.transform.position;
        }
    }

    public void Follow(GameObject gameObject)
    {   
        isFollowing = true;
        target = gameObject;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}