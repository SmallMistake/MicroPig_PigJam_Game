using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerHolderController : MonoBehaviour
{
    [SerializeField]
    private GameObject customerPrefab;

    private GameObject currentCustomer;

    public void SetCustomer(Customer customer)
    {
        if(currentCustomer != null)
        {
            currentCustomer.GetComponent<CustomerUI>().ExitStore();
            currentCustomer = null;
        }

        currentCustomer = Instantiate(customerPrefab);
        currentCustomer.transform.SetParent(transform);
        currentCustomer.GetComponent<CustomerUI>().SetCustomer(customer);
    }
}
