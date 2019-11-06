﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame() => SceneManager.LoadScene(1);
    public void Options() => Debug.Log("Options");
    public void Quit() => Application.Quit();
}
