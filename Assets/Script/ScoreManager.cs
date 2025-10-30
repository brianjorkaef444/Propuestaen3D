using UnityEngine;
using TMPro; // 👈 Importante: TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [Header("UI")]
    public TextMeshProUGUI scoreText;      // Texto para mostrar el score
    public TextMeshProUGUI highScoreText;  // Texto para mostrar el high score

    [Header("Score")]
    public float scoreMultiplier = 1f;     // Multiplicador de puntuación
    [HideInInspector] public float score = 0f; // Score actual

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Actualiza el texto del score
        if (scoreText != null)
            scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();

        // Actualiza el high score si existe
        if (highScoreText != null)
        {
            int high = PlayerPrefs.GetInt("HighScore", 0);
            highScoreText.text = "High Score: " + high.ToString();
        }
    }
}
