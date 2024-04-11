using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsScript : MonoBehaviour
{
    public AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
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
    
    
    
}
