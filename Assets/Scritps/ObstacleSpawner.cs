using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    private int _time;
    private float _spawnPoint;
    public GameObject obstacle;

    private float _subtractor = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawner()
    {
        _spawnPoint = Random.Range(9, 3);
        _time = Random.Range(1, 3);
        yield return new WaitForSeconds(_time - _subtractor);

        Instantiate(obstacle, new Vector3(10.5f, _spawnPoint), quaternion.identity);
        StartCoroutine(Spawner());

        yield return new WaitForSeconds(30);
        _subtractor =- 0.5f;
    }
}
