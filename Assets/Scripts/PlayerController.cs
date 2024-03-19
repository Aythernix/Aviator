using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpStrength;
    public int score;
    private bool _spaceCheck;
    
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

        if (Input.GetKey(KeyCode.W) && (transform.eulerAngles.z < 2 && transform.eulerAngles.z > 0.99) )
        {
            _spaceCheck = true;
            rb.angularVelocity = 0;
            rb.velocity = new Vector2(0, 0);
        }
        else
        {
            _spaceCheck = false;
        }
        
        
        #region Angle
        if (rb.velocity.y > 1.5 && !_spaceCheck)
        {
            rb.angularVelocity = 30;
        }
        else if (rb.velocity.y < 1.5 && rb.velocity.y > -1.5)
        {
            rb.angularVelocity = 0;
        }
        else if (rb.velocity.y < -1.5 && !_spaceCheck)
        {
            rb.angularVelocity = -50;
        }
        
        if (transform.eulerAngles.z > 13 && transform.eulerAngles.z < 20)
        {
          transform.eulerAngles = new Vector3(0, 0, 13);
        }
        else if (transform.eulerAngles.z > 346 && transform.eulerAngles.z < 347)
        {
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
        score++;
    }
    
   #endregion

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Scoring"))
        {
            Scoring();
        }
        
        if (col.gameObject.CompareTag("Death") || col.gameObject.CompareTag("ObstacleDown") || col.gameObject.CompareTag("ObstacleUp"))
        {
            Death();
        }
    }

    
}
