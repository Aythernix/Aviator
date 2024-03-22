using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    
    public GameObject timer;
    public bool denyJump;
    public GameObject obstacleSpawner;
    
    private Rigidbody2D rb;
    private GameObject[] obsatclesUp;
    private GameObject[] obsatclesDown;
    
    #region PowerUp vars
    private bool _glide;
    private bool _slow;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_glide) // Runs if _glide is true
        {
            glidePowerUp(); // Runs the glide function every frame
        }

        if (_slow)
        {
            obstacleSpawner.GetComponent<ObstacleSpawner>()._time = 30f;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        #region PowerUps Collision
        
        // Runs the function depending on what powerup has been collided with
        switch (col.gameObject.tag)
        {
            case "Glide":
                _glide = true;
                _slow = false;
                Destroy(col.gameObject);
                break;
            case "Slow":
                _slow = true;
                _glide = false;
                SlowPowerUp();
                Destroy(col.gameObject);
                Debug.Log("test");
                break;
                
        }
        #endregion
    }
    
    #region Power Ups
    private void glidePowerUp() // Manages the glide powerup
    {
        // Runs if W is being held down and the aircraft is straight
        if (Input.GetKey(KeyCode.W) && (transform.eulerAngles.z < 2 && transform.eulerAngles.z > 0.99))
        {
            // Sets Deny Jump to true, and locks the aircraft in space
            denyJump = true;
            rb.angularVelocity = 0;
            rb.velocity = new Vector2(0, 0);
            
            // Starts the timer function
            StartCoroutine(Timer());
        }
        else
        {
            // Sets Deny Jump to false
            denyJump = false;
        }
    }

    private void SlowPowerUp()
    {
        StartCoroutine(Timer());
        obsatclesUp = GameObject.FindGameObjectsWithTag("ObstacleUp");
        if (obsatclesUp != null)
        {
            foreach (var obstacle in obsatclesUp)
            {
                obstacle.gameObject.GetComponent<ObstacleController>().moveSpeed = 2.5f;
            }
        }
        
        obsatclesDown = GameObject.FindGameObjectsWithTag("ObstacleDown");
        if (obsatclesUp != null)
        {
            foreach (var obstacle in obsatclesDown)
            {
                obstacle.gameObject.GetComponent<ObstacleController>().moveSpeed = 2.5f;
            }
        }
        
    }

    private IEnumerator Timer() // Manges the timer
    {
        // Playes the timer animation
        timer.GetComponent<Animator>().Play("Timer");
        
        // Waits 10 seconds
        yield return new WaitForSeconds(10);
        
        // Sets the timer to it's default position
        timer.GetComponent<Animator>().Play("Idle");
        
        // Resets all powerups
        _glide = false;
        denyJump = false;
        _slow = false;

        #region Slow Reset

        obsatclesUp = GameObject.FindGameObjectsWithTag("ObstacleUp");
        if (obsatclesUp != null)
        {
            foreach (var obstacle in obsatclesUp)
            {
                obstacle.gameObject.GetComponent<ObstacleController>().moveSpeed = 5f;
            }
        }
        
        obsatclesDown = GameObject.FindGameObjectsWithTag("ObstacleDown");
        if (obsatclesUp != null)
        {
            foreach (var obstacle in obsatclesDown)
            {
                obstacle.gameObject.GetComponent<ObstacleController>().moveSpeed = 5f;
            }
        }

        #endregion

    }
    #endregion
}
