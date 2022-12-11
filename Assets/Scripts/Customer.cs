using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public int pizzaNeed = 1; // number of pizza needed
    public List<GameObject> pizzas; // list of pizzas
    private int pizzaCost = 10; // cost of one pizza
    [SerializeField] AddPoints addPoints; 

    void Start()
    {
        pizzas = new List<GameObject>();
    }
    // add pizza to list
    public void AddPizzas(GameObject pizza)
    {
        pizzas.Add(pizza);
        pizza.transform.parent = transform;
        addPoints.ShowMoney(pizzaCost);
    }
}
