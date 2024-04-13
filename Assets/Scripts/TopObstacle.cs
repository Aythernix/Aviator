using System;
using System.Runtime.ExceptionServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TopObstacle : MonoBehaviour
{

    
    [FormerlySerializedAs("bottomObsatcle")] public GameObject bottomObstatcle;
    public PowerUpInfo powerUpInfo;
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
        Instantiate(bottomObstatcle, new Vector3(transform.GetChild(1).position.x, bottomSpawn), quaternion.identity, transform);
        Checker();
    }

    private void Checker() // Checks if the position of the bottom obstacle is valid
    {
        // Runs if the distance between both obstacles is less than 15.5units or if the X axis of both obstacles don't match
        // If the check is set too high it will be out of the randomiser range and it will cause an overflow
        if (Vector3.Distance(transform.GetChild(2).transform.GetChild(0).position, transform.GetChild(1).position) < 16 || (int)math.round(transform.GetChild(2).transform.GetChild(0).position.x) != (int)math.round(transform.GetChild(1).position.x))
        {
            // Gets a new position from the "Randomiser" and relocates the bottom obstacle
            Randomiser();
            transform.GetChild(2).position = new Vector3(transform.position.x, bottomSpawn);
            
            
            // Checks if the new position is valid
            Checker();
            
            
            // This function will repeat until a valid position is found
        }
        else
        {
            // If the position of the bottom obstacle is valid PowerUps run
            PowerUps();
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

    private void PowerUps() // Decides whether to spawn in a power up and which one
    {
        // Chooses a number between 1 and 25
        int chance = Random.Range(1, 11);
        
        // Runs if the chance is equal to one
        if (chance == 1)
        {
            // Chooses what powerup to spawn
            int choice = Random.Range(1, 3);
            
            // Executes the function depending on the power up
            switch (choice)
            {
                case 1: // Glide
                    Instantiate(powerUpInfo.GlideData.prefab , new Vector3(transform.position.x, (transform.GetChild(1).position.y + transform.GetChild(2).transform.GetChild(0).position.y) / 2), quaternion.identity, transform);
                    break;
                case 2: // Slow
                    Instantiate(powerUpInfo.SlowData.prefab, new Vector3(transform.position.x, (transform.GetChild(1).position.y + transform.GetChild(2).transform.GetChild(0).position.y) / 2), quaternion.identity, transform);
                    break;
            }
        }
    }
}
