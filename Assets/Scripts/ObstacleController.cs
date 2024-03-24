
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float moveSpeed;
    
    #region Unity Functions
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
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
        _rb.velocity = new Vector2(-moveSpeed, _rb.velocity.y);
    }
    #endregion
}
