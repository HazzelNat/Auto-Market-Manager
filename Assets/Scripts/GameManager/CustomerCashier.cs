using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCashier : MonoBehaviour
{
    public GameObject customer;
    public bool transactionFinished = false;
    [SerializeField] private Cashier cashier;

    private void Update() {
        
    }

    public void AddCustomer(GameObject gameObject)
    {
        transactionFinished = false;

        if(customer == null){
            customer = gameObject;
        }
    }

    public void RemoveCustomer()
    {
        if(customer != null){
            customer = null;
        }
        transactionFinished = true;

        Invoke("TransactionFinished", .1f);
    }

    public void TransactionFinished()
    {
        transactionFinished = false;
    }
}
