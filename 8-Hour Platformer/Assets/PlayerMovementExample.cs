using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float speed = 6f;    //Speed of player
    public Text scoreText;      //The score text element
    public GameObject[] Coll;   //All the collectables in the level

    int maxScore;
    int currentScore;          //Number of collectables
    Vector3 spawn;              //Spawn points
    Vector3 movement;           //Used for moving the player
    Rigidbody playerRigidbody;  //Player's rigid body
    float maxSpeed = 10;

    void Awake()
    {
        spawn = transform.position;                     //Spawn point
        playerRigidbody = GetComponent<Rigidbody>();    //Player's Rigid body
    }

    void FixedUpdate()
    {
    //For three D movement but can be changed
        //float h = Input.GetAxisRaw("Horizontal");//Input for horizontal movement
        //float v = Input.GetAxisRaw("Vertical");// Input 
        //movement = new Vector3(h, 0f, v);

        //if (playerRigidbody.velocity.magnitude < maxSpeed)  //Keeps player at a speed
        //{
        //    playerRigidbody.AddForce(movement * speed);
        //}
    }

    void Die()//Death and set to spawn point
    {
        Instantiate(deathParticals, transform.position, Quaternion.identity);
        transform.position = spawn;
    }

    void OnTriggerEnter(Collider other) //Used to see of what the player has triggered an object
    {
        if (other.gameObject.CompareTag("Collect"))//Collecting object
        {
            other.gameObject.SetActive(false);
            currentScore -= 1;
            SetScoreText();
        }

        if (other.transform.tag == ("Goal") && currentScore <= 0)//Goal trigger
        {
            LevelController.CompleteLevel();
        }
    }

    void OnCollisionEnter(Collision other)  //Used to detect Enemies who collide with the player
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Die();
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
