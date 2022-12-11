using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunCars : MonoBehaviour
{
    [SerializeField] GameObject player;
    private bool activateCars=false;
    [SerializeField] ObjectPool [] objectPool;
    [SerializeField]CollisionDetection collisionDetection;
    private Vector3 leftPosition, rightPosition;
    

    void Update()
    {
        if(!activateCars&&Vector3.Distance(transform.position,player.transform.position)<50)
        {
            activateCars = true;
            StartCoroutine(StartCars(leftPosition, new Vector3(0,90,0))); // start cars from left side
            StartCoroutine(StartCars(rightPosition, new Vector3(0, -90, 0))); // start cars from right side
        }
    }
    private void Start()
    {
        leftPosition = new Vector3(2.5f, 1, -5);
        rightPosition = new Vector3(-2.5f, 1, 5);
        collisionDetection.Stop += StopCars;// subscribe to event stop
    }
    IEnumerator StartCars(Vector3 position, Vector3 rotation)
    {
        int carNumber = Random.Range(0, 4);
        GameObject car = objectPool[carNumber].GetPooledObject(); // get car from random pool of cars
        car.GetComponent<CarMoving>().ObjectPool = objectPool[carNumber];
        // set position? rotation and parent
        car.transform.localEulerAngles = rotation;
        car.transform.parent = transform;
        car.transform.localPosition = position;
        yield return new WaitForSeconds(Random.Range(1f, 2));
        // if player in field of view start again
        if (Vector3.Distance(transform.position, player.transform.position) < 50)
            StartCoroutine(StartCars(position,rotation));
    }
    void StopCars(float stop)
    {
        StopAllCoroutines();
    }

}
