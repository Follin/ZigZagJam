using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    int _score = 0;

    void Start()
    {
        Debug.Log("HIGHSCORE: " + GetHighScore());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _score++;
            if (_score > GetHighScore())            
                PlayerPrefs.SetInt("highscore", _score);                
            
            Debug.Log("Score: " + _score);
        }
    }

    int GetHighScore()
    {
        int highscore = PlayerPrefs.GetInt("highscore", 0);
        return highscore;
    }
}
