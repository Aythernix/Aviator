using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerPowerUps : MonoBehaviour
{
    
    public GameObject timer;
    public bool denyJump;
    public PowerUpInfo powerUpInfo;
    public Image currentlyActiveImage;
    public Image reserveImage;
    public TMP_Text overrideText;
    public GameObject sound;
    
    private Rigidbody2D _rb;
    private bool _running;
    private string _currentlyActive;
    private bool _override;
    private PlayerInput _playerInput;
    private InputActions _inputActions;
    private float _mobilePowerUpPos;
    
    
    #region PowerUp vars
    private bool _glide;
    private bool _slow;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        _inputActions = new InputActions();
        _inputActions.Player.Enable();
        _inputActions.Player.PowerUp.performed += PowerUpOnperformed;
        _inputActions.Player.PowerUp.canceled += PowerUpOncanceled;
        _inputActions.Player.Override.performed += OverrideOnperformed;
        _inputActions.Player.MobilePowerup.performed += MobilePowerUpperformed;
        _inputActions.Player.MobilePowerup.canceled += MobilePowerupOncanceled;
        
        Reset();
    }

   


    private void OnDisable()
    {
        _inputActions.Player.PowerUp.performed -= PowerUpOnperformed;
        _inputActions.Player.PowerUp.canceled -= PowerUpOncanceled;
        _inputActions.Player.Override.performed -= OverrideOnperformed;
        _inputActions.Player.MobilePowerup.performed -= MobilePowerUpperformed;
        _inputActions.Player.MobilePowerup.canceled -= MobilePowerupOncanceled;
        _inputActions.Player.Disable();
    }

  

    #region Controls
    private void PowerUpOnperformed(InputAction.CallbackContext context)
    {
        
    }
    
    private void PowerUpOncanceled(InputAction.CallbackContext obj)
    {
        denyJump = false; // Allows the player to jump again
    }
    
    private void MobilePowerUpperformed(InputAction.CallbackContext obj)
    {
        _mobilePowerUpPos = _inputActions.Player.MobilePowerup.ReadValue<Vector2>().x;
    }
    
    private void MobilePowerupOncanceled(InputAction.CallbackContext obj)
    {
        _mobilePowerUpPos = 0;
    }
    #endregion
    
    private void OverrideOnperformed(InputAction.CallbackContext obj)
    {
        if (_override)
        {
            _override = false;
        }
    }
    
    

    // Update is called once per frame
    void Update()
    {
        // Runs if the the powerup buttons are pressed or if the right left side of the screen is pressed 
        if (_inputActions.Player.PowerUp.ReadValue<float>() != 0 || (_mobilePowerUpPos < 800f && _mobilePowerUpPos != 0))
        {
            if (_glide) // Runs if _glide is true
            {
                GlidePowerUp(); // Runs the glide function every frame
            }
        
            if (_slow)
            {
                SlowPowerUp();
            }
        }
        //Debug.Log(_mobilePowerUpPos);
    }

    
    private void OnTriggerEnter2D(Collider2D col)
    {
        #region PowerUps Collision
        
        // Runs the function depending on what power up has been collided with
        switch (col.gameObject.tag)
        {
            case "Glide":
                sound.GetComponent<AudioSource>().Play();
                col.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                col.gameObject.GetComponent<Collider2D>().enabled = false;
                StartCoroutine(Override(type: "Glide"));
                break;
            
            case "Slow":
                sound.GetComponent<AudioSource>().Play();
                col.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                col.gameObject.GetComponent<Collider2D>().enabled = false;
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
    public void OverrideButton()
    {
        if (_override)
        {
            _override = false;
        }
    }
}
