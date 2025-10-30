using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public int highScore;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void CheckHighScore(int score)
    {
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
}
