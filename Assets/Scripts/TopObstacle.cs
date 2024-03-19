using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TopObstacle : MonoBehaviour
{

    
    public GameObject bottomObsatcle;
    

    private float bottomSpawn;
    
    #region Unity Functions
    // Start is called before the first frame update
    void Start()
    {
        BottomSpawner(); // Calls the "BottomSpawner function
    }

    // Update is called once per frame
    void Update()
    {
    }
    #endregion
    
    #region Bottom Obstacle Spawn
    private void BottomSpawner() // Spawns the bottom obstacle
    {
        // Calls the "Randomiser" fanction
        Randomiser(); 
        
        // Spawns the bottom obstacle and calls the "Checker" function
        Instantiate(bottomObsatcle, new Vector3(transform.GetChild(1).position.x, bottomSpawn), quaternion.identity, transform);
        Checker();
    }

    private void Checker() // Checks if the position of the bottom obstacle is valid
    {
        // Runs if the distance between both obstacles is less than 15.5units or if the X axis of both obstacles don't match
        // If the check is set too high it will be out of the randomiser range and it will cause an overflow
        if (Vector3.Distance(transform.GetChild(2).transform.GetChild(0).position, transform.GetChild(1).position) < 15.5|| (int)math.round(transform.GetChild(2).transform.GetChild(0).position.x) != (int)math.round(transform.GetChild(1).position.x))
        {
            // Gets a new position from the "Randomiser" and relocates the bottom obstacle
            Randomiser();
            transform.GetChild(2).position = new Vector3(transform.position.x, bottomSpawn);
            
            
            // Checks if the new position is valid
            Checker();
            
            
            // This function will repeat until a valid position is found
        }
    }

    private void Randomiser() // Generates a random number for the spawn position
    {
        // Sets the "bottomSpawn" to a random number between -9.2 and -2
        bottomSpawn = Random.Range(-10.5f, -2);
    }
    #endregion

    #region colision
    private void OnTriggerEnter2D(Collider2D col) // runs on collision with a trigger
    {
        // Runs if the collision matches the tag
        if (col.gameObject.CompareTag("Boundary"))
        {
            // Destroys the obstacle
            Destroy(gameObject); 
        }
    }
    #endregion
}
