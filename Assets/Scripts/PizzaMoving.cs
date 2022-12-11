using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaMoving : MonoBehaviour
{
    float lerp = 0;
 
    // move pizza from hand to customer with Vector3 lerp
    public IEnumerator MoveToCustomer(GameObject hand, GameObject Customer)
    {
        float yPos = Customer.GetComponent<Customer>().pizzas.Count/10f;
        while (lerp <= 1)
        {
            transform.position = Vector3.Lerp(hand.transform.position, new Vector3(Customer.transform.position.x - 1, yPos, Customer.transform.position.z), lerp);
            transform.Rotate(Vector3.up, 50);
            lerp += 0.1f;
            yield return null;
        }
        transform.position = new Vector3(Customer.transform.position.x - 1, yPos, Customer.transform.position.z);

        Customer.GetComponent<Customer>().AddPizzas(this.gameObject);
    }

}
