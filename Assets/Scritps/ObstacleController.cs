using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed;
    
    #region Unity Functions
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
         Move();
    }
    #endregion

    #region Movement
    void Move()
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
    }
    #endregion
}
