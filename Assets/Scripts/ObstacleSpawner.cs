using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    public float _time;
    private float _spawnPoint;
    public GameObject obstacle;

    private float _subtractor = 0;
    
    #region Unity Functions
    // Start is called before the first frame update
    void Start()
    {
        // Calls the "Spawner" function
        StartCoroutine(Spawner());
        
        // Calls the "Subtractor" function
        StartCoroutine(Subtractor());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Obstacle Spawner
    IEnumerator Spawner()
    {
        // Sets the spawn point to a random number between 9, 3
        _spawnPoint = Random.Range(10.5f, 5);
        // Sets the spawn time interval to a random number between 1, 3
        _time = Random.Range(2, 4 - _subtractor);
        
        // Waits for the random number between 1,3 and subtracts it by the subtracor
        yield return new WaitForSeconds(_time);

        // Spawns the top obstacle on the random spawn point
        Instantiate(obstacle, new Vector3(10.5f, _spawnPoint), quaternion.identity);
        
        
        // Restarts the "Spawner" function
        StartCoroutine(Spawner());
    }

    IEnumerator Subtractor() // Makes the subtractor increase by 0.1
    {
        // Waits for 30 seconds
        yield return new WaitForSeconds(15);
        // Subtracts the subtractor by 0.5
        _subtractor += 0.3f;
        // Restarts the "Subtractor" function
        StartCoroutine(Subtractor());
    }
    #endregion
}
