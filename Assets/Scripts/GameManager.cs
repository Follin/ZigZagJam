using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int Score = 0;

    [Header("In Game")]
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _highscoreText;

    [Header("Death Screen")]
    [SerializeField] GameObject _deathCanvas;
    [SerializeField] TextMeshProUGUI _deathScoreText;
    [SerializeField] TextMeshProUGUI _deathHighscoreText;
    [SerializeField] TextMeshProUGUI _youGotHighscoreText;

    private SoundManager _soundManager;

    private void Awake()
    {
        if (!_highscoreText && !_deathHighscoreText) Debug.LogError("No score text in " + this);
        _highscoreText.text = "Highscore: " + GetHighscore;
        _deathHighscoreText.text = "Highscore: " + GetHighscore;
    }
    private void Start()
    {
        _soundManager = FindObjectOfType<SoundManager>();
        FindObjectOfType<Road>().StartBuilding();

        if (!_scoreText && !_deathScoreText) Debug.LogError("No score text in " + this);        
        _scoreText.text = "Score: 0";
        _deathScoreText.text = "Score: 0";

        _youGotHighscoreText.gameObject.SetActive(false);
        _deathScoreText.gameObject.SetActive(true);
        _deathCanvas.SetActive(false);
    }
    private void Update()
    {       
        if (Input.GetKeyDown(KeyCode.Escape))
            EndGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        _soundManager.IsFalling = false;

        SetInGameUI(true);
        _soundManager.ResetPitch();
    }
    public void EndGame()
    {
        _soundManager.IsFalling = false;
        SetInGameUI(true);

        _soundManager.ResetPitch();
        SceneManager.LoadScene(0);
    }

    public void IncreaseScore()
    {
        Score++;
        _scoreText.text = "Score: " + Score;
        _deathScoreText.text = "Score: " + Score;

        if (Score > GetHighscore)
        {
            PlayerPrefs.SetInt("Highscore", Score);
            _highscoreText.text = "Highscore: " + Score;
            _deathHighscoreText.text = "Highscore: " + Score;
            _youGotHighscoreText.gameObject.SetActive(true);
            _deathScoreText.gameObject.SetActive(false);
        }
        else
        {
            _youGotHighscoreText.gameObject.SetActive(false);
            _deathScoreText.gameObject.SetActive(true);
        }

    }
    private int GetHighscore => PlayerPrefs.GetInt("Highscore");

    public void Death() => SetInGameUI(false);    

    private void SetInGameUI(bool active)
    {
        _scoreText.gameObject.SetActive(active);
        _highscoreText.gameObject.SetActive(active);
        _deathCanvas.SetActive(!active);
    }
}
