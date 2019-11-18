using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    private int Score = 0;

    [SerializeField] PlayerController _player;

    [Header("In Game")]
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _highscoreText;

    [Header("Death Screen")]
    [SerializeField] GameObject _deathCanvas;
    [SerializeField] TextMeshProUGUI _deathScoreText;
    [SerializeField] TextMeshProUGUI _deathHighscoreText;
    [SerializeField] TextMeshProUGUI _youGotHighscoreText;
    [SerializeField] Button _selectedButton;

    private SoundManager _soundManager;

    [HideInInspector] public bool StopUpdating = false;

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

        if (_player.IsDead) Death();
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

    public void IncreaseScore(int score)
    {
        Score += score;
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

    private void Death()
    {
        SetInGameUI(false);
        FindObjectOfType<Road>().StopBuilding();
    }
    
    private void SetInGameUI(bool active)
    {
        _scoreText.gameObject.SetActive(active);
        _highscoreText.gameObject.SetActive(active);
        _deathCanvas.SetActive(!active);

        if (!active && _player.IsDead && !StopUpdating)        
            StartCoroutine(Coroutine());   
    }
    IEnumerator Coroutine()
    {
        _selectedButton.OnSelect(null);
        EventSystem.current.SetSelectedGameObject(_selectedButton.gameObject);

        yield return new WaitForEndOfFrame();
        _player.IsDead = false;
        StopUpdating = true;
    }

}
