using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    private float _time;
    private float _spawnPoint;
    public GameObject obstacle;
    public GameManager gm;

    private float _subtracter = 0;
    private bool _start = false;
    
    #region Unity Functions
    // Start is called before the first frame update
    void Start()
    {
        // Calls the "Spawner" function
        StartCoroutine(Spawner());
        
        // Calls the "Subtracter" function
        StartCoroutine(Subtracter());
    }

    // Update is called once per frame
    void Update()
    {
        if (_start == false)
        {
            _start = gm.start;
        }
    }
    #endregion

    #region Obstacle Spawner

    private IEnumerator Spawner()
    {
        yield return new WaitUntil(() => _start);
        // Sets the spawn point to a random number between 9, 3
        _spawnPoint = Random.Range(10.5f, 5);
        // Sets the spawn time interval to a random number between 1, 3
        _time = Random.Range(2, 4 - _subtracter);
        
        // Waits for the random number between 1,3 and subtracts it by the subtracer
        yield return new WaitForSeconds(_time);

        // Spawns the top obstacle on the random spawn point
        Instantiate(obstacle, new Vector3(10.5f, _spawnPoint), quaternion.identity);
        
        
        // Restarts the "Spawner" function
        StartCoroutine(Spawner());
    }

    private IEnumerator Subtracter() // Makes the subtracter increase by 0.1
    {
        // Waits for 30 seconds
        yield return new WaitForSeconds(15);
        // Subtracts the subtracter by 0.5
        _subtracter += 0.3f;
        // Restarts the "Subtracter" function
        StartCoroutine(Subtracter());
    }
    #endregion
}
