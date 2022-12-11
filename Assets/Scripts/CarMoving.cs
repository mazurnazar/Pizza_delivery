using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMoving : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] ObjectPool objectPool;

    [SerializeField] GameObject player;
    [SerializeField] CollisionDetection collisionDetection;
    public ObjectPool ObjectPool { get => objectPool; set => objectPool = value; }
    private float zMax =6;
    private float position;

    void Start()
    {
        collisionDetection =GameObject.Find("Player").GetComponent<CollisionDetection>();
        collisionDetection.Stop += StopCar;
        position = transform.localPosition.z;
    }
   
    // Update is called once per frame
    void Update()
    {
        MoveCar();
    }
    // move car forward
    void MoveCar()
    {
        if (transform.localPosition.z > zMax || transform.localPosition.z < -zMax) objectPool.ReturnToPool(gameObject);
        if (position < 0)
            transform.position += Vector3.right * speed * Time.deltaTime;
        else transform.position -= Vector3.right * speed * Time.deltaTime;
    }
    void StopCar(float stop)
    {
        speed = stop;
    }
}
