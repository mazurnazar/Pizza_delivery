using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public int currentPizzaNumber = 0;
    [SerializeField] ObjectPool objectPool;
    [SerializeField] GameObject hand;
    [SerializeField] PlayerMoving playerMoving;
    private GameObject pizza;
    private GameObject customer;
    public GameObject Customer { get => customer; set => customer = value; }
    private Animator playerAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        
    }
    // get pizza from pool and give it to hand of player
    public void CreatePizza()
    {
        pizza = objectPool.GetPooledObject();
        pizza.transform.position = hand.transform.position;
        pizza.transform.parent = hand.transform;
    }
    // throw pizza
    void ThrowPizza()
    {
            pizza.transform.eulerAngles = new Vector3(0, 0, 0);
            pizza.transform.parent = null;
            StartCoroutine( pizza.GetComponent<PizzaMoving>().MoveToCustomer(hand, customer));
    }
    // stop throwing when current amount equals to needed amount
    public void StopThrow()
    {
        currentPizzaNumber++;
        if (currentPizzaNumber >= customer.GetComponent<Customer>().pizzaNeed)
        {
            playerAnim.SetBool("ThrowPizza", false);
            playerMoving.ReturnSpeed();

        }
    }
    
}
