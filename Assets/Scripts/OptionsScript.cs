using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class OptionsScript : MonoBehaviour
{
    public AudioSource sound;
    public Slider slider;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        Time.timeScale = 0;
        slider.value = ValueStore.Volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Close()
    {
        StartCoroutine(_Close());
    }

    private IEnumerator _Close()
    {
        sound.Play();
        yield return new WaitForSecondsRealtime(0.3f);
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(sceneBuildIndex: 3);
    }

    public void Volume()
    {
        ValueStore.Volume = slider.value;

    }
    
    
    
    
    
    
}
