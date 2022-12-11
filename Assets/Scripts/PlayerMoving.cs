using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    [SerializeField] Animator bikeAnim, playerAnim;
    [SerializeField] private float speed = 1;
    public float currentSpeed;
    public float Speed { get => speed; set => speed = value; }
    [SerializeField] GameObject player;

    [SerializeField] JoystickController joystickController;
    [SerializeField] CollisionDetection collisionDetection;

    // Start is called before the first frame update
    void Awake()
    {
        // subscribe to events of changing speed, slowing, and stop
        joystickController.ChangeSpeed += ChangeSpeed;
        collisionDetection.SlowSpeed += SlowSpeed;
        collisionDetection.Stop += StopAnim;
    }

    // Update is called once per frame
    void Update()
    {
        if(speed>0)
        MoveForward();
    }
    // change speed in accordance of joystick move
    void ChangeSpeed()
    {
        speed = joystickController.Speed>0? joystickController.Speed:0;
        bikeAnim.SetFloat("speed", speed);
        playerAnim.SetFloat("speed", speed);
    }
    //slowdown
    void SlowSpeed(float toChange)
    {
        currentSpeed = speed;
        speed /= toChange/2;       
    }
    // retutrn the original value of speed
    public void ReturnSpeed()
    {
        speed = currentSpeed;
    }
    
    // move forward
    void MoveForward()
    {
        player.transform.position += Vector3.forward * speed *5*  Time.deltaTime;
    }
    public void StopAnim(float speedToChange)
    {
        speed = speedToChange;
        playerAnim.SetBool("Stop", true);
        bikeAnim.SetBool("Stop", true);
    }
}
