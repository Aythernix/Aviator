
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float jumpStrength;
    public PlayerData playerData;
    private bool _denyJump;
    public TMP_Text text;
    private bool start;
    public GameManager gm;
    
    public AudioClip explosion;
    public AudioClip defaultSound;
    public AudioSource sound;
    
    
    
    #region Untiy Functions
    // Start is called before the first frame update
    void Start()
    {
        playerData.DataReset();
        _rb = GetComponent<Rigidbody2D>();
        sound.clip = defaultSound;
        sound.loop = true;
        sound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (start == false)
        {
            start = gm.start;
        }
        if (!start)
        {
            _rb.velocity = new Vector2(0, 0);
            transform.position = new Vector3(0, 0, 0);
            _denyJump = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jumping();
        }

        _denyJump = GetComponent<PlayerPowerUps>().denyJump;
        
        #region Angle
        
        // Runs if the Y velocity is bigger than 1.5 and space isn't being held
        if (_rb.velocity.y > 1.5 && !_denyJump)
        {
            // Makes the nose increase
            _rb.angularVelocity = 30;
        }
        // Runs if the Y velocity is in-between 1.5 and -1.5
        else if (_rb.velocity.y < 1.5 && _rb.velocity.y > -1.5)
        {
            // Makes the angular velocity 0
            _rb.angularVelocity = 0;
        }
        // runs if the Y velocity is less than -1.5 and space isn't being held
        else if (_rb.velocity.y < -1.5 && !_denyJump)
        {
            // Makes the nose drop
            _rb.angularVelocity = -50;
        }
        
        // Runs if the rotation is in-between 13 and 20
        if (transform.eulerAngles.z is > 13 and < 20)
        {
            // Makes the rotation 13 
            transform.eulerAngles = new Vector3(0, 0, 13);
        }
        // Runs if the rotation is in-between -13 and -14
        else if (transform.eulerAngles.z is > 346 and < 347)
        {
            // Sets the rotation to -13
            transform.eulerAngles = new Vector3(0, 0, -13);
        }
        
        #endregion
    }
    #endregion

    #region Jumping
    void Jumping()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, jumpStrength);
    }
    #endregion

    #region Death
    void Death()
    {
        playerData.SetHighScore();
        sound.loop = false;
        sound.clip = explosion;
        sound.Play();
        Time.timeScale = 0;
        SceneManager.LoadSceneAsync("DeathScene", LoadSceneMode.Additive);
        
    }

    #endregion

   #region Scoring
    void Scoring()
    {
        // Increases score by one and updates the UI
        playerData.score++;
        text.text = playerData.score.ToString();
    }
    
   #endregion

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Runs if the object increases score
        if (col.CompareTag("Scoring"))
        {
            Scoring();
        }

        if (col.CompareTag("Death") || col.CompareTag("ObstacleUp") || col.CompareTag("ObstacleDown"))
        {
            Death();
        }
    }
}
