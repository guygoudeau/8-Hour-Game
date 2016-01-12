using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float speed = 6f;    //Speed of player
    public GameObject[] Coll;   //All the collectables in the level
    public float jumpHeight = 10f;
    public GameObject deathParticals;
    public Text winText;

    int maxScore;
    int currentScore;          //Number of collectables
    Vector3 spawn;              //Spawn points
    Vector2 movement;           //Used for moving the player
    Rigidbody playerRigidbody;  //Player's rigid body
    float maxSpeed = 10;
    bool jump = true;
    int currentLevel;

    void Awake()
    {
        currentLevel = LevelController.currentLevel;
        winText.text = "";
        maxScore = Coll.Length;
        currentScore = maxScore;
        spawn = transform.position;                     //Spawn point
        playerRigidbody = GetComponent<Rigidbody>();    //Player's Rigid body
    }

    void FixedUpdate()
    {
        
        //For three D movement but can be changed
        float h = Input.GetAxisRaw("Horizontal");//Input for horizontal movement
        float v = Input.GetAxisRaw("Vertical");
       
        if(jump == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                playerRigidbody.velocity = new Vector2(h, jumpHeight);
                jump = false;
            }
        }    
       
        movement = new Vector2(h, v);

        if (playerRigidbody.velocity.magnitude < maxSpeed)  //Keeps player at a speed
        {
            playerRigidbody.AddForce(movement * speed);
        }

        if(playerRigidbody.position.y <= -25f)
        {
            Die();
        }

        if (currentLevel >= 4)
        {
            winText.text = "You Win!!!";
        }
    }

    void Die()//Death and set to spawn point
    {
        Instantiate(deathParticals, transform.position, Quaternion.identity);
        transform.position = spawn;
        ResetCollect();
        currentScore = maxScore;
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
            LevelController.CompleteLevel();
        } 
    }

    void OnCollisionEnter(Collision other)  //Used to detect Enemies who collide with the player
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Die();
        }

        else if(other.gameObject.CompareTag("Ground"))
        {
            jump = true;
        }
    }

    void ResetCollect()//Resetting the collectables to active
    {
        for(int i = 0; i < Coll.Length ; i++)
        {
            Coll[i].SetActive(true);
        }
    }
}
