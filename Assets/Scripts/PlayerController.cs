using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpStrength;
    public int score;
    private bool _DenyJump;
    public TMP_Text text;
    
    
    
    #region Untiy Functions
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jumping();
        }

        _DenyJump = GetComponent<PlayerPowerUps>().denyJump;
        
        #region Angle
        
        // Runs if the Y velocity is bigger than 1.5 and space isn't being held
        if (rb.velocity.y > 1.5 && !_DenyJump)
        {
            // Makes the nose increase
            rb.angularVelocity = 30;
        }
        // Runs if the Y velocity is in-between 1.5 and -1.5
        else if (rb.velocity.y < 1.5 && rb.velocity.y > -1.5)
        {
            // Makes the angular velocity 0
            rb.angularVelocity = 0;
        }
        // runs if the Y velocity is less than -1.5 and space isn't being held
        else if (rb.velocity.y < -1.5 && !_DenyJump)
        {
            // Makes the nose drop
            rb.angularVelocity = -50;
        }
        
        // Runs if the rotation is in-between 13 and 20
        if (transform.eulerAngles.z > 13 && transform.eulerAngles.z < 20)
        {
            // Makes the rotation 13 
            transform.eulerAngles = new Vector3(0, 0, 13);
        }
        // Runs if the rotation is in-between -13 and -14
        else if (transform.eulerAngles.z > 346 && transform.eulerAngles.z < 347)
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
        rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
    }
    #endregion

    #region Death
    void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    #endregion

   #region Scoring
    void Scoring()
    {
        // Increases score by one and updates the UI
        score++;
        text.text = score.ToString();
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
