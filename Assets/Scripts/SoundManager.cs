using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public GameObject[] sound;
    
    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = ValueStore.Volume;
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = ValueStore.Volume;
    }
}
