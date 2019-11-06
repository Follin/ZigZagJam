using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int Score = 0;

    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _highscoreText;      

    private void Awake()
    {
        if (!_highscoreText) Debug.LogError("No score text in " + this);
        _highscoreText.text = "Highscore: " + GetHighscore;
    }
    private void Start()
    {
        FindObjectOfType<Road>().StartBuilding();

        if (!_scoreText) Debug.LogError("No score text in " + this);        
        _scoreText.text = "Score: 0";
    }
    private void Update()
    {       
        if (Input.GetKeyDown(KeyCode.Escape))
            EndGame();
    }

    public void RestartGame() => SceneManager.LoadScene(1);
    public void EndGame() => SceneManager.LoadScene(0);

    public void IncreaseScore()
    {
        Score++;
        _scoreText.text = "Score: " + Score;

        if (Score > GetHighscore)
        {
            PlayerPrefs.SetInt("Highscore", Score);
            _highscoreText.text = "Highscore: " + Score;
        }
    }
    private int GetHighscore => PlayerPrefs.GetInt("Highscore");
}
