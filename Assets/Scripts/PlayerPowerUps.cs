using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    
    public GameObject timer;
    public bool denyJump;
    public PowerUpInfo powerUpInfo;
    
    private Rigidbody2D _rb;
    private bool _running;
    
    
    #region PowerUp vars
    private bool _glide;
    private bool  _slow;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        Reset();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_glide) // Runs if _glide is true
        {
            GlidePowerUp(); // Runs the glide function every frame
        }
        
    }

    
    private void OnTriggerEnter2D(Collider2D col)
    {
        #region PowerUps Collision
        
        // Runs the function depending on what power up has been collided with
        switch (col.gameObject.tag)
        {
            case "Glide":
                Reset(); // Resets the power ups
                _glide = true;
                Destroy(col.gameObject); // Removes the power up from the game
                break;
            case "Slow":
                Reset();
                _slow = true;
                SlowPowerUp();
                Destroy(col.gameObject);
                break;
                
        }
        #endregion
    }
    
    #region Power Ups
    private void GlidePowerUp() // Manages the glide power up
    {
        // Runs if W is being held down and the aircraft is straight
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.RightArrow)) && transform.eulerAngles.z < 2 && transform.eulerAngles.z > 0.99)
        {
            // Sets Deny Jump to true, and locks the aircraft in space
            denyJump = true;
            _rb.angularVelocity = 0;
            _rb.velocity = new Vector2(0, 0);
            
            // Starts the timer function
            StartCoroutine(Timer(powerUpInfo.GlideData.time));
        }
        else
        {
            // Sets Deny Jump to false
            denyJump = false;
        }
    }

    private void SlowPowerUp()
    {
        // Slows down time by half
        Time.timeScale = .5f;
        StartCoroutine(Timer(powerUpInfo.SlowData.time));
    }
    #endregion
    
    #region Timer & Reset
    private IEnumerator Timer(float time) // Manges the timer
    {
        // Only runs if the timer isn't running already
        if (!_running)
        {
            _running = true;
            // Plays the timer animation
            timer.GetComponent<Animator>().Play("Timer");

            // Waits certain seconds
            yield return new WaitForSeconds(time);

            // Sets the timer to it's default position
            timer.GetComponent<Animator>().Play("Idle");

            Reset();
            _running = false;
        }
    }

    public void Reset() // Resets all the perks to default
    {
        #region Glide
        _glide = false;
        denyJump = false;
        #endregion
        
        #region Slow
        _slow = false;
        Time.timeScale = 1f;
        #endregion
    }
    
    #endregion
    
    
    
}
