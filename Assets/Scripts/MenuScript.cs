using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public AudioSource sound;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        StartCoroutine(_Play());
    }
    
    public void Options()
    {
        StartCoroutine(_Options());
    }

    private IEnumerator _Play()
    {
        sound.Play();
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadSceneAsync(sceneBuildIndex: 1, LoadSceneMode.Single);
    }

    private IEnumerator _Options()
    {
        sound.Play();
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadSceneAsync(sceneBuildIndex: 3, LoadSceneMode.Additive);
    }
}
