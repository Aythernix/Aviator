using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Power Ups / Create Power up")]
public class PowerUpData: ScriptableObject
{
    public string powerUpName;
    public Sprite image;
    public float time = 10f;
    public string description;
    public GameObject prefab;
    

}
