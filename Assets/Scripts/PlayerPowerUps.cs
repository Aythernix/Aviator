using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerUps : MonoBehaviour
{
    
    public GameObject timer;
    public bool denyJump;
    public PowerUpInfo powerUpInfo;
    public Image currentlyActiveImage;
    public Image reserveImage;
    public TMP_Text overrideText;
    
    private Rigidbody2D _rb;
    private bool _running;
    private string _currentlyActive;
    private bool _powerUpHoldButton;
    private bool _powerUpButton;
    private bool _override;
    
    
    #region PowerUp vars
    private bool _glide;
    private bool _slow;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        #region Power Up Button
        
        // These are true if W, RMB or right arrow are pressed/held
        _powerUpHoldButton = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.RightArrow);
        _powerUpButton = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.RightArrow);
        #endregion
        
        #region Hold Powerup Button
        if (_powerUpHoldButton)
        {
            if (_glide) // Runs if _glide is true
            {
                GlidePowerUp(); // Runs the glide function every frame
            }
        }
        else
        {
            denyJump = false; // Allows the player to jump again
        }
        #endregion
        

        #region Press Poweup Button
        if (_powerUpButton)
        {
            if (_slow)
            {
                SlowPowerUp();
            }
        }
        else
        {
            
        }
        #endregion

        if (_override)
        {
            // Overrides the currently active powerup if "enter" is pressed
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _override = false;
            }
        }
        
    }

    
    private void OnTriggerEnter2D(Collider2D col)
    {
        #region PowerUps Collision
        
        // Runs the function depending on what power up has been collided with
        switch (col.gameObject.tag)
        {
            case "Glide":
                col.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                StartCoroutine(Override(type: "Glide"));
                break;
            
            case "Slow":
                col.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                StartCoroutine(Override(type: "Slow"));
                break;
                
        }
        #endregion
    }
    
    #region Power Ups
    private void GlidePowerUp() // Manages the glide power up
    {
        // Runs if W is being held down and the aircraft is straight
        if (transform.eulerAngles.z < 2 && transform.eulerAngles.z > 0.99)
        {
            // Sets Deny Jump to true, and locks the aircraft in space
            denyJump = true;
            _rb.angularVelocity = 0;
            _rb.velocity = new Vector2(0, 0);
            
            // Starts the timer function
            StartCoroutine(Timer(powerUpInfo.GlideData.time));   
        }
    }

    private void SlowPowerUp() // Manages the slow power up
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
            
            Reset();
            _running = false;
        }
    }

    public void Reset() // Resets all the values to default
    {
        // Sets the timer to it's default position
        timer.GetComponent<Animator>().Play("Idle");
        
        #region Glide
        _glide = false;
        denyJump = false;
        #endregion
        
        #region Slow
        _slow = false;
        Time.timeScale = 1f;
        #endregion
        
        _currentlyActive = null;
        reserveImage.enabled = false;
        currentlyActiveImage.enabled = false;
        overrideText.enabled = false;
    }
    
    #endregion

    private IEnumerator Override(string type)
    {
        // Runs if there is already an active sprite and there isn't a reserve sprite
        if (_currentlyActive != null && !_override)
        {
            // Enables the override, shows and sets the UI images to the power up image.
            _override = true;
            reserveImage.enabled = true;
            reserveImage.sprite = powerUpInfo.GlideData.sprite;
            overrideText.enabled = true;
            switch (type)
            {
                case "Glide":
                    reserveImage.sprite = powerUpInfo.GlideData.sprite;
                    break;
                case "Slow":
                    reserveImage.sprite = powerUpInfo.SlowData.sprite;
                    break;
            }
                    
            // Waits until either there is no current powerup, or the current powerup is overriden 
            yield return new WaitUntil(() => _currentlyActive == null || !_override);

            // if a powerup is currently being used it waits until it's finished
            if (_running)
            {
                yield return new WaitUntil(() => !_running);
            }
            _override = false;
        }
                
        // Runs if override is disabled
        if (!_override)
        {
            // Resets the powerups, shows and sets the currently active UI and enables the powerup.
            Reset();
            _currentlyActive = type; // Sets the currently active powerup
            currentlyActiveImage.enabled = true;
            switch (type)
            {
                case "Glide":
                    _glide = true;
                    currentlyActiveImage.sprite = powerUpInfo.GlideData.sprite;
                    break;
                case "Slow":
                    _slow = true;
                    currentlyActiveImage.sprite = powerUpInfo.SlowData.sprite;
                    break;
            }
        }
    }
}
