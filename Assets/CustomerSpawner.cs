using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] float cooldownTime;
    [SerializeField] GameObject customerPrefab;
    int randomRange;

    private void Start() {
        randomRange = Random.Range(-3, 8);
        InvokeRepeating("SpawnCustomer", 20f, cooldownTime + randomRange);
    }

    private void SpawnCustomer()
    {
        randomRange = Random.Range(-3, 8);

        GameObject instance = Instantiate(customerPrefab, transform.position, Quaternion.identity);

        instance.transform.parent = transform;
        instance.gameObject.SetActive(true);
    }
}
