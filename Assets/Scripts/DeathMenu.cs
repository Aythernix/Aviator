using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public TMP_Text currentScore;
    public TMP_Text highScore;
    public PlayerData playerData;
    public GameObject sound;
    
    // Start is called before the first frame update
    void Start()
    {
        currentScore.text = playerData.score.ToString();

        highScore.text = playerData.highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAgain()
    {
        StartCoroutine(_PlayAgain());
    }
    public void QuitGame()
    {
        StartCoroutine(_QuitGame());
    }

    private IEnumerator _PlayAgain()
    {
        sound.GetComponent<AudioSource>().Play();
        yield return new WaitForSecondsRealtime(0.3f);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }

    private IEnumerator _QuitGame()
    {
        sound.GetComponent<AudioSource>().Play();
        yield return new WaitForSecondsRealtime(0.3f);
        Application.Quit();
    }
}
