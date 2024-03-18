using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpStrength;
    public int score;
    
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

        Debug.Log(score);
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
        Debug.Log("death");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Death"))
        {
            Death();
        }
    }

    #endregion

   #region Scoring
    void Scoring()
    {
        score++;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Scoring"))
        {
            Scoring();
        }
    }

    #endregion
}
