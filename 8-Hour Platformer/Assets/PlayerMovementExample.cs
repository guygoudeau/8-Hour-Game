using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovementExample : MonoBehaviour {

    public float speed = 6f;    //Speed of player
    public GameObject[] Coll;   //All the collectables in the level
    public float jumpHeight = 10f;

    int maxScore;
    int currentScore;          //Number of collectables
    Vector3 spawn;              //Spawn points
    Vector2 movement;           //Used for moving the player
    Rigidbody playerRigidbody;  //Player's rigid body
    float maxSpeed = 10;
    bool playerGrounded = false;

    void Awake()
    {
        spawn = transform.position;                     //Spawn point
        playerRigidbody = GetComponent<Rigidbody>();    //Player's Rigid body
    }

    void FixedUpdate()
    {
        //For three D movement but can be changed
        float h = Input.GetAxisRaw("Horizontal");//Input for horizontal movement
        float v = Input.GetAxisRaw("Vertical");// Input

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerRigidbody.velocity = new Vector2(h, jumpHeight);
        }

        movement = new Vector2(h, v);

        if (playerRigidbody.velocity.magnitude < maxSpeed)  //Keeps player at a speed
        {
            playerRigidbody.AddForce(movement * speed);
        }
    }

    void Die()//Death and set to spawn point
    {
        //Instantiate(deathParticals, transform.position, Quaternion.identity);
        transform.position = spawn;
    }

    void OnTriggerEnter(Collider other) //Used to see of what the player has triggered an object
    {
        if (other.gameObject.CompareTag("Collect"))//Collecting object
        {
            other.gameObject.SetActive(false);
            currentScore -= 1;

        }

        if (other.transform.tag == ("Goal") && currentScore <= 0)//Goal trigger
        {
            //LevelController.CompleteLevel();
        } 
    }

    void OnCollisionStay(Collision other)  //Used to detect Enemies who collide with the player
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Die();
        }

        if(other.gameObject.CompareTag("Ground"))
        {
            playerGrounded = true;
        }
    }

    void ResetCollect()//Resetting the collectables to active
    {
        for(int i = 0; currentScore >= 0; i++)
        {
            Coll[i].SetActive(true);
        }
    }
}
