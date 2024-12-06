using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeCashier : MonoBehaviour
{
    public GameObject employee;
    public bool transactionFinished = false;
    [SerializeField] private CustomerCashier customerCashier;

    private void Update() {
        
    }

    public void AddEmployee(GameObject gameObject)
    {
        transactionFinished = false;

        if(employee == null){
            employee = gameObject;
        }
    }

    public void RemoveEmployee()
    {
        if(employee != null){
            employee = null;
        }
        transactionFinished = true;

        Invoke("TransactionFinished", .1f);
    }

    public void TransactionFinished()
    {
        transactionFinished = false;
    }
}
