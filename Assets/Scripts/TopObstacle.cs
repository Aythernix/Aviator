using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
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
        //Debug.Log(Vector3.Distance(transform.GetChild(2).transform.GetChild(1).position, transform.GetChild(1).position));
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
        // Runs if the distance between both obstacles is less than 12.4 units or if the X axis of both obstacles don't match
        if (Vector3.Distance(transform.GetChild(2).transform.GetChild(0).position, transform.GetChild(1).position) < 12.4 || (int)math.round(transform.GetChild(2).transform.GetChild(0).position.x) != (int)math.round(transform.GetChild(1).position.x))
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
        bottomSpawn = Random.Range(-9.2f, -2);
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D col) // runs on collision with a trigger
    {
        // Runs if the collision matches the tag
        if (col.gameObject.CompareTag("Boundary"))
        {
            // Destroys the obstacle
            Destroy(gameObject); 
        }
    }
}
