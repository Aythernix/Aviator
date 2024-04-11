using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
   public int score;
   public int highScore;
   

   [SerializeField] private int scoreDefault;

   public void DataReset()
   {
      score = scoreDefault;
   }

   public void SetHighScore()
   {
      highScore = Mathf.Max(score, highScore);
      PlayerPrefs.SetInt("Highscore", highScore);
      PlayerPrefs.Save();
   }
}
