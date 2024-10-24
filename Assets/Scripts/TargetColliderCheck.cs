using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetColliderCheck : MonoBehaviour
{
    public bool moveable = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 8){
            moveable = false;
        } else {
            moveable = true;
        }
    }
}
