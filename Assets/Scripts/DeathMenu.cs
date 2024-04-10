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
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
