﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame() => SceneManager.LoadScene(1);
    public void Quit() => Application.Quit();

    private void Start()
    {
        Cursor.visible = false;
    }
}
