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
    public float distance;

    private float bottomSpawn;
    // Start is called before the first frame update
    void Start()
    {
        BottomSpawner();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.GetChild(0).position, transform.position);
    }

    private void BottomSpawner()
    {
        Randomiser();
        
        Instantiate(bottomObsatcle, new Vector3(transform.position.x, bottomSpawn), quaternion.identity, transform);
        Checker();
    }

    private void Checker()
    {
        if (Vector3.Distance(transform.GetChild(0).position, transform.position) < 12.4 || (int)math.round(transform.GetChild(0).position.x) != (int)math.round(transform.position.x))
        {
            Randomiser();
            transform.GetChild(0).position = new Vector3(transform.position.x, bottomSpawn);
            
            Checker();
        }
        
        
    }

    private void Randomiser()
    {
        bottomSpawn = Random.Range(-9.2f, -3);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }
}
