using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

public class Cashier : MonoBehaviour
{
    [SerializeField] private GameObject customerSide;
    [SerializeField] private GameObject employeeSide;
    private CustomerCashier customerCashierScript;
    private EmployeeCashier employeeCashierScript;
    public GameObject customer;
    public GameObject employee;

    public float money;
    public bool alreadyTransaction;
    [SerializeField] private MoneyManager moneyManager;
    private bool isTransactionInProgress = false;

    private void Start() {
        customerCashierScript = customerSide.GetComponent<CustomerCashier>();
        employeeCashierScript = employeeSide.GetComponent<EmployeeCashier>();
    }

    public void Update() {
        if (customerCashierScript.customer || employeeCashierScript.employee) {
            if (customerCashierScript.customer != null) {
                Debug.Log("Customer: " + customerCashierScript.customer.name);
            } else {
                Debug.Log("No Customer Assigned.");
            }
            
            if (employeeCashierScript.employee != null) {
                Debug.Log("Employee: " + employeeCashierScript.employee.name);
            } else {
                Debug.Log("No Employee Assigned.");
            }
        }

        // Ensure both are not null before processing
        if (customerCashierScript.customer != null && employeeCashierScript.employee != null) {
            Debug.Log("Transaction in progress...");
            isTransactionInProgress = true;
            Transaction();
        }
    }

    public void Transaction() {
        customer = customerCashierScript.customer;
        employee = employeeCashierScript.employee;

        if (customer == null || employee == null) {
            Debug.LogWarning("Transaction failed: Missing customer or employee.");
            return;
        }

        if(customer && employee){
            Debug.Log("Transaction started...");
            // Process the transaction
            money = customerCashierScript.customer.GetComponent<CustomerBehaviour>().totalCost;
            moneyManager.ProfitMoney(money);

            customerCashierScript.RemoveCustomer();
            employeeCashierScript.RemoveEmployee();
            Debug.Log("Transaction Completed!");

            customer = null;
            employee = null;
            money = 0;
        }

        isTransactionInProgress = false; // Reset after completion
    }

    // public void Update() {
    //     if(customerCashierScript.customer || employeeCashierScript.employee){
    //         Debug.Log(customerCashierScript.customer.name);
    //         Debug.Log(employeeCashierScript.employee.name);
    //     }
       

    //     if(customerCashierScript.customer != null && employeeCashierScript.employee != null){
    //         Transaction();
    //     }
    // }

    // public void Transaction()
    // {
    //     money = customerCashierScript.customer.GetComponent<CustomerBehaviour>().totalCost;
    //     moneyManager.ProfitMoney(money);

    //     customerCashierScript.RemoveCustomer();
    //     employeeCashierScript.RemoveEmployee();

    //     customer = null;
    //     employee = null;

    //     money = 0;
    //     Debug.Log("Transaksi");
    // }
}
