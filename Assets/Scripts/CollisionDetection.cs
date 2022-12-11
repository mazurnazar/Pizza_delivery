using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionDetection : MonoBehaviour
{

    [SerializeField] Camera camera;

    [SerializeField] GameObject player;

    [SerializeField] Throw throwFrom;
    
    [SerializeField] Animator playerAnim;
    [SerializeField] AddPoints addPoints;

    [SerializeField] PlayerMoving playerMoving;
    public delegate void Impact(float speed);
    public event Impact Stop;
    public event Impact SlowSpeed;

   // public bool throwPizza = true;
    // when collides with obstacle
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            transform.GetComponent<Rigidbody>().AddForce((transform.position- collision.gameObject.transform.position), ForceMode.Impulse);
            camera.transform.parent = null;
            Stop?.Invoke(0);
            StartCoroutine(RestartGame());
        }
    }
    // restart game
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);

    }
    // when enters trigger of customer
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Customer")
        {
            addPoints.customer = other.gameObject.GetComponent<Customer>();
            throwFrom = player.GetComponent<Throw>();
            throwFrom.Customer = other.gameObject;
            SlowSpeed?.Invoke(other.GetComponent<Customer>().pizzaNeed);
            playerAnim.SetBool("ThrowPizza", true);
        }
    }
    // exits trigger
    private void OnTriggerExit(Collider other)
    {
        playerAnim.SetBool("ThrowPizza", false);
    }


}
